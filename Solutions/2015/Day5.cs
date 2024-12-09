using Core;
using Core.DataHelper;

namespace Solutions._2015;

public class Day5 : IDay
{
    private const string Vowels = "aeiou";
    private const string BadStrings = "ab,cd,pq,xy";

    public long Part1(FileStream fileStream)
    {
        var strings = fileStream.ReadLines();
        var niceStrings = 0;

        foreach (var str in strings)
        {
            var vowelCount = 0;
            var doubleLetter = false;
            var badString = false;

            for (var i = 0; i < str.Length; i++)
            {
                if (Vowels.Contains(str[i])) vowelCount++;

                if (i > 0 && str[i] == str[i - 1]) doubleLetter = true;

                if (i > 0 && BadStrings.Contains(str.Substring(i - 1, 2))) badString = true;
            }

            if (vowelCount >= 3 && doubleLetter && !badString) niceStrings++;
        }

        return niceStrings;
    }

    public long Part2(FileStream fileStream)
    {
        var strings = fileStream.ReadLines();
        var niceStrings = 0;

        foreach (var str in strings)
        {
            var pair = false;
            var repeat = false;

            for (var i = 0; i < str.Length - 1; i++)
            {
                if (i < str.Length - 2 && str.Substring(i + 2).Contains(str.Substring(i, 2))) pair = true;

                if (i < str.Length - 2 && str[i] == str[i + 2]) repeat = true;
            }

            if (pair && repeat) niceStrings++;
        }

        return niceStrings;
    }
}