using Core.UnitTest;
using Shouldly;
using Solutions._2015;

namespace Solutions.UnitTest._2015;

[TestClass]
public class Day1Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day1();
        var data = FileStreamHelper.GetFileStream("(())");
        var result = day.Part1(data);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void Part1Test2()
    {
        var day = new Day1();
        var data = FileStreamHelper.GetFileStream("()()");
        var result = day.Part1(data);
        result.ShouldBe(0);
    }

    [TestMethod]
    public void Part1Test3()
    {
        var day = new Day1();
        var data = FileStreamHelper.GetFileStream("(((");
        var result = day.Part1(data);
        result.ShouldBe(3);
    }

    [TestMethod]
    public void Part1Test4()
    {
        var day = new Day1();
        var data = FileStreamHelper.GetFileStream("(()(()(");
        var result = day.Part1(data);
        result.ShouldBe(3);
    }

    [TestMethod]
    public void Part1Test5()
    {
        var day = new Day1();
        var data = FileStreamHelper.GetFileStream("))(((((");
        var result = day.Part1(data);
        result.ShouldBe(3);
    }

    [TestMethod]
    public void Part1Test6()
    {
        var day = new Day1();
        var data = FileStreamHelper.GetFileStream("())");
        var result = day.Part1(data);
        result.ShouldBe(-1);
    }

    [TestMethod]
    public void Part1Test7()
    {
        var day = new Day1();
        var data = FileStreamHelper.GetFileStream("))(");
        var result = day.Part1(data);
        result.ShouldBe(-1);
    }

    [TestMethod]
    public void Part1Test8()
    {
        var day = new Day1();
        var data = FileStreamHelper.GetFileStream(")))");
        var result = day.Part1(data);
        result.ShouldBe(-3);
    }

    [TestMethod]
    public void Part1Test9()
    {
        var day = new Day1();
        var data = FileStreamHelper.GetFileStream(")())())");
        var result = day.Part1(data);
        result.ShouldBe(-3);
    }

    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day1();
        var data = FileStreamHelper.GetFileStream(")");
        var result = day.Part2(data);
        result.ShouldBe(1);
    }

    [TestMethod]
    public void Part2Test2()
    {
        var day = new Day1();
        var data = FileStreamHelper.GetFileStream("()())");
        var result = day.Part2(data);
        result.ShouldBe(5);
    }
}