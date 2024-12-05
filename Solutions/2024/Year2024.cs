using System.Reflection;
using Core;

namespace Solutions._2024;

public class Year2024 : Year
{
    private static readonly Assembly LocalAssembly = Assembly.GetExecutingAssembly();
    
    public override async Task RunAll()
    {
        await RunAllInYear(LocalAssembly, 2024);
    }

    public override async Task RunDay(int day)
    {
        await Run(LocalAssembly, 2024, day);
    }
}