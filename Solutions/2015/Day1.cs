using Core.Interfaces;

namespace Solutions._2015;

public class Day1 : BaseDay
{
    public override long Part1(string input)
    {
        var chars = input.ToCharArray();
        var floor = 0;
        foreach (var c in chars)
            switch (c)
            {
                case '(':
                    floor++;
                    break;
                case ')':
                    floor--;
                    break;
            }

        return floor;
    }

    public override long Part2(string input)
    {
        var chars = input.ToCharArray();
        var floor = 0;
        for (var i = 0; i < chars.Length; i++)
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

            if (floor == -1) return i + 1;
        }

        return -1;
    }
}