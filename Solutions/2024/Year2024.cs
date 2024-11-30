using Core;

namespace Solutions._2024.Data;

public class Year2024 : IYear
{
    private readonly IDictionary<int, IDay> _days = new Dictionary<int, IDay>
    {
        {1, new Day1()}
    };
    
    public void RunAll()
    {
        throw new NotImplementedException();
    }

    public void RunDay(int day)
    {
        throw new NotImplementedException();
    }
}