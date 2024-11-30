using Core.UnitTest;
using Shouldly;
using Solutions._2015;

namespace Solutions.UnitTest._2015;

[TestClass]
public class Day10Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day10();
        var data = FileStreamHelper.GetFileStream("1");
        var result = day.Part1(data);
        result.ShouldBe(11);
    }
    
    [TestMethod]
    public void Part1Test2()
    {
        var day = new Day10();
        var data = FileStreamHelper.GetFileStream("11");
        var result = day.Part1(data);
        result.ShouldBe(21);
    }
    
    [TestMethod]
    public void Part1Test3()
    {
        var day = new Day10();
        var data = FileStreamHelper.GetFileStream("21");
        var result = day.Part1(data);
        result.ShouldBe(1211);
    }
    
    [TestMethod]
    public void Part1Test4()
    {
        var day = new Day10();
        var data = FileStreamHelper.GetFileStream("1211");
        var result = day.Part1(data);
        result.ShouldBe(111221);
    }
    
    [TestMethod]
    public void Part1Test5()
    {
        var day = new Day10();
        var data = FileStreamHelper.GetFileStream("111221");
        var result = day.Part1(data);
        result.ShouldBe(312211);
    }
}