using Core.UnitTest;
using Shouldly;
using Solutions._2015;

namespace Solutions.UnitTest._2015;

[TestClass]
public class Day9Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day9();
        var data = FileStreamHelper.GetFileStream("""
                                                  London to Dublin = 464
                                                  London to Belfast = 518
                                                  Dublin to Belfast = 141
                                                  """);
        var result = day.Part1(data);
        result.ShouldBe("605");
    }
    
    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day9();
        var data = FileStreamHelper.GetFileStream("""
                                                  London to Dublin = 464
                                                  London to Belfast = 518
                                                  Dublin to Belfast = 141
                                                  """);
        var result = day.Part2(data);
        result.ShouldBe("982");
    }
}