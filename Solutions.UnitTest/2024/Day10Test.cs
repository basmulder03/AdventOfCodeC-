using Core.UnitTest;
using Shouldly;
using Solutions._2024;

namespace Solutions.UnitTest._2024;

[TestClass]
public class Day10Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day10();
        var data = FileStreamHelper.GetFileStream("""
                                                  89010123
                                                  78121874
                                                  87430965
                                                  96549874
                                                  45678903
                                                  32019012
                                                  01329801
                                                  10456732
                                                  """);
        var result = day.Part1(data);
        result.ShouldBe(36);
    }

    [TestMethod]
    public void Part1Test2()
    {
        var day = new Day10();
        var data = FileStreamHelper.GetFileStream("""
                                                  5550555
                                                  5551555
                                                  5552555
                                                  6543456
                                                  7555557
                                                  8555558
                                                  9555559
                                                  """);
        var result = day.Part1(data);
        result.ShouldBe(2);
    }
    
    [TestMethod]
    public void Part1Test3()
    {
        var day = new Day10();
        var data = FileStreamHelper.GetFileStream("""
                                                  5590559
                                                  5551598
                                                  4562657
                                                  6543456
                                                  7655987
                                                  8764555
                                                  9875555
                                                  """);
        var result = day.Part1(data);
        result.ShouldBe(4);
    }

    [TestMethod]
    public void Part1Test4()
    {
        var day = new Day10();
        var data = FileStreamHelper.GetFileStream("""
                                                  1033933
                                                  2544833
                                                  3344734
                                                  4567654
                                                  4348453
                                                  4449452
                                                  4444401
                                                  """);
        var result = day.Part1(data);
        result.ShouldBe(3);
    }

    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day10();
        var data = FileStreamHelper.GetFileStream("""
                                                  2223202
                                                  2243213
                                                  2251222
                                                  2265433
                                                  2272242
                                                  2287652
                                                  2292222
                                                  """);
        var result = day.Part2(data);
        result.ShouldBe(3);
    }
}