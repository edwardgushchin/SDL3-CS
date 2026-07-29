using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SDL3.Generators;

MainCallbacksGeneratorTests.GeneratesEntryPointForOptedInCallbackType();
Console.WriteLine("Managed main callback generator positive test passed.");
MainCallbacksGeneratorTests.DoesNotGenerateWithoutOptIn();
Console.WriteLine("Managed main callback generator opt-in test passed.");
MainCallbacksGeneratorTests.ReportsMissingPartial();
Console.WriteLine("Managed main callback generator partial diagnostic test passed.");
MainCallbacksGeneratorTests.ReportsMultipleCallbackTypes();
Console.WriteLine("Managed main callback generator uniqueness diagnostic test passed.");
MainCallbacksGeneratorTests.ReportsIncompatibleContract();
Console.WriteLine("Managed main callback generator contract diagnostic test passed.");
MainCallbacksGeneratorTests.ReportsExistingEntryPoint();
Console.WriteLine("Managed main callback generator entry-point diagnostic test passed.");
MainCallbacksGeneratorTests.ReportsTopLevelStatements();
Console.WriteLine("Managed main callback generator top-level statements diagnostic test passed.");
MainCallbacksGeneratorTests.ReportsUnsupportedTypeShape();
Console.WriteLine("Managed main callback generator type-shape diagnostic test passed.");
MainCallbacksGeneratorTests.ReportsNonExecutableProject();
Console.WriteLine("Managed main callback generator output-kind diagnostic test passed.");

internal static class MainCallbacksGeneratorTests
{
    private const string ValidCallbackType = """
        #nullable enable
        using SDL3;

        namespace Demo;

        [SDL.GenerateMain]
        internal sealed partial class Game : SDL.IMainCallbacks<Game>
        {
            public static SDL.AppResult AppInit(out Game? appState, string[] args)
            {
                appState = new Game();
                return SDL.AppResult.Continue;
            }

            public SDL.AppResult AppIterate() => SDL.AppResult.Continue;
            public SDL.AppResult AppEvent(ref SDL.Event @event) => SDL.AppResult.Continue;
            public void AppQuit(SDL.AppResult result) { }
        }
        """;

