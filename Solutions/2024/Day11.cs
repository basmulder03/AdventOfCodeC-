using Core.DataHelper;
using Core.Interfaces;
using Core.StringHelpers;

namespace Solutions._2024;

public class Day11 : BaseDay
{
    public long Part1(FileStream fileStream)
    {
        var input = Parse(fileStream);
        return ApplyRules(input, 25);
    }

    public long Part2(FileStream fileStream)
    {
        var input = Parse(fileStream);
        return ApplyRules(input, 75);
    }

    private static List<long> Parse(FileStream fileStream)
    {
        return fileStream.ReadSingleLine().GetLongs().ToList();
    }

    private static long ApplyRules(List<long> stones, int amount)
    {
        var cache = stones.GroupBy(stone => stone)
                          .ToDictionary(group => group.Key, group => (long)group.Count());

        for (var i = 0; i < amount; i++)
        {
            var newStones = new Dictionary<long, long>();
            foreach (var (key, value) in cache)
            {
                if (key == 0)
                {
                    newStones[1] = newStones.GetValueOrDefault(1, 0) + value;
                }
                else if (key.ToString().Length % 2 == 0)
                {
                    var strKey = key.ToString();
                    var half = strKey.Length / 2;
                    var firstHalf = long.Parse(strKey[..half]);
                    var secondHalf = long.Parse(strKey[half..]);
                    newStones[firstHalf] = newStones.GetValueOrDefault(firstHalf, 0) + value;
                    newStones[secondHalf] = newStones.GetValueOrDefault(secondHalf, 0) + value;
                }
                else
                {
                    newStones[key * 2024] = newStones.GetValueOrDefault(key * 2024, 0) + value;
                }
            }
            cache = newStones;
        }

        return cache.Values.Sum();
    }
}