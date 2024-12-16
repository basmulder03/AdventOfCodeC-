using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2024;

public class Day2 : IBaseDay
{
    public long Part1(string input)
    {
        var lines = input.ReadLines();
        var safeReports = 0;

        foreach (var line in lines)
        {
            var levels = line.Split(" ").Select(int.Parse).ToList();
            if (IsSafe(levels)) safeReports++;
        }

        return safeReports;
    }

    public long Part2(string input)
    {
        var lines = input.ReadLines();
        var safeReports = lines.Select(line => line.Split(" ").Select(int.Parse).ToList())
            .Count(levels => IsSafe(levels) || CanBeMadeSafeByRemovingOne(levels));

        return safeReports;
    }

    private static bool IsSafe(List<int> levels)
    {
        var isIncreasing = levels[1] > levels[0];
        for (var i = 1; i < levels.Count; i++)
        {
            if ((isIncreasing && levels[i] < levels[i - 1]) || (!isIncreasing && levels[i] > levels[i - 1]))
                return false;
            if (Math.Abs(levels[i] - levels[i - 1]) > 3 || Math.Abs(levels[i] - levels[i - 1]) < 1) return false;
        }

        return true;
    }

    private static bool CanBeMadeSafeByRemovingOne(List<int> levels)
    {
        return levels.Select((t, i) => (List<int>)levels.Take(i).Concat(levels.Skip(i + 1)).ToList()).Any(IsSafe);
    }
}