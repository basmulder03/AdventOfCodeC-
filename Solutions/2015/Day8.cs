using System.Text.RegularExpressions;
using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2015;

public class Day8 : BaseDay
{
    public override long Part1(string input)
    {
        var lines = input.ReadLines();
        var total = lines.Sum(line => line.Length);
        var memory = lines.Sum(line => Regex.Unescape(line).Length - 2);
        return total - memory;
    }

    public override long Part2(string input)
    {
        var lines = input.ReadLines();
        return lines.Select(s => new
            {
                Original = s,
                Escaped = "\"" + s.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\""
            })
            .Sum(s => s.Escaped.Length - s.Original.Length);
    }
}