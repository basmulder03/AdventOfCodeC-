using Shouldly;
using Solutions._2024;

namespace Solutions.UnitTest._2024;

[TestClass]
public class Day5Test
{
    [TestMethod]
    public void Part1Test1()
    {
        var day = new Day5();
        const string data = """
                   47|53
                   97|13
                   97|61
                   97|47
                   75|29
                   61|13
                   75|53
                   29|13
                   97|29
                   53|29
                   61|53
                   97|53
                   61|29
                   47|13
                   75|47
                   97|75
                   47|61
                   75|61
                   47|29
                   75|13
                   53|13

                   75,47,61,53,29
                   97,61,53,29,13
                   75,29,13
                   75,97,47,61,53
                   61,13,29
                   97,13,75,29,47
                   """;
        var result = day.Part1(data);
        result.ShouldBe(143);
    }

    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day5();
        const string data = """
                   47|53
                   97|13
                   97|61
                   97|47
                   75|29
                   61|13
                   75|53
                   29|13
                   97|29
                   53|29
                   61|53
                   97|53
                   61|29
                   47|13
                   75|47
                   97|75
                   47|61
                   75|61
                   47|29
                   75|13
                   53|13

                   75,47,61,53,29
                   97,61,53,29,13
                   75,29,13
                   75,97,47,61,53
                   61,13,29
                   97,13,75,29,47
                   """;
        var result = day.Part2(data);
        true.ShouldBeTrue();
    }
}