    public static void GeneratesEntryPointForOptedInCallbackType()
    {
        GeneratorRun run = Run(ValidCallbackType);

        AssertEqual(0, run.GeneratorDiagnostics.Count(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error), "Valid opt-in must not produce generator errors.");
        AssertEqual(1, run.GeneratedSources.Length, "Valid opt-in must generate exactly one source file.");
        string generated = run.GeneratedSources[0];
        AssertContains("public static int Main(string[] args)", generated, "Generated source must contain a conventional entry point.");
        AssertContains("global::SDL3.SDL.RunMainCallbacks<global::Demo.Game>(args)", generated, "Generated entry point must delegate to the managed runner.");
        Diagnostic[] compilationErrors = run.OutputCompilation.GetDiagnostics().Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error).ToArray();
        AssertEqual(0, compilationErrors.Length, $"Generated compilation must succeed: {string.Join(Environment.NewLine, compilationErrors.Select(diagnostic => diagnostic.ToString()))}");
    }

    public static void DoesNotGenerateWithoutOptIn()
    {
        string source = ValidCallbackType.Replace("[SDL.GenerateMain]", string.Empty, StringComparison.Ordinal);
        GeneratorRun run = Run(source, OutputKind.DynamicallyLinkedLibrary);

        AssertEqual(0, run.GeneratedSources.Length, "A project without opt-in must not receive generated sources.");
        AssertEqual(0, run.GeneratorDiagnostics.Length, "A project without opt-in must not receive generator diagnostics.");
    }

    public static void ReportsMissingPartial()
    {
        string source = ValidCallbackType.Replace("partial class Game", "class Game", StringComparison.Ordinal);
        AssertDiagnostic(source, "SDLGEN001");
    }

    public static void ReportsMultipleCallbackTypes()
    {
        string secondType = """

            [SDL.GenerateMain]
            internal sealed partial class SecondGame : SDL.IMainCallbacks<SecondGame>
            {
                public static SDL.AppResult AppInit(out SecondGame? appState, string[] args)
                {
                    appState = new SecondGame();
                    return SDL.AppResult.Continue;
                }

                public SDL.AppResult AppIterate() => SDL.AppResult.Continue;
                public SDL.AppResult AppEvent(ref SDL.Event @event) => SDL.AppResult.Continue;
                public void AppQuit(SDL.AppResult result) { }
            }
            """;
        AssertDiagnostic(ValidCallbackType + secondType, "SDLGEN002");
    }

    public static void ReportsIncompatibleContract()
    {
        string source = """
            using SDL3;
            [SDL.GenerateMain]
            internal sealed partial class Game
            {
            }
            """;
        AssertDiagnostic(source, "SDLGEN003");
    }

    public static void ReportsExistingEntryPoint()
    {
        string source = ValidCallbackType + """

            internal static class Program
            {
                public static void Main(string[] args) { }
            }
            """;
        AssertDiagnostic(source, "SDLGEN004");
    }

    public static void ReportsTopLevelStatements()
    {
        string source = ValidCallbackType.Replace("namespace Demo;", "System.Console.WriteLine(\"existing\");", StringComparison.Ordinal);
        AssertDiagnostic(source, "SDLGEN004");
    }

    public static void ReportsUnsupportedTypeShape()
    {
        string source = ValidCallbackType.Replace("sealed partial class Game", "abstract partial class Game", StringComparison.Ordinal);
        AssertDiagnostic(source, "SDLGEN005");
    }

    public static void ReportsNonExecutableProject()
    {
        AssertDiagnostic(ValidCallbackType, "SDLGEN006", OutputKind.DynamicallyLinkedLibrary);
    }

    private static void AssertDiagnostic(string source, string expectedId, OutputKind outputKind = OutputKind.ConsoleApplication)
    {
        GeneratorRun run = Run(source, outputKind);
        Diagnostic[] matches = run.GeneratorDiagnostics.Where(diagnostic => diagnostic.Id == expectedId).ToArray();
        int expectedCount = expectedId == "SDLGEN002" ? 2 : 1;
        AssertEqual(expectedCount, matches.Length, $"Generator must report {expectedId}. Actual: {string.Join(", ", run.GeneratorDiagnostics.Select(diagnostic => diagnostic.Id))}");
        AssertEqual(0, run.GeneratedSources.Length, $"Generator must not emit source when reporting {expectedId}.");
    }

    private static GeneratorRun Run(string source, OutputKind outputKind = OutputKind.ConsoleApplication)
    {
        CSharpParseOptions parseOptions = new(LanguageVersion.Preview);
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source, parseOptions);
        IEnumerable<MetadataReference> references = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!)
            .Split(Path.PathSeparator)
            .Select(path => MetadataReference.CreateFromFile(path))
            .Append(MetadataReference.CreateFromFile(typeof(SDL3.SDL).Assembly.Location));
        CSharpCompilation compilation = CSharpCompilation.Create(
            "GeneratorConsumer",
            [syntaxTree],
            references,
            new CSharpCompilationOptions(outputKind, nullableContextOptions: NullableContextOptions.Enable));

        ISourceGenerator generator = new MainCallbacksGenerator().AsSourceGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create([generator], parseOptions: parseOptions);
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out Compilation outputCompilation, out _);
        GeneratorDriverRunResult result = driver.GetRunResult();
        string[] generatedSources = result.Results.SelectMany(generatorResult => generatorResult.GeneratedSources).Select(generated => generated.SourceText.ToString()).ToArray();
        Diagnostic[] diagnostics = result.Results.SelectMany(generatorResult => generatorResult.Diagnostics).ToArray();
        return new GeneratorRun(outputCompilation, generatedSources, diagnostics);
    }

    private static void AssertContains(string expected, string actual, string message)
    {
        if (!actual.Contains(expected, StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"{message} Expected fragment: {expected}{Environment.NewLine}Generated:{Environment.NewLine}{actual}");
        }
    }

    private static void AssertEqual<T>(T expected, T actual, string message)
    {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
        {
            throw new InvalidOperationException($"{message} Expected: {expected}; actual: {actual}.");
        }
    }

    private sealed record GeneratorRun(Compilation OutputCompilation, string[] GeneratedSources, Diagnostic[] GeneratorDiagnostics);
}
