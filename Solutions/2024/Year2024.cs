using Core;

namespace Solutions._2024.Data;

public class Year2024 : IYear
{
    private readonly IDictionary<int, IDay> _days = new Dictionary<int, IDay>
    {
        {1, new Day1()},
        {2, new Day2()}
    };
    private const string PathPrefix = "./2024/Data/";
    
    public void RunAll()
    {
        
    }

    public void RunDay(int day)
    {
        _days[day].Run($"{PathPrefix}Day{day}");
    }
}