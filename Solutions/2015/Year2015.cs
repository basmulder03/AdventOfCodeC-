using System.Reflection;
using Core;

namespace Solutions._2015;

public class Year2015: Year
{
    private static readonly Assembly LocalAssembly = Assembly.GetExecutingAssembly();
    
    public override void RunAll()
    {
        RunAllInYear(LocalAssembly, 2015);
    }
    
    public override void RunDay(int day)
    {
        Run(LocalAssembly, 2015, day);
    }
}