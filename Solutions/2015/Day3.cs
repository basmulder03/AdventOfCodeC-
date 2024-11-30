using Core;
using Core.DataHelper;

namespace Solutions._2015;

public class Day3 : IDay
{
    public long Part1(FileStream fileStream)
    {
        var chars = fileStream.ReadSingleLineAsChars();
        
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

    public long Part2(FileStream fileStream)
    {
        var chars = fileStream.ReadSingleLineAsChars();
        
        var visited = new HashSet<(int, int)>();
        var santaX = 0;
        var santaY = 0;
        var roboX = 0;
        var roboY = 0;
        visited.Add((santaX, santaY));
        
        for (var i = 0; i < chars.Count; i++)
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