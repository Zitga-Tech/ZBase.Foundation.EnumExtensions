﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Threading;
using ZBase.Foundation.SourceGen;

namespace ZBase.Foundation.EnumExtensions
{
    [Generator]
    public class EnumExtensionsForGenerator : IIncrementalGenerator
    {
        public const string ENUM_EXTENSIONS_FOR_ATTRIBUTE = "global::ZBase.Foundation.EnumExtensions.EnumExtensionsForAttribute";
        public const string FLAGS_ATTRIBUTE = "global::System.FlagsAttribute";
        public const string GENERATOR_NAME = nameof(EnumExtensionsGenerator);

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var projectPathProvider = SourceGenHelpers.GetSourceGenConfigProvider(context);

            var candidateProvider = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: IsValidClassSyntax,
                transform: GetSemanticSymbolMatch
            ).Where(t => t.syntax is { } && t.symbol is { });

            var compilationProvider = context.CompilationProvider;
            var combined = candidateProvider.Combine(compilationProvider).Combine(projectPathProvider);

            context.RegisterSourceOutput(combined, (sourceProductionContext, source) => {
                GenerateOutput(
                      sourceProductionContext
                    , source.Left.Right
                    , source.Left.Left
                    , source.Right.projectPath
                    , source.Right.outputSourceGenFiles
                );
            });
        }

        private static bool IsValidClassSyntax(SyntaxNode node, CancellationToken _)
        {
            return node is ClassDeclarationSyntax syntax
                && syntax.AttributeLists.Count > 0
                && syntax.HasAttributeCandidate("ZBase.Foundation.EnumExtensions", "EnumExtensionsFor")
                ;
        }

        private static MatchedSemantic GetSemanticSymbolMatch(
              GeneratorSyntaxContext context
            , CancellationToken token
        )
        {
            token.ThrowIfCancellationRequested();

            if (context.Node is not ClassDeclarationSyntax syntax)
            {
                return default;
            }

            var semanticModel = context.SemanticModel;
            var classSymbol = semanticModel.GetDeclaredSymbol(syntax, token);

            if (classSymbol == null || classSymbol.IsStatic == false)
            {
                return default;
            }

            var attribute = classSymbol.GetAttribute(ENUM_EXTENSIONS_FOR_ATTRIBUTE);

            if (attribute == null || attribute.ConstructorArguments.Length < 1)
            {
                return default;
            }

            INamedTypeSymbol symbol = null;

            foreach (var attribList in syntax.AttributeLists)
            {
                foreach (var attrib in attribList.Attributes)
                {
                    if (attrib.Name.IsTypeNameCandidate("ZBase.Foundation.EnumExtensions", "EnumExtensionsFor") == false
                        || attrib.ArgumentList == null
                        || attrib.ArgumentList.Arguments.Count < 1
                    )
                    {
                        continue;
                    }

                    var arg = attrib.ArgumentList.Arguments[0];
                    var expr = arg.Expression;

                    if (expr is TypeOfExpressionSyntax typeOfExpr)
                    {
                        var candidate = semanticModel.GetSymbolInfo(typeOfExpr.Type, token).Symbol as INamedTypeSymbol;

                        if (candidate.TypeKind == TypeKind.Enum)
                        {
                            symbol = candidate;
                        }
                    }

                    if (symbol != null)
                    {
                        break;
                    }
                }

                if (symbol != null)
                {
                    break;
                }
            }

            return new MatchedSemantic {
                syntax = syntax,
                symbol = symbol,
                hasFlags = symbol.HasAttribute(FLAGS_ATTRIBUTE),
                accessibility = classSymbol.DeclaredAccessibility,
            };
        }

        private static void GenerateOutput(
              SourceProductionContext context
            , Compilation compilation
            , MatchedSemantic candidate
            , string projectPath
            , bool outputSourceGenFiles
        )
        {
            if (candidate.syntax == null || candidate.symbol == null)
            {
                return;
            }

            context.CancellationToken.ThrowIfCancellationRequested();

            try
            {
                SourceGenHelpers.ProjectPath = projectPath;

                var syntaxTree = candidate.syntax.SyntaxTree;
                var declaration = new EnumDeclaration(
                      candidate.symbol
                    , candidate.hasFlags
                    , candidate.syntax.Parent is NamespaceDeclarationSyntax
                    , candidate.syntax.Identifier.Text
                    , candidate.accessibility
                );

                var source = declaration.WriteCode();
                var sourceFilePath = syntaxTree.GetGeneratedSourceFilePath(compilation.Assembly.Name, GENERATOR_NAME);
                var outputSource = TypeCreationHelpers.GenerateSourceTextForRootNodes(
                      sourceFilePath
                    , candidate.syntax
                    , source
                    , context.CancellationToken
                );

                context.AddSource(
                      syntaxTree.GetGeneratedSourceFileName(GENERATOR_NAME, candidate.syntax)
                    , outputSource
                );

                if (outputSourceGenFiles)
                {
                    SourceGenHelpers.OutputSourceToFile(
                          context
                        , candidate.syntax.GetLocation()
                        , sourceFilePath
                        , outputSource
                    );
                }
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException)
                {
                    throw;
                }

                context.ReportDiagnostic(Diagnostic.Create(
                      s_errorDescriptor
                    , candidate.syntax.GetLocation()
                    , e.ToUnityPrintableString()
                ));
            }
        }

        private static readonly DiagnosticDescriptor s_errorDescriptor
            = new("SG_ENUM_EXTENSIONS_02"
                , "Enum Extensions For Generator Error"
                , "This error indicates a bug in the Enum Extensions For source generators. Error message: '{0}'."
                , "ZBase.Foundation.EnumExtensions.EnumExtensionsForAttribute"
                , DiagnosticSeverity.Error
                , isEnabledByDefault: true
                , description: ""
            );

        private struct MatchedSemantic
        {
            public ClassDeclarationSyntax syntax;
            public INamedTypeSymbol symbol;
            public bool hasFlags;
            public Accessibility accessibility;
        }
    }
}
