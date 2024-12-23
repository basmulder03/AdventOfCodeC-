using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2024;

public class Day1 : BaseDay
{
    public override long Part1(string input)
    {
        var lines = input.ReadLines();
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

    public override long Part2(string input)
    {
        var lines = input.ReadLines();
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