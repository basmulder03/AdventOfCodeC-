using System.Diagnostics;
using System.Reflection;
using System.Text;
using Core.InputDownloader;
using Core.Interfaces;

namespace Solutions;

public static class AdventOfCodeRunner
{
    private static readonly Assembly
        LocalAssembly = Assembly.GetExecutingAssembly(); // Reference to the executing assembly.

    /// <summary>
    ///     Runs the latest available day of the latest year.
    /// </summary>
    public static async Task RunLatest()
    {
        var latestYear = GetLatestYear(); // Get the most recent year.
        await RunLatest(latestYear); // Run the latest day of that year.
    }

    /// <summary>
    ///     Runs the latest available day of the specified year.
    /// </summary>
    /// <param name="year">The year to run the latest day for.</param>
    public static async Task RunLatest(int year)
    {
        var lastDay = GetLastDayForYear(year); // Find the last day for the specified year.
        if (lastDay == 0)
        {
            Console.WriteLine($"No days found for year {year}.");
            return;
        }

        await RunDay(year, lastDay); // Run the last available day.
    }

    /// <summary>
    ///     Runs a specific day of a given year.
    /// </summary>
    /// <param name="year">The year of the day to run.</param>
    /// <param name="day">The day to run.</param>
    public static async Task RunDay(int year, int day)
    {
        var types = GetTypesInNamespace(LocalAssembly,
            $"Solutions._{year}"); // Get all types in the namespace for the year.
        var type = types.FirstOrDefault(t =>
            t.Name == $"Day{day}" && typeof(IBaseDay).IsAssignableFrom(t)); // Find the matching day class.

        if (type == null)
        {
            Console.WriteLine($"Day {day} not found for year {year}.");
            return;
        }

        var instance = (IBaseDay)Activator.CreateInstance(type)!; // Create an instance of the day class.
        var filePath = await PuzzleInputForDayExist(LocalAssembly, year, day); // Ensure the input for the day exists.

        var results =
            new List<(int Year, int Day, int Part, string Result, string Runtime)>(); // Initialize the results table.

        // Get the input data
        using var reader = new StreamReader(filePath, Encoding.UTF8);
        var input = await reader.ReadToEndAsync();

        // Open a console window with the input
        var windowIdentifier = ConsoleWindowHelper.CreateIdentifier(year, day);
        ConsoleWindowHelper.ShowInGuiWindow(windowIdentifier, filePath);

        // Part 1
        var watch = Stopwatch.StartNew(); // Start timing for Part 1.
        var part1Result = instance.Part1(input);
        watch.Stop();
        var part1Time = watch.ElapsedTicks;
        results.Add((year, day, 1, part1Result.ToString(), FormatTime(part1Time))); // Add Part 1 results to the table.

        RenderTable(results, part1Time); // Render the table with Part 1 results.

        // Part 2
        long part2Time = 0;
        string part2Result;
        try
        {
            watch.Restart(); // Restart timing for Part 2.
            part2Result = instance.Part2(input).ToString(); // Run Part 2.
            watch.Stop();
            part2Time = watch.ElapsedTicks; // Record runtime.
        }
        catch (NotImplementedException)
        {
            part2Result = "N/A"; // Handle cases where Part 2 is not implemented.
        }

        results.Add((year, day, 2, part2Result, FormatTime(part2Time))); // Add Part 2 results to the table.
        RenderTable(results, part1Time + part2Time); // Re-render the table with both parts.
    }

