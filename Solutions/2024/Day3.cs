using System.Text.RegularExpressions;
using Core;
using Core.DataHelper;

namespace Solutions._2024;

public class Day3 : IDay
{
    private static readonly Regex Part1Regex = new(@"mul\(\d+,\d+\)");
    private static readonly Regex Part2Regex = new(@"mul\(\d+,\d+\)|do\(\)|don't\(\)");

    public int Part1(FileStream fileStream)
    {
        var result = ProcessLines(fileStream, Part1Regex, true);
        return result;
    }

    public int Part2(FileStream fileStream)
    {
        var result = ProcessLines(fileStream, Part2Regex, false);
        return result;
    }

    private static int ProcessLines(FileStream fileStream, Regex regex, bool alwaysEnabled)
    {
        var lines = fileStream.ReadLines();
        var result = 0;
        var enabled = true;

        foreach (var matchValue in from line in lines select regex.Matches(line) into matches from Match match in matches select match.Value)
        {
            if (!alwaysEnabled)
            {
                switch (matchValue)
                {
                    case "do()":
                        enabled = true;
                        continue;
                    case "don't()":
                        enabled = false;
                        continue;
                }
            }

            if (!enabled) continue;
            var numbers = matchValue[4..^1].Split(",");
            result += int.Parse(numbers[0]) * int.Parse(numbers[1]);
        }

        return result;
    }
}