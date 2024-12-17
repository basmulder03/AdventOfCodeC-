using Core.Extensions;
using Core.Interfaces;

namespace Solutions._2024;

public class Day11 : IBaseDay
{
    public long Part1(string input)
    {
        var parsedInput = Parse(input);
        return ApplyRules(parsedInput, 25);
    }

    public long Part2(string input)
    {
        var parsedInput = Parse(input);
        return ApplyRules(parsedInput, 75);
    }

    private static List<long> Parse(string input)
    {
        return input.GetLongs().ToList();
    }

    private static long ApplyRules(List<long> stones, int amount)
    {
        var cache = stones.GroupBy(stone => stone)
            .ToDictionary(group => group.Key, group => (long)group.Count());

        for (var i = 0; i < amount; i++)
        {
            var newStones = new Dictionary<long, long>();
            foreach (var (key, value) in cache)
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

            cache = newStones;
        }

        return cache.Values.Sum();
    }
}