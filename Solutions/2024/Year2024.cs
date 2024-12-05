using System.Reflection;
using Core;

namespace Solutions._2024;

public class Year2024 : Year
{
    private static readonly Assembly LocalAssembly = Assembly.GetExecutingAssembly();
    
    public override void RunAll()
    {
        RunAllInYear(LocalAssembly, 2024);
    }

    public override void RunDay(int day)
    {
        Run(LocalAssembly, 2024, day);
    }
}