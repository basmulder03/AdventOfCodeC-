using Shouldly;
using Solutions._2015;

namespace Solutions.UnitTest._2015;

[TestClass]
public class Day3Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day3();
        const string data = ">";
        var result = day.Part1(data);
        result.ShouldBe(2);
    }

    [TestMethod]
    public void Part1Test2()
    {
        var day = new Day3();
        const string data = "^>v<";
        var result = day.Part1(data);
        result.ShouldBe(4);
    }

    [TestMethod]
    public void Part1Test3()
    {
        var day = new Day3();
        const string data = "^v^v^v^v^v";
        var result = day.Part1(data);
        result.ShouldBe(2);
    }

    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day3();
        const string data = "^v";
        var result = day.Part2(data);
        result.ShouldBe(3);
    }

    [TestMethod]
    public void Part2Test2()
    {
        var day = new Day3();
        const string data = "^>v<";
        var result = day.Part2(data);
        result.ShouldBe(3);
    }

    [TestMethod]
    public void Part2Test3()
    {
        var day = new Day3();
        const string data = "^v^v^v^v^v";
        var result = day.Part2(data);
        result.ShouldBe(11);
    }
}