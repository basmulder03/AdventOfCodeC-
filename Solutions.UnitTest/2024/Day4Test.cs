using Core.UnitTest;
using Shouldly;
using Solutions._2024;

namespace Solutions.UnitTest._2024;

[TestClass]
public class Day4Test
{
    [TestMethod]
    public void Part2Test1()
    {
        var day = new Day4();
        var data = FileStreamHelper.GetFileStream("""
                                                  .M.S......
                                                  ..A..MSMS.
                                                  .M.S.MAA..
                                                  ..A.ASMSM.
                                                  .M.S.M....
                                                  ..........
                                                  S.S.S.S.S.
                                                  .A.A.A.A..
                                                  M.M.M.M.M.
                                                  ..........
                                                  """);
        var result = day.Part2(data);
        result.ShouldBe("9");
    }
}