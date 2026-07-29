using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace SDL3.Generators;

[Generator(LanguageNames.CSharp)]
public sealed class MainCallbacksGenerator : IIncrementalGenerator
{
    private const string AttributeMetadataName = "SDL3.SDL+GenerateMainAttribute";
    private const string ContractMetadataName = "SDL3.SDL+IMainCallbacks`1";

    private static readonly DiagnosticDescriptor MissingPartial = new(
        "SDLGEN001",
        "Main callback type must be partial",
        "Type '{0}' must be declared partial so SDL3-CS can generate its Main entry point",
        "SDL3-CS.MainCallbacks",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor MultipleCallbackTypes = new(
        "SDLGEN002",
        "Only one generated SDL entry point is allowed",
        "Project contains multiple types marked with SDL.GenerateMain; keep the attribute only on one callback type",
        "SDL3-CS.MainCallbacks",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor IncompatibleContract = new(
        "SDLGEN003",
        "Main callback type does not implement its SDL contract",
        "Type '{0}' must implement SDL.IMainCallbacks<{0}>",
        "SDL3-CS.MainCallbacks",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor ExistingEntryPoint = new(
        "SDLGEN004",
        "Project already has an entry point",
        "SDL3-CS cannot generate Main for '{0}' because the project already contains an entry point or top-level statements",
        "SDL3-CS.MainCallbacks",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor UnsupportedTypeShape = new(
        "SDLGEN005",
        "Unsupported main callback type",
        "Type '{0}' must be a top-level, non-abstract, non-generic class",
        "SDL3-CS.MainCallbacks",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor UnsupportedOutputKind = new(
        "SDLGEN006",
        "Generated SDL entry point requires an executable project",
        "SDL.GenerateMain on '{0}' requires OutputType Exe or WinExe",
        "SDL3-CS.MainCallbacks",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<INamedTypeSymbol> candidates = context.SyntaxProvider.ForAttributeWithMetadataName(
            AttributeMetadataName,
            static (node, _) => node is ClassDeclarationSyntax,
            static (attributeContext, _) => (INamedTypeSymbol)attributeContext.TargetSymbol);

        IncrementalValueProvider<(Compilation Compilation, ImmutableArray<INamedTypeSymbol> Candidates)> input =
            context.CompilationProvider.Combine(candidates.Collect());

        context.RegisterSourceOutput(input, static (sourceContext, value) =>
            Generate(sourceContext, value.Compilation, value.Candidates));
    }

    private static void Generate(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<INamedTypeSymbol> candidates)
    {
        if (candidates.IsDefaultOrEmpty)
        {
            return;
        }

        List<INamedTypeSymbol> uniqueCandidates = [];
        foreach (INamedTypeSymbol candidate in candidates)
        {
            if (!uniqueCandidates.Any(existing => SymbolEqualityComparer.Default.Equals(existing, candidate)))
            {
                uniqueCandidates.Add(candidate);
            }
        }

        if (uniqueCandidates.Count != 1)
        {
            foreach (INamedTypeSymbol candidate in uniqueCandidates)
            {
                context.ReportDiagnostic(Diagnostic.Create(MultipleCallbackTypes, GetLocation(candidate)));
            }

            return;
        }

        INamedTypeSymbol callbackType = uniqueCandidates[0];
        if (!IsPartial(callbackType, context.CancellationToken))
        {
            context.ReportDiagnostic(Diagnostic.Create(MissingPartial, GetLocation(callbackType), callbackType.Name));
            return;
        }

        if (callbackType.TypeKind != TypeKind.Class ||
            callbackType.IsAbstract ||
            callbackType.IsStatic ||
            callbackType.Arity != 0 ||
            callbackType.ContainingType is not null)
        {
            context.ReportDiagnostic(Diagnostic.Create(UnsupportedTypeShape, GetLocation(callbackType), callbackType.Name));
            return;
        }

        INamedTypeSymbol? contract = compilation.GetTypeByMetadataName(ContractMetadataName);
        bool implementsSelfContract = contract is not null && callbackType.AllInterfaces.Any(candidateContract =>
            SymbolEqualityComparer.Default.Equals(candidateContract.OriginalDefinition, contract) &&
            candidateContract.TypeArguments.Length == 1 &&
            SymbolEqualityComparer.Default.Equals(candidateContract.TypeArguments[0], callbackType));
        if (!implementsSelfContract)
        {
            context.ReportDiagnostic(Diagnostic.Create(IncompatibleContract, GetLocation(callbackType), callbackType.Name));
            return;
        }

        if (compilation.Options.OutputKind is not OutputKind.ConsoleApplication and not OutputKind.WindowsApplication)
        {
            context.ReportDiagnostic(Diagnostic.Create(UnsupportedOutputKind, GetLocation(callbackType), callbackType.Name));
            return;
        }

        if (compilation.GetEntryPoint(context.CancellationToken) is not null)
        {
            context.ReportDiagnostic(Diagnostic.Create(ExistingEntryPoint, GetLocation(callbackType), callbackType.Name));
            return;
        }

        string typeName = EscapeIdentifier(callbackType.Name);
        string fullyQualifiedTypeName = callbackType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        string? namespaceName = callbackType.ContainingNamespace is { IsGlobalNamespace: false } containingNamespace
            ? containingNamespace.ToDisplayString()
            : null;
        StringBuilder source = new();
        source.AppendLine("// <auto-generated/>");
        source.AppendLine("#nullable enable");
        if (namespaceName is not null)
        {
            source.Append("namespace ").Append(namespaceName).AppendLine();
            source.AppendLine("{");
        }

        string indent = namespaceName is null ? string.Empty : "    ";
        source.Append(indent).Append("partial class ").Append(typeName).AppendLine();
        source.Append(indent).AppendLine("{");
        source.Append(indent).AppendLine("    /// <summary>Runs the SDL managed main-callback application.</summary>");
        source.Append(indent).AppendLine("    /// <param name=\"args\">Application arguments, excluding the executable path.</param>");
        source.Append(indent).AppendLine("    /// <returns>The platform main return code.</returns>");
        source.Append(indent).AppendLine("    public static int Main(string[] args)");
        source.Append(indent).Append("        => global::SDL3.SDL.RunMainCallbacks<")
            .Append(fullyQualifiedTypeName)
            .AppendLine(">(args);");
        source.Append(indent).AppendLine("}");
        if (namespaceName is not null)
        {
            source.AppendLine("}");
        }

        string hintName = $"SDL3.MainCallbacks.{SanitizeHintName(callbackType)}.g.cs";
        context.AddSource(hintName, SourceText.From(source.ToString(), Encoding.UTF8));
    }

    private static bool IsPartial(INamedTypeSymbol type, CancellationToken cancellationToken)
    {
        ImmutableArray<SyntaxReference> declarations = type.DeclaringSyntaxReferences;
        return declarations.Length != 0 && declarations.All(reference =>
            reference.GetSyntax(cancellationToken) is TypeDeclarationSyntax declaration &&
            declaration.Modifiers.Any(SyntaxKind.PartialKeyword));
    }

    private static Location GetLocation(INamedTypeSymbol type)
    {
        return type.Locations.FirstOrDefault(location => location.IsInSource) ?? Location.None;
    }

    private static string EscapeIdentifier(string identifier)
    {
        return SyntaxFacts.GetKeywordKind(identifier) == SyntaxKind.None ? identifier : $"@{identifier}";
    }

    private static string SanitizeHintName(INamedTypeSymbol type)
    {
        string name = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        foreach (char invalidCharacter in new[] { '<', '>', ':', '"', '/', '\\', '|', '?', '*', '.', '+' })
        {
            name = name.Replace(invalidCharacter, '_');
        }

        return name;
    }
}
