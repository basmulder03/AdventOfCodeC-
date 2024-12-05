using Core;
using Core.DataHelper;

namespace Solutions._2015;

public class Day1 : IDay
{
    public int Part1(FileStream fileStream)
    {
        var chars = fileStream.ReadSingleLineAsChars();
        var floor = 0;
        foreach (var c in chars)
        {
            switch (c)
            {
                case '(':
                    floor++;
                    break;
                case ')':
                    floor--;
                    break;
            }
        }

        return floor;
    }

    public int Part2(FileStream fileStream)
    {
        var chars = fileStream.ReadSingleLineAsChars();
        var floor = 0;
        for (var i = 0; i < chars.Count; i++)
        {
            switch (chars[i])
            {
                case '(':
                    floor++;
                    break;
                case ')':
                    floor--;
                    break;
            }

            if (floor == -1)
            {
                return i + 1;
            }
        }

        return -1;
    }
}