using Shouldly;
using Solutions._2024;

namespace Solutions.UnitTest._2024;

[TestClass]
public class Day2Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day2();
        const string data = "7 6 4 2 1";
        var result = day.Part1(data);
        result.ShouldBe(1);
    }

    [TestMethod]
    public void Part1Test2()
    {
        var day = new Day2();
        const string data = "1 2 7 8 9";
        var result = day.Part1(data);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void Part1Test3()
    {
        var day = new Day2();
        const string data = "9 7 6 2 1";
        var result = day.Part1(data);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void Part1Test4()
    {
        var day = new Day2();
        const string data = "1 3 2 4 5";
        var result = day.Part1(data);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void Part1Test5()
    {
        var day = new Day2();
        const string data = "8 6 4 4 1";
        var result = day.Part1(data);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void Part1Test6()
    {
        var day = new Day2();
        const string data = "1 3 6 7 9";
        var result = day.Part1(data);
        result.ShouldBe(1);
    }

    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day2();
        const string data = "7 6 4 2 1";
        var result = day.Part2(data);
        result.ShouldBe(1);
    }

    [TestMethod]
    public void Part2Test2()
    {
        var day = new Day2();
        const string data = "1 2 7 8 9";
        var result = day.Part2(data);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void Part2Test3()
    {
        var day = new Day2();
        const string data = "9 7 6 2 1";
        var result = day.Part2(data);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void Part2Test4()
    {
        var day = new Day2();
        const string data = "1 3 2 4 5";
        var result = day.Part2(data);
        result.ShouldBe(1);
    }

    [TestMethod]
    public void Part2Test5()
    {
        var day = new Day2();
        const string data = "8 6 4 4 1";
        var result = day.Part2(data);
        result.ShouldBe(1);
    }

    [TestMethod]
    public void Part2Test6()
    {
        var day = new Day2();
        const string data = "1 3 6 7 9";
        var result = day.Part2(data);
        result.ShouldBe(1);
    }
}