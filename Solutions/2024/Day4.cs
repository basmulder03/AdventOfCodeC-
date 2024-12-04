using Core;
using Core.DataHelper;

namespace Solutions._2024;

public class Day4 : IDay
{
    public string Part1(FileStream fileStream)
    {
        var grid = Parse(fileStream.ReadLines());
        var xmasCounter = 0;

        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] == 'X')
                {
                    xmasCounter += FindWord(grid, x, y, "XMAS");
                }
            }
        }

        return xmasCounter.ToString();
    }

    public string Part2(FileStream fileStream)
    {
        var grid = Parse(fileStream.ReadLines());
        var xmasCounter = 0;

        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] == 'A')
                {
                    xmasCounter += FindCrossedWord(grid, x, y);
                }
            }
        }

        return xmasCounter.ToString();
    }

    private static char[][] Parse(List<string> lines)
    {
        var height = lines.Count;
        var result = new char[height][];

        for (var i = 0; i < height; i++)
        {
            result[i] = lines[i].ToCharArray();
        }

        return result;
    }

    private static int FindWord(char[][] grid, int x, int y, string word)
    {
        var directions = new (int x, int y)[]
        {
            (1, 0), (0, 1), (-1, 0), (0, -1), // Horizontal and vertical
            (1, 1), (1, -1), (-1, 1), (-1, -1) // Diagonal
        };
        var counter = 0;

        foreach (var (dx, dy) in directions)
        {
            var found = true;

            for (var i = 0; i < word.Length; i++)
            {
                var nx = x + dx * i;
                var ny = y + dy * i;

                if (nx < 0 || nx >= grid[0].Length || ny < 0 || ny >= grid.Length || grid[ny][nx] != word[i])
                {
                    found = false;
                    break;
                }
            }

            if (found)
            {
                counter++;
            }
        }

        return counter;
    }

    private static int FindCrossedWord(char[][] grid, int x, int y)
    {
        var counter = 0;
        var directions = new (int x, int y)[]
        {
            (1, 1), (1, -1), (-1, -1), (-1, 1) // Diagonal cross
        };

        foreach (var (dx, dy) in directions)
        {
            var ny = y + dy;
            var nx = x + dx;

            if (nx < 0 || nx >= grid[0].Length || ny < 0 || ny >= grid.Length || grid[ny][nx] != 'M')
            {
                continue;
            }

            var oppNy = y - dy;
            var oppNx = x - dx;

            if (oppNx < 0 || oppNx >= grid[0].Length || oppNy < 0 || oppNy >= grid.Length || grid[oppNy][oppNx] != 'S')
            {
                continue;
            }

            counter++;
        }

        return counter > 1 ? 1 : 0;
    }
}