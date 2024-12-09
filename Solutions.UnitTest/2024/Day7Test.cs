using Core.UnitTest;
using Shouldly;
using Solutions._2024;

namespace Solutions.UnitTest._2024;

[TestClass]
public class Day7Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day7();
        var data = FileStreamHelper.GetFileStream("""
                                                  190: 10 19
                                                  3267: 81 40 27
                                                  83: 17 5
                                                  156: 15 6
                                                  7290: 6 8 6 15
                                                  161011: 16 10 13
                                                  192: 17 8 14
                                                  21037: 9 7 18 13
                                                  292: 11 6 16 20
                                                  """);
        var result = day.Part1(data);
        result.ShouldBe(3749);
    }

    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day7();
        var data = FileStreamHelper.GetFileStream("""
                                                  190: 10 19
                                                  3267: 81 40 27
                                                  83: 17 5
                                                  156: 15 6
                                                  7290: 6 8 6 15
                                                  161011: 16 10 13
                                                  192: 17 8 14
                                                  21037: 9 7 18 13
                                                  292: 11 6 16 20
                                                  """);
        var result = day.Part2(data);
        result.ShouldBe(11387);
    }
}