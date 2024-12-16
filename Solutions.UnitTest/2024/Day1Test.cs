using Shouldly;
using Solutions._2024;

namespace Solutions.UnitTest._2024;

[TestClass]
public class Day1Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day1();
        const string data = """
                   3   4
                   4   3
                   2   5
                   1   3
                   3   9
                   3   3
                   """;
        var result = day.Part1(data);
        result.ShouldBe(11);
    }

    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day1();
        const string data = """
                   3   4
                   4   3
                   2   5
                   1   3
                   3   9
                   3   3
                   """;
        var result = day.Part2(data);
        result.ShouldBe(31);
    }
}