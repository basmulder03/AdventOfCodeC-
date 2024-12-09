using Core.UnitTest;
using Shouldly;
using Solutions._2015;

namespace Solutions.UnitTest._2015;

[TestClass]
public class Day4Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day4();
        var data = FileStreamHelper.GetFileStream("abcdef");
        var result = day.Part1(data);
        result.ShouldBe(609043);
    }

    [TestMethod]
    public void Part1Test2()
    {
        var day = new Day4();
        var data = FileStreamHelper.GetFileStream("pqrstuv");
        var result = day.Part1(data);
        result.ShouldBe(1048970);
    }
}