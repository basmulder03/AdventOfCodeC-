using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Core.InputDownloader;

public static class ConsoleWindowHelper
{
    private static readonly string StateFilePath = Path.Combine(Path.GetTempPath(), "ConsoleWindowHelperState.txt");
    private static readonly Dictionary<string, Process?> OpenWindows = LoadState();

    /// <summary>
    ///     Opens a new GUI window displaying the content of the provided file stream.
    ///     Ensures that only one window per unique identifier (e.g., year and day) is open at a time.
    /// </summary>
    /// <param name="identifier">A unique identifier for the data, such as "Year2024Day1".</param>
    /// <param name="path">The path of the input</param>
    public static void ShowInGuiWindow(string identifier, string path)
    {
        if (OpenWindows.TryGetValue(identifier, out var existingProcess))
        {
            if (existingProcess is { HasExited: false })
                // Bring the existing window to the front if it is still active.
                return;

            // If the process has exited, remove it from the dictionary.
            OpenWindows.Remove(identifier);
        }

        // Close all other open windows that do not match the current identifier.
        foreach (var key in new List<string>(OpenWindows.Keys))
        {
            var process = OpenWindows[key];
            if (key == identifier || process == null || process.HasExited) continue;
            try
            {
                process.Kill();
                process.Dispose();
            }
            catch (Exception)
            {
                // Safely handle any exceptions during process termination.
            }

            OpenWindows.Remove(key);
        }

        // Display the content using a GUI-based viewer.
        StartGuiViewer(identifier, path);
    }

    /// <summary>
    ///     Generates a unique identifier for the data, such as "Year2024Day1".
    /// </summary>
    /// <param name="year">The year of the data.</param>
    /// <param name="day">The day of the data.</param>
    /// <returns>A unique string identifier.</returns>
    public static string CreateIdentifier(int year, int day)
    {
        return $"Year{year}Day{day}";
    }

    /// <summary>
    ///     Starts a cross-platform GUI viewer to display the content.
    /// </summary>
    /// <param name="identifier">The identifier for the content.</param>
    /// <param name="path">The text content to display.</param>
    private static void StartGuiViewer(string identifier, string path)
    {
        var process = new Process();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "notepad.exe",
                Arguments = path,
                UseShellExecute = true
            };
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "xterm",
                Arguments = $"-hold -e 'cat {path}'",
                UseShellExecute = true
            };
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "open",
                Arguments = $"-a TextEdit {path}",
                UseShellExecute = true
            };
        else
            throw new PlatformNotSupportedException("The current platform is not supported.");

        process.Start();
        OpenWindows[identifier] = process;
        SaveState();
    }

    /// <summary>
    ///     Saves the current state of open windows to a file for persistence.
    /// </summary>
    private static void SaveState()
    {
        var activeWindows = OpenWindows.Where(kv => kv.Value != null).Select(kv => kv.Key);
        File.WriteAllLines(StateFilePath, activeWindows);
    }

    /// <summary>
    ///     Loads the state of open windows from a file.
    /// </summary>
    /// <returns>A dictionary of open windows with dummy processes for initialization.</returns>
    private static Dictionary<string, Process?> LoadState()
    {
        if (!File.Exists(StateFilePath))
            return new Dictionary<string, Process?>();

        var identifiers = File.ReadAllLines(StateFilePath);
        var state = new Dictionary<string, Process?>();

        foreach (var identifier in identifiers)
            // Create a dummy process entry. The actual process will be managed during runtime.
            state[identifier] = null;

        return state;
    }
}