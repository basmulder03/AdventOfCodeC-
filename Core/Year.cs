using System.Reflection;
using Core.InputDownloader;

namespace Core;

public abstract class Year
{
    public abstract Task RunAll();

    public abstract Task RunDay(int day);

    protected static async Task Run(Assembly assembly, int year, int day)
    {
        var types = GetTypesInNamespace(assembly, $"Solutions._{year}");
        var type = types.FirstOrDefault(t => t.Name == $"Day{day}");
        if (type == null)
        {
            Console.WriteLine($"Day {day} not found for year {year}");
            return;
        }

        var instance = (IDay)Activator.CreateInstance(type)!;
        await PuzzleInputForDayExist(assembly, year, day);
        instance.Run($"./{year}/Data/Day{day}");
    }

    protected static async Task RunAllInYear(Assembly assembly, int year)
    {
        var types = GetTypesInNamespace(assembly, $"Solutions._{year}")
            .Where(t => t.GetInterfaces().Contains(typeof(IDay)))
            .OrderBy(t => t.Name);
        foreach (var type in types)
        {
            var instance = (IDay)Activator.CreateInstance(type)!;
            await PuzzleInputForDayExist(assembly, year, int.Parse(type.Name[3..]));
            instance.Run($"./{year}/Data/{type.Name}");
        }
    }

    private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
        return assembly.GetTypes()
            .Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
            .ToArray();
    }

    private static Task PuzzleInputForDayExist(Assembly assembly, int year, int day)
    {
        // Base the start of the path on the available assembly information
        var path = Path.Combine(Path.GetDirectoryName(assembly.Location)!, year.ToString(), "Data", $"Day{day}");
        return DownloadInput.ForYear(year, day, path);
    }
}