using Core.UnitTest;
using Shouldly;
using Solutions._2024;

namespace Solutions.UnitTest._2024;

[TestClass]
public class Day6Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day6();
        var data = FileStreamHelper.GetFileStream("""
                                                  ....#.....
                                                  .........#
                                                  ..........
                                                  ..#.......
                                                  .......#..
                                                  ..........
                                                  .#..^.....
                                                  ........#.
                                                  #.........
                                                  ......#...
                                                  """);
        var result = day.Part1(data);
        result.ShouldBe(41);
    }

    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day6();
        var data = FileStreamHelper.GetFileStream("""
                                                  ....#.....
                                                  .........#
                                                  ..........
                                                  ..#.......
                                                  .......#..
                                                  ..........
                                                  .#..^.....
                                                  ........#.
                                                  #.........
                                                  ......#...
                                                  """);
        var result = day.Part2(data);
        result.ShouldBe(6);
    }
}