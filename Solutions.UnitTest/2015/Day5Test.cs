using Shouldly;
using Solutions._2015;

namespace Solutions.UnitTest._2015;

[TestClass]
public class Day5Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day5();
        const string data = "ugknbfddgicrmopn";
        var result = day.Part1(data);
        result.ShouldBe(1);
    }

    [TestMethod]
    public void Part1Test2()
    {
        var day = new Day5();
        const string data = "aaa";
        var result = day.Part1(data);
        result.ShouldBe(1);
    }

    [TestMethod]
    public void Part1Test3()
    {
        var day = new Day5();
        const string data = "jchzalrnumimnmhp";
        var result = day.Part1(data);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void Part1Test4()
    {
        var day = new Day5();
        const string data = "haegwjzuvuyypxyu";
        var result = day.Part1(data);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void Part1Test5()
    {
        var day = new Day5();
        const string data = "dvszwmarrgswjxmb";
        var result = day.Part1(data);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day5();
        const string data = "qjhvhtzxzqqjkmpb";
        var result = day.Part2(data);
        result.ShouldBe(1);
    }

    [TestMethod]
    public void Part2Test2()
    {
        var day = new Day5();
        const string data = "xxyxx";
        var result = day.Part2(data);
        result.ShouldBe(1);
    }

    [TestMethod]
    public void Part2Test3()
    {
        var day = new Day5();
        const string data = "uurcxstgmygtbstg";
        var result = day.Part2(data);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void Part2Test4()
    {
        var day = new Day5();
        const string data = "ieodomkazucvgmuy";
        var result = day.Part2(data);
        result.ShouldBe(0);
    }
}