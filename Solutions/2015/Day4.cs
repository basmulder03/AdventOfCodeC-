using Core;
using Core.DataHelper;
using Core.Hashing;

namespace Solutions._2015;

public class Day4 : BaseDay
{
    public long Part1(FileStream fileStream)
    {
        var data = fileStream.ReadSingleLine();
        var hasher = new MD5Hasher();

        for (var i = 0; i < int.MaxValue; i++)
        {
            var hash = hasher.Hash($"{data}{i}");
            if (hash.StartsWith("00000")) return i;
        }

        return -1;
    }

    public long Part2(FileStream fileStream)
    {
        var data = fileStream.ReadSingleLine();
        var hasher = new MD5Hasher();

        for (var i = 0; i < int.MaxValue; i++)
        {
            var hash = hasher.Hash($"{data}{i}");
            if (hash.StartsWith("000000")) return i;
        }

        return -1;
    }
}