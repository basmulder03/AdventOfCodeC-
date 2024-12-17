using Core.Extensions;
using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2024;

public class Day7 : IBaseDay
{
    public long Part1(string input)
    {
        var lines = input.ReadLines();
        var total = 0L;

        foreach (var line in lines)
        {
            var split = line.Split(": ");
            var sum = long.Parse(split[0]);
            var nums = split[1].GetLongs();

            var totals = new List<long> { nums[0] };

            foreach (var num in nums.Skip(1))
            {
                var newTotals = new List<long>();
                foreach (var totalValue in totals)
                {
                    newTotals.Add(Mult(totalValue, num));
                    newTotals.Add(Sum(totalValue, num));
                }

                totals = newTotals;
            }

            if (totals.Contains(sum)) total += sum;
        }

        return total;
    }

    public long Part2(string input)
    {
        var lines = input.ReadLines();
        var total = 0L;

        foreach (var line in lines)
        {
            var split = line.Split(": ");
            var sum = long.Parse(split[0]);
            var nums = split[1].GetLongs();

            var totals = new List<long> { nums[0] };

            foreach (var num in nums.Skip(1))
            {
                var newTotals = new List<long>();
                foreach (var totalValue in totals)
                {
                    newTotals.Add(Mult(totalValue, num));
                    newTotals.Add(Sum(totalValue, num));
                    newTotals.Add(Combine(totalValue, num));
                }

                totals = newTotals;
            }

            if (totals.Contains(sum)) total += sum;
        }

        return total;
    }

    private static long Mult(long a, long b)
    {
        return a * b;
    }

    private static long Sum(long a, long b)
    {
        return a + b;
    }

    private static long Combine(long a, long b)
    {
        return long.Parse($"{a}{b}");
    }
}