﻿using Core;
using Core.DataHelper;

namespace Solutions._2015;

public class Day1 : IDay
{
    public string Part1(FileStream fileStream)
    {
        var chars = fileStream.ReadSingleLineAsChars();
        var floor = 0;
        foreach (var c in chars)
        {
            if (c == '(')
            {
                floor++;
            }
            else if (c == ')')
            {
                floor--;
            }
        }

        return floor.ToString();
    }

    public string Part2(FileStream fileStream)
    {
        var chars = fileStream.ReadSingleLineAsChars();
        var floor = 0;
        for (var i = 0; i < chars.Count; i++)
        {
            if (chars[i] == '(')
            {
                floor++;
            }
            else if (chars[i] == ')')
            {
                floor--;
            }

            if (floor == -1)
            {
                return (i + 1).ToString();
            }
        }

        return "Not Found";
    }
}