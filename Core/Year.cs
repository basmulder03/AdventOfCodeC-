using System.Reflection;

namespace Core;

public abstract class Year
{
    public abstract void RunAll();

    public abstract void RunDay(int day);

    protected static void Run(Assembly assembly, int year, int day)
    {
        var types = GetTypesInNamespace(assembly, $"Solutions._{year}");
        var type = types.FirstOrDefault(t => t.Name == $"Day{day}");
        if (type == null)
        {
            Console.WriteLine($"Day {day} not found for year {year}");
            return;
        }

        var instance = (IDay)Activator.CreateInstance(type)!;
        instance.Run($"./{year}/Data/Day{day}");
    }

    protected static void RunAllInYear(Assembly assembly, int year)
    {
        var types = GetTypesInNamespace(assembly, $"Solutions._{year}")
            .Where(t => t.GetInterfaces().Contains(typeof(IDay)))
            .OrderBy(t => t.Name);
        foreach (var type in types)
        {
            var instance = (IDay)Activator.CreateInstance(type)!;
            instance.Run($"./{year}/Data/{type.Name}");
        }
    }

    private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
        return assembly.GetTypes()
            .Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
            .ToArray();
    }
}