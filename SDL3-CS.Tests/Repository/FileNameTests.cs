using System.Diagnostics;

namespace SDL3.Tests.Repository;

internal static class FileNameTests
{
    public static void TrackedFilePaths_DoNotContainCyrillicCharacters()
    {
        string repositoryRoot = FindRepositoryRoot(AppContext.BaseDirectory);
        string[] trackedPaths = GetTrackedPaths(repositoryRoot);

        string[] pathsWithCyrillic = trackedPaths
            .Where(ContainsCyrillic)
            .Order(StringComparer.Ordinal)
            .ToArray();

        TestAssert.Equal(
            0,
            pathsWithCyrillic.Length,
            "Tracked file paths must not contain Cyrillic characters: "
            + string.Join(", ", pathsWithCyrillic));
    }

    public static void TrackedCSharpIdentifiers_DoNotContainCyrillicCharacters()
    {
        string repositoryRoot = FindRepositoryRoot(AppContext.BaseDirectory);

        string[] identifiersWithCyrillic = GetTrackedPaths(repositoryRoot)
            .Where(path => path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
            .SelectMany(path => FindCyrillicIdentifiers(repositoryRoot, path))
            .Order(StringComparer.Ordinal)
            .ToArray();

        TestAssert.Equal(
            0,
            identifiersWithCyrillic.Length,
            "Tracked C# identifiers must not contain Cyrillic characters:"
            + Environment.NewLine
            + FormatViolations(identifiersWithCyrillic));
    }

    public static void WrapperSourceFiles_DoNotContainCyrillicCharacters()
    {
        string repositoryRoot = FindRepositoryRoot(AppContext.BaseDirectory);

        string[] sourceLinesWithCyrillic = GetTrackedPaths(repositoryRoot)
            .Where(path => path.StartsWith("SDL3-CS/", StringComparison.Ordinal))
            .Where(path => path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
            .SelectMany(path => FindCyrillicSourceLines(repositoryRoot, path))
            .Order(StringComparer.Ordinal)
            .ToArray();

        TestAssert.Equal(
            0,
            sourceLinesWithCyrillic.Length,
            "Wrapper source files must not contain Cyrillic characters:"
            + Environment.NewLine
            + FormatViolations(sourceLinesWithCyrillic));
    }

    private static string[] GetTrackedPaths(string repositoryRoot)
    {
        using Process process = StartGitLsFiles(repositoryRoot);

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        TestAssert.Equal(
            0,
            process.ExitCode,
            $"git ls-files failed while checking tracked file paths. stderr: {error}");

        return output
            .Split('\0', StringSplitOptions.RemoveEmptyEntries);
    }

    private static IEnumerable<string> FindCyrillicIdentifiers(string repositoryRoot, string relativePath)
    {
        string text = File.ReadAllText(GetFullPath(repositoryRoot, relativePath));
        var scanner = new CSharpIdentifierScanner(relativePath, text);

        return scanner.FindCyrillicIdentifiers();
    }

    private static IEnumerable<string> FindCyrillicSourceLines(string repositoryRoot, string relativePath)
    {
        string fullPath = GetFullPath(repositoryRoot, relativePath);
        int lineNumber = 0;

        foreach (string line in File.ReadLines(fullPath))
        {
            lineNumber++;
            if (ContainsCyrillic(line))
            {
                yield return $"{relativePath}:{lineNumber}: {line.Trim()}";
            }
        }
    }

    private static Process StartGitLsFiles(string repositoryRoot)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "git",
            WorkingDirectory = repositoryRoot,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };

        startInfo.ArgumentList.Add("ls-files");
        startInfo.ArgumentList.Add("--full-name");
        startInfo.ArgumentList.Add("-z");

        return Process.Start(startInfo)
            ?? throw new InvalidOperationException("Unable to start git ls-files.");
    }

