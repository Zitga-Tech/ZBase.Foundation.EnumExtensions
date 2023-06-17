using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Threading;
using ZBase.Foundation.SourceGen;

namespace ZBase.Foundation.EnumExtensions
{
    [Generator]
    public class EnumExtensionsGenerator : IIncrementalGenerator
    {
        public const string ENUM_EXTENSIONS_ATTRIBUTE = "global::ZBase.Foundation.EnumExtensions.EnumExtensionsAttribute";
        public const string FLAGS_ATTRIBUTE = "global::System.FlagsAttribute";
        public const string GENERATOR_NAME = nameof(EnumExtensionsGenerator);

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var projectPathProvider = SourceGenHelpers.GetSourceGenConfigProvider(context);

            var candidateProvider = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: IsValidEnumSyntax,
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

        private static bool IsValidEnumSyntax(SyntaxNode node, CancellationToken _)
        {
            return node is EnumDeclarationSyntax syntax
                && syntax.AttributeLists.Count > 0
                && syntax.HasAttributeCandidate("ZBase.Foundation.EnumExtensions", "EnumExtensions")
                ;
        }

        public static (EnumDeclarationSyntax syntax, INamedTypeSymbol symbol, bool hasFlags) GetSemanticSymbolMatch(
              GeneratorSyntaxContext context
            , CancellationToken token
        )
        {
            token.ThrowIfCancellationRequested();

            if (context.Node is not EnumDeclarationSyntax syntax)
            {
                return (null, null, false);
            }

            var semanticModel = context.SemanticModel;
            var symbol = semanticModel.GetDeclaredSymbol(syntax, token);

            if (symbol == null || symbol.HasAttribute(ENUM_EXTENSIONS_ATTRIBUTE) == false)
            {
                return (null, null, false);
            }

            return (syntax, symbol, symbol.HasAttribute(FLAGS_ATTRIBUTE));
        }

        private static void GenerateOutput(
              SourceProductionContext context
            , Compilation compilation
            , (EnumDeclarationSyntax syntax, INamedTypeSymbol symbol, bool hasFlag) candidate
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
                var declaration = new EnumDeclaration(candidate.syntax, candidate.symbol, candidate.hasFlag);

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
