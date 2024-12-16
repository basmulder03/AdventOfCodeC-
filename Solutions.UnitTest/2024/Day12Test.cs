using Shouldly;
using Solutions._2024;

namespace Solutions.UnitTest._2024;

[TestClass]
public class Day12Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day12();
        const string data = """
                   RRRRIICCFF
                   RRRRIICCCF
                   VVRRRCCFFF
                   VVRCCCJFFF
                   VVVVCJJCFE
                   VVIVCCJJEE
                   VVIIICJJEE
                   MIIIIIJJEE
                   MIIISIJEEE
                   MMMISSJEEE
                   """;
        var result = day.Part1(data);
        result.ShouldBe(1930);
    }

    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day12();
        const string data = """
                   RRRRIICCFF
                   RRRRIICCCF
                   VVRRRCCFFF
                   VVRCCCJFFF
                   VVVVCJJCFE
                   VVIVCCJJEE
                   VVIIICJJEE
                   MIIIIIJJEE
                   MIIISIJEEE
                   MMMISSJEEE
                   """;
        var result = day.Part2(data);
        result.ShouldBe(1206);
    }

    [TestMethod]
    public void Part2Test2()
    {
        var day = new Day12();
        const string data = """
                   AAAA
                   BBCD
                   BBCC
                   EEEC
                   """;
        var result = day.Part2(data);
        result.ShouldBe(80);
    }

    [TestMethod]
    public void Part2Test3()
    {
        var day = new Day12();
        const string data = """
                   OOOOO
                   OXOXO
                   OOOOO
                   OXOXO
                   OOOOO
                   """;
        var result = day.Part2(data);
        result.ShouldBe(436);
    }

    [TestMethod]
    public void Part2Test4()
    {
        var day = new Day12();
        const string data = """
                   AAAAAA
                   AAABBA
                   AAABBA
                   ABBAAA
                   ABBAAA
                   AAAAAA
                   """;
        var result = day.Part2(data);
        result.ShouldBe(368);
    }
}