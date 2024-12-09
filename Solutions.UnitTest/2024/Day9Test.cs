using Core.UnitTest;
using Shouldly;
using Solutions._2024;

namespace Solutions.UnitTest._2024;

[TestClass]
public class Day9Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day9();
        var data = FileStreamHelper.GetFileStream("2333133121414131402");
        var result = day.Part1(data);
        result.ShouldBe(1928);
    }
    
    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day9();
        var data = FileStreamHelper.GetFileStream("2333133121414131402");
        var result = day.Part2(data);
        result.ShouldBe(2858);
    }
}