using Core.UnitTest;
using Shouldly;
using Solutions._2015;

namespace Solutions.UnitTest._2015;

[TestClass]
public class Day8Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day8();
        var data = FileStreamHelper.GetFileStream("\"\"");
        var result = day.Part1(data);
        result.ShouldBe(2);
    }

    [TestMethod]
    public void Part1Test2()
    {
        var day = new Day8();
        var data = FileStreamHelper.GetFileStream("\"abc\"");
        var result = day.Part1(data);
        result.ShouldBe(2);
    }

    [TestMethod]
    public void Part1Test3()
    {
        var day = new Day8();
        var data = FileStreamHelper.GetFileStream("\"aaa\\\"aaa\"");
        var result = day.Part1(data);
        result.ShouldBe(3);
    }

    [TestMethod]
    public void Part1Test4()
    {
        var day = new Day8();
        var data = FileStreamHelper.GetFileStream("\"\\x27\"");
        var result = day.Part1(data);
        result.ShouldBe(5);
    }

    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day8();
        var data = FileStreamHelper.GetFileStream("\"\"");
        var result = day.Part2(data);
        result.ShouldBe(4);
    }

    [TestMethod]
    public void Part2Test2()
    {
        var day = new Day8();
        var data = FileStreamHelper.GetFileStream("\"abc\"");
        var result = day.Part2(data);
        result.ShouldBe(4);
    }

    [TestMethod]
    public void Part2Test3()
    {
        var day = new Day8();
        var data = FileStreamHelper.GetFileStream("\"aaa\\\"aaa\"");
        var result = day.Part2(data);
        result.ShouldBe(6);
    }

    [TestMethod]
    public void Part2Test4()
    {
        var day = new Day8();
        var data = FileStreamHelper.GetFileStream("\"\\x27\"");
        var result = day.Part2(data);
        result.ShouldBe(5);
    }
}