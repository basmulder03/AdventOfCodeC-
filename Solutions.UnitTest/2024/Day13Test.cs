using Core.UnitTest;
using Shouldly;
using Solutions._2024;

namespace Solutions.UnitTest._2024;

[TestClass]
public class Day13Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day13();
        var data = FileStreamHelper.GetFileStream("");
        var result = day.Part1(data);
        result.ShouldBe(0);
    }
    
    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day13();
        var data = FileStreamHelper.GetFileStream("");
        var result = day.Part2(data);
        result.ShouldBe(0);
    }
}