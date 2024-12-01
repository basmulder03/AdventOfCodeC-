using Core;
using Core.DataHelper;

namespace Solutions._2024;

public class Day1 : IDay
{
    public string Part1(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var listA = new List<int>();
        var listB = new List<int>();
        
        foreach (var line in lines)
        {
            var split = line.Split("   ");
            listA.Add(int.Parse(split[0]));
            listB.Add(int.Parse(split[1]));
        }
        
        listA = listA.OrderBy(x => x).ToList();
        listB = listB.OrderBy(x => x).ToList();

        var result = 0;
        for (var i = 0; i < listA.Count; i++)
        {
            result += Math.Abs(listA[i] - listB[i]);
        }

        return result.ToString();
    }

    public string Part2(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var listA = new List<int>();
        var listB = new List<int>();
        
        foreach (var line in lines)
        {
            var split = line.Split("   ");
            listA.Add(int.Parse(split[0]));
            listB.Add(int.Parse(split[1]));
        }
        
        listA = listA.OrderBy(x => x).ToList();
        
        var listBGroup = listB.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

        var result = 0;
        for (var i = 0; i < listA.Count; i++)
        {
            listBGroup.TryGetValue(listA[i], out var group);
            if (group == null)
            {
                continue;
            }
            
            result += listA[i] * group;
        }

        return result.ToString();
    }
}