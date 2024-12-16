using Core.Extensions;
using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2024;

public class Day13 : IBaseDay
{
    public long Part1(string input)
    {
        return Solve(input, 0);
    }

    public long Part2(string input)
    {
        return Solve(input, 10_000_000_000_000);
    }

    private static long Solve(string input, long offset)
    {
        var lines = input.ReadLines();
        var totalTokens = 0L;
        // ReSharper disable InconsistentNaming
        int buttonAX = 0, buttonAY = 0, buttonBX = 0, buttonBY = 0;

        foreach (var line in lines)
            if (line.StartsWith("Button"))
            {
                // Parse button coordinates
                var parts = line.Split(" ");
                var buttonType = parts[1].Split(":")[0];
                if (buttonType == "A")
                {
                    buttonAX = int.Parse(parts[2][2..^1]);
                    buttonAY = int.Parse(parts[3][2..]);
                }
                else
                {
                    buttonBX = int.Parse(parts[2][2..^1]);
                    buttonBY = int.Parse(parts[3][2..]);
                }
            }
            else if (line.StartsWith("Prize"))
            {
                // Parse prize coordinates and apply offset
                var parts = line.Split(" ");
                var prizeX = int.Parse(parts[1][2..^1]) + offset;
                var prizeY = int.Parse(parts[2][2..]) + offset;

                // Calculate coefficients a and b
                var denominator = buttonAX * buttonBY - buttonAY * buttonBX;
                var a = (prizeX * buttonBY - prizeY * buttonBX) / (double)denominator;
                var b = (prizeY * buttonAX - prizeX * buttonAY) / (double)denominator;

                // Check if a and b are integers and update total tokens
                // ReSharper disable CompareOfFloatsByEqualityOperator
                if (a == a.ToLong() && b == b.ToLong()) totalTokens += (3 * a + b).ToLong();
            }

        return totalTokens;
    }
}