    private static string FindRepositoryRoot(string startPath)
    {
        var directory = new DirectoryInfo(startPath);

        while (directory is not null)
        {
            string gitPath = Path.Combine(directory.FullName, ".git");
            if (Directory.Exists(gitPath) || File.Exists(gitPath))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new InvalidOperationException($"Unable to find repository root from {startPath}.");
    }

    private static bool ContainsCyrillic(string value)
    {
        return value.Any(character => character is >= '\u0400' and <= '\u04FF');
    }

    private static string GetFullPath(string repositoryRoot, string relativePath)
    {
        return Path.Combine(repositoryRoot, relativePath.Replace('/', Path.DirectorySeparatorChar));
    }

    private static string FormatViolations(IReadOnlyCollection<string> violations)
    {
        const int maxDisplayedViolations = 30;

        string message = string.Join(Environment.NewLine, violations.Take(maxDisplayedViolations));
        if (violations.Count > maxDisplayedViolations)
        {
            message += Environment.NewLine
                + $"...and {violations.Count - maxDisplayedViolations} more violation(s).";
        }

        return message;
    }

    private sealed class CSharpIdentifierScanner(string path, string text)
    {
        private readonly List<string> violations = [];
        private int index;
        private int line = 1;
        private int column = 1;

        public string[] FindCyrillicIdentifiers()
        {
            while (index < text.Length)
            {
                if (TrySkipTriviaOrLiteral())
                {
                    continue;
                }

                if (IsIdentifierStart(Current) || IsVerbatimIdentifierStart())
                {
                    ScanIdentifier();
                    continue;
                }

                Advance();
            }

            return [.. violations];
        }

        private bool TrySkipTriviaOrLiteral()
        {
            if (Current == '/' && Peek(1) == '/')
            {
                SkipLineComment();
                return true;
            }

            if (Current == '/' && Peek(1) == '*')
            {
                SkipBlockComment();
                return true;
            }

            if (Current == '$' && Peek(1) == '@' && Peek(2) == '"')
            {
                Advance();
                Advance();
                SkipVerbatimString();
                return true;
            }

            if (Current == '@' && Peek(1) == '$' && Peek(2) == '"')
            {
                Advance();
                Advance();
                SkipVerbatimString();
                return true;
            }

            if (Current == '@' && Peek(1) == '"')
            {
                Advance();
                SkipVerbatimString();
                return true;
            }

            if (Current == '$' && Peek(1) == '"')
            {
                Advance();
                SkipRegularString();
                return true;
            }

            if (Current == '"')
            {
                SkipRegularString();
                return true;
            }

            if (Current == '\'')
            {
                SkipCharacterLiteral();
                return true;
            }

            return false;
        }

        private void ScanIdentifier()
        {
            int startLine = line;
            int startColumn = column;
            int startIndex = index;

            if (Current == '@')
            {
                Advance();
            }

            while (index < text.Length && IsIdentifierPart(Current))
            {
                Advance();
            }

            string identifier = text[startIndex..index];
            if (ContainsCyrillic(identifier))
            {
                violations.Add($"{path}:{startLine}:{startColumn}: {identifier}");
            }
        }

        private void SkipLineComment()
        {
            while (index < text.Length && Current is not '\r' and not '\n')
            {
                Advance();
            }
        }

        private void SkipBlockComment()
        {
            Advance();
            Advance();

            while (index < text.Length)
            {
                if (Current == '*' && Peek(1) == '/')
                {
                    Advance();
                    Advance();
                    return;
                }

                Advance();
            }
        }

        private void SkipRegularString()
        {
            Advance();

            while (index < text.Length)
            {
                if (Current == '\\')
                {
                    Advance();
                    if (index < text.Length)
                    {
                        Advance();
                    }

                    continue;
                }

                if (Current == '"')
                {
                    Advance();
                    return;
                }

                Advance();
            }
        }

        private void SkipVerbatimString()
        {
            Advance();

            while (index < text.Length)
            {
                if (Current == '"' && Peek(1) == '"')
                {
                    Advance();
                    Advance();
                    continue;
                }

                if (Current == '"')
                {
                    Advance();
                    return;
                }

                Advance();
            }
        }

        private void SkipCharacterLiteral()
        {
            Advance();

            while (index < text.Length)
            {
                if (Current == '\\')
                {
                    Advance();
                    if (index < text.Length)
                    {
                        Advance();
                    }

                    continue;
                }

                if (Current == '\'')
                {
                    Advance();
                    return;
                }

                Advance();
            }
        }

        private bool IsVerbatimIdentifierStart()
        {
            return Current == '@' && IsIdentifierStart(Peek(1));
        }

        private static bool IsIdentifierStart(char character)
        {
            return character == '_' || char.IsLetter(character);
        }

        private static bool IsIdentifierPart(char character)
        {
            return character == '_' || char.IsLetterOrDigit(character);
        }

        private char Current => index < text.Length ? text[index] : '\0';

        private char Peek(int offset)
        {
            int position = index + offset;
            return position < text.Length ? text[position] : '\0';
        }

        private void Advance()
        {
            if (Current == '\r')
            {
                index++;
                if (Current == '\n')
                {
                    index++;
                }

                line++;
                column = 1;
                return;
            }

            if (Current == '\n')
            {
                index++;
                line++;
                column = 1;
                return;
            }

            index++;
            column++;
        }
    }
}
