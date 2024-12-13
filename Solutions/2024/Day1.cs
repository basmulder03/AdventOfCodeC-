﻿using Core.DataHelper;
using Core.Interfaces;

namespace Solutions._2024;

public class Day1 : BaseDay
{
    public long Part1(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var listA = new List<int>();
        var listB = new List<int>();

        foreach (var split in lines.Select(line => line.Split("   ")))
        {
            listA.Add(int.Parse(split[0]));
            listB.Add(int.Parse(split[1]));
        }

        listA = listA.OrderBy(x => x).ToList();
        listB = listB.OrderBy(x => x).ToList();

        var result = listA.Select((t, i) => Math.Abs(t - listB[i])).Sum();

        return result;
    }

    public long Part2(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var listA = new List<int>();
        var listB = new List<int>();

        foreach (var split in lines.Select(line => line.Split("   ")))
        {
            listA.Add(int.Parse(split[0]));
            listB.Add(int.Parse(split[1]));
        }

        listA = listA.OrderBy(x => x).ToList();

        var listBGroup = listB.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

        var result = 0;
        foreach (var t in listA)
        {
            if (!listBGroup.TryGetValue(t, out var group)) continue;

            result += t * group;
        }

        return result;
    }
}