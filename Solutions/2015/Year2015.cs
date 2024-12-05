using System.Reflection;
using Core;

namespace Solutions._2015;

public class Year2015: Year
{
    private static readonly Assembly LocalAssembly = Assembly.GetExecutingAssembly();
    
    public override async Task RunAll()
    {
        await RunAllInYear(LocalAssembly, 2015);
    }
    
    public override async Task RunDay(int day)
    {
        await Run(LocalAssembly, 2015, day);
    }
}