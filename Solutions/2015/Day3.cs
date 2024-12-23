using Core.Interfaces;

namespace Solutions._2015;

public class Day3 : BaseDay
{
    public override long Part1(string input)
    {
        var chars = input.ToCharArray();

        var visited = new HashSet<(int, int)>();
        var x = 0;
        var y = 0;
        visited.Add((x, y));

        foreach (var c in chars)
        {
            switch (c)
            {
                case '^':
                    y++;
                    break;
                case 'v':
                    y--;
                    break;
                case '>':
                    x++;
                    break;
                case '<':
                    x--;
                    break;
            }

            visited.Add((x, y));
        }

        return visited.Count;
    }

    public override long Part2(string input)
    {
        var chars = input.ToCharArray();

        var visited = new HashSet<(int, int)>();
        var santaX = 0;
        var santaY = 0;
        var roboX = 0;
        var roboY = 0;
        visited.Add((santaX, santaY));

        for (var i = 0; i < chars.Length; i++)
        {
            var c = chars[i];
            var x = i % 2 == 0 ? santaX : roboX;
            var y = i % 2 == 0 ? santaY : roboY;

            switch (c)
            {
                case '^':
                    y++;
                    break;
                case 'v':
                    y--;
                    break;
                case '>':
                    x++;
                    break;
                case '<':
                    x--;
                    break;
            }

            visited.Add((x, y));

            if (i % 2 == 0)
            {
                santaX = x;
                santaY = y;
            }
            else
            {
                roboX = x;
                roboY = y;
            }
        }

        return visited.Count;
    }
}