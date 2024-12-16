using Shouldly;
using Solutions._2015;

namespace Solutions.UnitTest._2015;

[TestClass]
public class Day2Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day2();
        const string data = "2x3x4";
        var result = day.Part1(data);
        result.ShouldBe(58);
    }

    [TestMethod]
    public void Part1Test2()
    {
        var day = new Day2();
        const string data = "1x1x10";
        var result = day.Part1(data);
        result.ShouldBe(43);
    }

    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day2();
        const string data = "2x3x4";
        var result = day.Part2(data);
        result.ShouldBe(34);
    }

    [TestMethod]
    public void Part2Test2()
    {
        var day = new Day2();
        const string data = "1x1x10";
        var result = day.Part2(data);
        result.ShouldBe(14);
    }
}