using System.Diagnostics;

namespace SDL3.Tests.Repository;

internal static class FileNameTests
{
    public static void TrackedFilePaths_DoNotContainCyrillicCharacters()
    {
        string repositoryRoot = FindRepositoryRoot(AppContext.BaseDirectory);

        using Process process = StartGitLsFiles(repositoryRoot);

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        TestAssert.Equal(
            0,
            process.ExitCode,
            $"git ls-files failed while checking tracked file paths. stderr: {error}");

        string[] pathsWithCyrillic = output
            .Split('\0', StringSplitOptions.RemoveEmptyEntries)
            .Where(ContainsCyrillic)
            .Order(StringComparer.Ordinal)
            .ToArray();

        TestAssert.Equal(
            0,
            pathsWithCyrillic.Length,
            "Tracked file paths must not contain Cyrillic characters: "
            + string.Join(", ", pathsWithCyrillic));
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
}
