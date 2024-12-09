using Core.UnitTest;
using Shouldly;
using Solutions._2015;

namespace Solutions.UnitTest._2015;

[TestClass]
public class Day6Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day6();
        var data = FileStreamHelper.GetFileStream("turn on 0,0 through 999,999");
        var result = day.Part1(data);
        result.ShouldBe(1000000);
    }

    [TestMethod]
    public void Part1Test2()
    {
        var day = new Day6();
        var data = FileStreamHelper.GetFileStream("toggle 0,0 through 999,0");
        var result = day.Part1(data);
        result.ShouldBe(1000);
    }

    [TestMethod]
    public void Part1Test3()
    {
        var day = new Day6();
        var data = FileStreamHelper.GetFileStream("turn off 499,499 through 500,500");
        var result = day.Part1(data);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day6();
        var data = FileStreamHelper.GetFileStream("turn on 0,0 through 0,0");
        var result = day.Part2(data);
        result.ShouldBe(1);
    }

    [TestMethod]
    public void Part2Test2()
    {
        var day = new Day6();
        var data = FileStreamHelper.GetFileStream("toggle 0,0 through 999,999");
        var result = day.Part2(data);
        result.ShouldBe(2000000);
    }
}