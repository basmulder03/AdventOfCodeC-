using Core.Hashing;
using Core.Interfaces;

namespace Solutions._2015;

public class Day4 : BaseDay
{
    public override long Part1(string input)
    {
        var hasher = new MD5Hasher();

        for (var i = 0; i < int.MaxValue; i++)
        {
            var hash = hasher.Hash($"{input}{i}");
            if (hash.StartsWith("00000")) return i;
        }

        return -1;
    }

    public override long Part2(string input)
    {
        var hasher = new MD5Hasher();

        for (var i = 0; i < int.MaxValue; i++)
        {
            var hash = hasher.Hash($"{input}{i}");
            if (hash.StartsWith("000000")) return i;
        }

        return -1;
    }
}