    /// <summary>
    ///     Renders the results table in the console.
    /// </summary>
    /// <param name="results">The results to display.</param>
    /// <param name="totalTimeTicks">The total runtime for all parts.</param>
    private static void RenderTable(IEnumerable<(int Year, int Day, int Part, string Result, string Runtime)> results,
        long totalTimeTicks)
    {
        Console.Clear(); // Clear the console to redraw the table.

        var valueTuples = results as (int Year, int Day, int Part, string Result, string Runtime)[] ??
                          results.ToArray();
        var resultColumnWidth =
            Math.Max(valueTuples.Length != 0 ? valueTuples.Max(r => r.Result.Length) : 6,
                6); // Ensure minimum column width.
        Console.WriteLine($"| Year  | Day | Part | Result{new string(' ', resultColumnWidth - 6)} | Runtime         |");
        Console.WriteLine($"|-------|-----|------|-{new string('-', resultColumnWidth)}-|-----------------|");

        foreach (var (year, day, part, result, runtime) in valueTuples)
            Console.WriteLine(
                $"| {year,4}  | {day,3} | {part,4} | {result.PadRight(resultColumnWidth)} | {runtime,15} |");

        // Add a total row
        var totalRuntime = FormatTime(totalTimeTicks);
        Console.WriteLine(
            $"| Total |     |      | {new string(' ', resultColumnWidth)} | {totalRuntime,15} |");
    }

    /// <summary>
    ///     Gets the latest year available in the solution.
    /// </summary>
    private static int GetLatestYear()
    {
        return GetAvailableYears().Max();
    }

    /// <summary>
    ///     Retrieves all available years based on namespaces.
    /// </summary>
    private static IEnumerable<int> GetAvailableYears()
    {
        return LocalAssembly.GetTypes()
            .Where(t => t.Namespace?.StartsWith("Solutions._") ==
                        true) // Look for namespaces starting with "Solutions._".
            .Select(t => int.Parse(t.Namespace!.Split('_')[1])) // Extract the year from the namespace.
            .Distinct(); // Ensure unique years.
    }

    /// <summary>
    ///     Gets the last available day for a given year.
    /// </summary>
    /// <param name="year">The year to check.</param>
    private static int GetLastDayForYear(int year)
    {
        var types = GetTypesInNamespace(LocalAssembly, $"Solutions._{year}"); // Get all types for the year.
        return types.Where(t => typeof(IBaseDay).IsAssignableFrom(t)) // Filter types that implement BaseDay.
            .Select(t => int.Parse(t.Name[3..])) // Extract the day number from the type name.
            .DefaultIfEmpty(0) // Default to 0 if no days are found.
            .Max(); // Get the highest day number.
    }

    /// <summary>
    ///     Retrieves all types in a specified namespace that implement BaseDay.
    /// </summary>
    /// <param name="assembly">The assembly to search.</param>
    /// <param name="nameSpace">The namespace to search within.</param>
    private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
        return assembly.GetTypes()
            .Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal) &&
                        typeof(IBaseDay).IsAssignableFrom(t))
            .ToArray();
    }

    /// <summary>
    ///     Ensures that the input data for a specific day exists.
    /// </summary>
    /// <param name="assembly">The assembly to base the path on.</param>
    /// <param name="year">The year of the day.</param>
    /// <param name="day">The day to check.</param>
    private static Task<string> PuzzleInputForDayExist(Assembly assembly, int year, int day)
    {
        var path = Path.Combine(Path.GetDirectoryName(assembly.Location)!);
        return DownloadInput.ForYear(year, day, path); // Download input if not already present.
    }

    /// <summary>
    ///     Formats elapsed time in an appropriate unit (nanoseconds, microseconds, milliseconds, seconds).
    /// </summary>
    /// <param name="ticks">The elapsed time in ticks.</param>
    private static string FormatTime(long ticks)
    {
        var nsPerTick = 1_000_000_000.0 / Stopwatch.Frequency; // Nanoseconds per tick.
        var totalNs = ticks * nsPerTick; // Total elapsed time in nanoseconds.

        return totalNs switch
        {
            >= 1_000_000_000 => $"{totalNs / 1_000_000_000:F2} s", // Seconds
            >= 1_000_000 => $"{totalNs / 1_000_000:F2} ms", // Milliseconds
            >= 1_000 => $"{totalNs / 1_000:F2} μs", // Microseconds
            _ => $"{totalNs:F2} ns" // Nanoseconds
        };
    }
}