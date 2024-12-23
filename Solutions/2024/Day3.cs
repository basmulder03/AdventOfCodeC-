using System.Text.RegularExpressions;
using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2024;

public class Day3 : BaseDay
{
    private static readonly Regex Part1Regex = new(@"mul\(\d+,\d+\)");
    private static readonly Regex Part2Regex = new(@"mul\(\d+,\d+\)|do\(\)|don't\(\)");

    public override long Part1(string input)
    {
        var result = ProcessLines(input, Part1Regex, true);
        return result;
    }

    public override long Part2(string input)
    {
        var result = ProcessLines(input, Part2Regex, false);
        return result;
    }

    private static int ProcessLines(string input, Regex regex, bool alwaysEnabled)
    {
        var lines = input.ReadLines();
        var result = 0;
        var enabled = true;

        foreach (var matchValue in from line in lines
                 select regex.Matches(line)
                 into matches
                 from Match match in matches
                 select match.Value)
        {
            if (!alwaysEnabled)
                switch (matchValue)
                {
                    case "do()":
                        enabled = true;
                        continue;
                    case "don't()":
                        enabled = false;
                        continue;
                }

            if (!enabled) continue;
            var numbers = matchValue[4..^1].Split(",");
            result += int.Parse(numbers[0]) * int.Parse(numbers[1]);
        }

        return result;
    }
}