using Core;
using Core.DataHelper;
using Core.Extensions;

namespace Solutions._2024;

public class Day13 : BaseDay
{
    public long Part1(FileStream fileStream)
    {
        return Solve(fileStream, 0);
    }

    public long Part2(FileStream fileStream)
    {
        return Solve(fileStream, 10_000_000_000_000);
    }

    private static long Solve(FileStream fileStream, long offset)
    {
        var lines = fileStream.ReadLines();
        var totalTokens = 0L;
        // ReSharper disable InconsistentNaming
        int buttonAX = 0, buttonAY = 0, buttonBX = 0, buttonBY = 0;

        foreach (var line in lines)
        {
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
                var denominator = (buttonAX * buttonBY - buttonAY * buttonBX);
                var a = (prizeX * buttonBY - prizeY * buttonBX) / (double)denominator;
                var b = (prizeY * buttonAX - prizeX * buttonAY) / (double)denominator;

                // Check if a and b are integers and update total tokens
                // ReSharper disable CompareOfFloatsByEqualityOperator
                if (a == a.ToLong() && b == b.ToLong())
                {
                    totalTokens += (3 * a + b).ToLong();
                }
            }
        }

        return totalTokens;
    }
}