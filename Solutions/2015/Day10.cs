using System.Text;
using Core;
using Core.DataHelper;

namespace Solutions._2015;

public class Day10 : IDay
{
    public long Part1(FileStream fileStream)
    {
        var line = fileStream.ReadSingleLine();
        var result = Solve(line, 40);
        return result;
    }

    public long Part2(FileStream fileStream)
    {
        var line = fileStream.ReadSingleLine();
        var result = Solve(line, 50);
        return result;
    }

    private int Solve(string line, int times)
    {
        var split = line.ToCharArray();
        var result = new StringBuilder();

        for (var j = 0; j < times; j++)
        {
            result.Clear();
            var lastChar = split[0];
            var count = 1;
            for (var i = 1; i < split.Length; i++)
            {
                var c = split[i];
                if (c == lastChar)
                {
                    count++;
                    continue;
                }

                result.Append($"{count}{lastChar}");
                lastChar = c;
                count = 1;
            }

            result.Append($"{count}{lastChar}");
            split = result.ToString().ToCharArray();
        }

        return result.Length;
    }
}