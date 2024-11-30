using Core;

namespace Solutions._2015;

public class Year2015: IYear
{
    private readonly IDictionary<int, IDay> _days = new Dictionary<int, IDay>
    {
        {1, new Day1()},
        {2, new Day2()},
        {3, new Day3()},
        {4, new Day4()},
        {5, new Day5()},
        {6, new Day6()},
        {7, new Day7()},
        {8, new Day8()},
        {9, new Day9()},
        {10, new Day10()}
    };

    private const string PathPrefix = "./2015/Data/";

    public void RunAll()
    {
        _days.Keys.ToList().ForEach(day => _days[day].Run(PathPrefix + $"Day{day}"));
    }
    
    public void RunDay(int day)
    {
        _days[day].Run(PathPrefix + $"Day{day}");
    }
}