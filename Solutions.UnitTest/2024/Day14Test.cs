using Shouldly;
using Solutions._2024;

namespace Solutions.UnitTest._2024;

[TestClass]
public class Day14Test
{
    [TestInitialize]
    public void Initialize()
    {
        Day14.GridWidth = 11;
        Day14.GridHeight = 7;
    }

    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day14();
        const string data = """
                            p=0,4 v=3,-3
                            p=6,3 v=-1,-3
                            p=10,3 v=-1,2
                            p=2,0 v=2,-1
                            p=0,0 v=1,3
                            p=3,0 v=-2,-2
                            p=7,6 v=-1,-3
                            p=3,0 v=-1,-2
                            p=9,3 v=2,3
                            p=7,3 v=-1,2
                            p=2,4 v=2,-3
                            p=9,5 v=-3,-3
                            """;
        var result = day.Part1(data);
        result.ShouldBe(12);
    }
}