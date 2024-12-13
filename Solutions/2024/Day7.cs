using Core.DataHelper;
using Core.Interfaces;
using Core.StringHelpers;

namespace Solutions._2024;

public class Day7 : BaseDay
{
    public long Part1(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
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

    public long Part2(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
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