using Shouldly;
using Solutions._2024;

namespace Solutions.UnitTest._2024;

[TestClass]
public class Day11Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day11();
        const string data = "125 17";
        var result = day.Part1(data);
        result.ShouldBe(55312);
    }
}