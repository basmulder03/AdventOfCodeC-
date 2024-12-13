using System.Diagnostics;
using Core.DataHelper;

namespace Core;

public static class Runner
{
    public static void Run(this IDay day, string path)
    {
        var watch = Stopwatch.StartNew();

        var part1Result = day.Part1(ReadFile.GetFileStream(path));
        watch.Stop();
        var part1Time = watch.ElapsedMilliseconds;

        watch.Restart();
        long part2Result;
        try
        {
            part2Result = day.Part2(ReadFile.GetFileStream(path));
        }
        catch (NotImplementedException e)
        {
            part2Result = -1;
        }

        watch.Stop();
        var part2Time = watch.ElapsedMilliseconds;

        var partColumnWidth = Math.Max(part1Result.ToString().Length, part2Result.ToString().Length) + 2;
        var timeColumnWidth = Math.Max(part1Time.ToString().Length, part2Time.ToString().Length) + 4;

        Console.WriteLine(
            $"| Class Name{"".PadRight(day.GetType().FullName.Length)} | Part | Result{"".PadRight(partColumnWidth)} | Time (ms){"".PadRight(timeColumnWidth)} |");
        Console.WriteLine(
            $"|{"".PadRight(day.GetType().FullName.Length + 1, '-')}|------|{"".PadRight(partColumnWidth + 6, '-')}|{"".PadRight(timeColumnWidth + 10, '-')}|");
        var part1TimeFormatted = part1Time >= 1000 ? $"{part1Time / 1000.0:F2} s" : $"{part1Time} ms";
var part2TimeFormatted = part2Time >= 1000 ? $"{part2Time / 1000.0:F2} s" : $"{part2Time} ms";

Console.WriteLine(
    $"| {day.GetType().FullName} |  1   |  {part1Result.ToString().PadRight(partColumnWidth)} |   {part1TimeFormatted.PadRight(timeColumnWidth)} |");
Console.WriteLine(
    $"| {day.GetType().FullName} |  2   |  {part2Result.ToString().PadRight(partColumnWidth)} |   {part2TimeFormatted.PadRight(timeColumnWidth)} |");
    }
}