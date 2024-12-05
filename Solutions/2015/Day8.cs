using System.Text.RegularExpressions;
using Core;
using Core.DataHelper;

namespace Solutions._2015;

public class Day8 : IDay
{
    public int Part1(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var total = lines.Sum(line => line.Length);
        var memory = lines.Sum(line => Regex.Unescape(line).Length - 2);
        return total - memory;
    }

    public int Part2(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        return lines.Select(s => new
        {
            Original = s,
            Escaped = "\"" + s.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\""
        })
        .Sum(s => s.Escaped.Length - s.Original.Length);
    }
}