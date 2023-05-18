using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Threading;
using ZBase.Foundation.SourceGen;

namespace ZBase.Foundation.EnumExtensions
{
    [Generator]
    public class EnumExtensionsGenerator : IIncrementalGenerator
    {
        public const string ATTRIBUTE_NAME = "EnumExtensions";
        public const string FULL_EXTENSIONS_ATTRIBUTE_NAME = "global::ZBase.Foundation.EnumExtensions.EnumExtensionsAttribute";
        public const string GENERATOR_NAME = nameof(EnumExtensionsGenerator);

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var projectPathProvider = SourceGenHelpers.GetSourceGenConfigProvider(context);

            var candidateProvider = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: IsSyntaxMatch,
                transform: GetSemanticSyntaxMatch
            ).Where(t => t is { });

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

        public static bool IsSyntaxMatch(
              SyntaxNode syntaxNode
            , CancellationToken token
        )
        {
            token.ThrowIfCancellationRequested();

            if (syntaxNode is not EnumDeclarationSyntax enumSyntax)
            {
                return false;
            }

            if (enumSyntax.AttributeLists == null || enumSyntax.AttributeLists.Count < 1)
            {
                return false;
            }

            foreach (var attribList in enumSyntax.AttributeLists)
            {
                foreach (var attrib in attribList.Attributes)
                {
                    if (attrib.Name is IdentifierNameSyntax identifierNameSyntax
                        && identifierNameSyntax.Identifier.ValueText == ATTRIBUTE_NAME
                    )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static EnumDeclarationSyntax GetSemanticSyntaxMatch(
              GeneratorSyntaxContext syntaxContext
            , CancellationToken token
        )
        {
            token.ThrowIfCancellationRequested();

            if (syntaxContext.Node is not EnumDeclarationSyntax enumSyntax)
            {
                return null;
            }

            var semanticModel = syntaxContext.SemanticModel;

            foreach (var attribList in enumSyntax.AttributeLists)
            {
                foreach (var attrib in attribList.Attributes)
                {
                    var typeInfo = semanticModel.GetTypeInfo(attrib, token);
                    var fullName = typeInfo.Type.ToFullName();

                    if (fullName.StartsWith(FULL_EXTENSIONS_ATTRIBUTE_NAME))
                    {
                        return enumSyntax;
                    }
                }
            }

            return null;
        }

        private static void GenerateOutput(
              SourceProductionContext context
            , Compilation compilation
            , EnumDeclarationSyntax candidate
            , string projectPath
            , bool outputSourceGenFiles
        )
        {
            if (candidate == null)
            {
                return;
            }

            context.CancellationToken.ThrowIfCancellationRequested();

            try
            {
                SourceGenHelpers.ProjectPath = projectPath;

                var syntaxTree = candidate.SyntaxTree;
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                var declaration = new EnumDeclaration(candidate, semanticModel, context.CancellationToken);

                if (declaration.IsValid == false)
                {
                    return;
                }

                var source = declaration.WriteCode();
                var sourceFilePath = syntaxTree.GetGeneratedSourceFilePath(compilation.Assembly.Name, GENERATOR_NAME);
                var outputSource = TypeCreationHelpers.GenerateSourceTextForRootNodes(
                      sourceFilePath
                    , candidate
                    , source
                    , context.CancellationToken
                );

                context.AddSource(
                      syntaxTree.GetGeneratedSourceFileName(GENERATOR_NAME, candidate)
                    , outputSource
                );

                if (outputSourceGenFiles)
                {
                    SourceGenHelpers.OutputSourceToFile(
                          context
                        , candidate.GetLocation()
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
                    , candidate.GetLocation()
                    , e.ToUnityPrintableString()
                ));
            }
        }

        private static readonly DiagnosticDescriptor s_errorDescriptor
            = new("SG_ENUM_EXTENSIONS_01"
                , "Enum Extensions Generator Error"
                , "This error indicates a bug in the Enum Extensions source generators. Error message: '{0}'."
                , "ZBase.Foundation.EnumExtensions.EnumExtensionsAttribute"
                , DiagnosticSeverity.Error
                , isEnabledByDefault: true
                , description: ""
            );
    }
}
