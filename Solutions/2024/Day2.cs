using Core;
using Core.DataHelper;

namespace Solutions._2024;

public class Day2 : IDay
{
    public string Part1(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var safeReports = 0;

        foreach (var line in lines)
        {
            var levels = line.Split(" ").Select(int.Parse).ToList();
            if (IsSafe(levels))
            {
                safeReports++;
            }
        }

        return safeReports.ToString();
    }

    public string Part2(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var safeReports = 0;

        foreach (var line in lines)
        {
            var levels = line.Split(" ").Select(int.Parse).ToList();
            if (IsSafe(levels) || CanBeMadeSafeByRemovingOne(levels))
            {
                safeReports++;
            }
        }

        return safeReports.ToString();
    }

    private bool IsSafe(List<int> levels)
    {
        var isIncreasing = levels[1] > levels[0];
        for (int i = 1; i < levels.Count; i++)
        {
            if ((isIncreasing && levels[i] < levels[i - 1]) || (!isIncreasing && levels[i] > levels[i - 1]))
            {
                return false;
            }
            if (Math.Abs(levels[i] - levels[i - 1]) > 3 || Math.Abs(levels[i] - levels[i - 1]) < 1)
            {
                return false;
            }
        }
        return true;
    }

    private bool CanBeMadeSafeByRemovingOne(List<int> levels)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            var newLevels = levels.Take(i).Concat(levels.Skip(i + 1)).ToList();
            if (IsSafe(newLevels))
            {
                return true;
            }
        }
        return false;
    }
}