using Core;
using Core.DataHelper;

namespace Solutions._2015;

public class Day6 : BaseDay
{
    public long Part1(FileStream fileStream)
    {
        var strings = fileStream.ReadLines();
        var grid = new bool[1000, 1000];

        foreach (var str in strings)
        {
            var parts = str.Split(' ');
            var start = parts[parts.Length - 3].Split(',');
            var end = parts[parts.Length - 1].Split(',');
            var action = parts[0];
            var x1 = int.Parse(start[0]);
            var y1 = int.Parse(start[1]);
            var x2 = int.Parse(end[0]);
            var y2 = int.Parse(end[1]);

            for (var x = x1; x <= x2; x++)
            for (var y = y1; y <= y2; y++)
                switch (action)
                {
                    case "turn":
                        if (parts[1] == "on")
                            grid[x, y] = true;
                        else
                            grid[x, y] = false;
                        break;
                    case "toggle":
                        grid[x, y] = !grid[x, y];
                        break;
                }
        }

        var count = 0;

        for (var x = 0; x < 1000; x++)
        for (var y = 0; y < 1000; y++)
            if (grid[x, y])
                count++;

        return count;
    }

    public long Part2(FileStream fileStream)
    {
        var strings = fileStream.ReadLines();
        var grid = new int[1000, 1000];

        foreach (var str in strings)
        {
            var parts = str.Split(' ');
            var start = parts[parts.Length - 3].Split(',');
            var end = parts[parts.Length - 1].Split(',');
            var action = parts[0];
            var x1 = int.Parse(start[0]);
            var y1 = int.Parse(start[1]);
            var x2 = int.Parse(end[0]);
            var y2 = int.Parse(end[1]);

            for (var x = x1; x <= x2; x++)
            for (var y = y1; y <= y2; y++)
                switch (action)
                {
                    case "turn":
                        if (parts[1] == "on")
                            grid[x, y]++;
                        else
                            grid[x, y] = Math.Max(0, grid[x, y] - 1);
                        break;
                    case "toggle":
                        grid[x, y] += 2;
                        break;
                }
        }

        var count = 0;

        for (var x = 0; x < 1000; x++)
        for (var y = 0; y < 1000; y++)
            count += grid[x, y];

        return count;
    }
}