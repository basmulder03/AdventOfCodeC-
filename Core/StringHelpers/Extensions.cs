using System.Text.RegularExpressions;

namespace Core.StringHelpers;

public static partial class Extensions
{
    public static int[] GetInts(this string str)
    {
        var regex = NumberGroupRegex();
        var matches = regex.Matches(str);
        var result = new int[matches.Count];
        for (var i = 0; i < matches.Count; i++) result[i] = int.Parse(matches[i].Value);
        return result;
    }

    public static long[] GetLongs(this string str)
    {
        var regex = NumberGroupRegex();
        var matches = regex.Matches(str);
        var result = new long[matches.Count];
        for (var i = 0; i < matches.Count; i++) result[i] = long.Parse(matches[i].Value);
        return result;
    }

    public static double[] GetDoubles(this string str)
    {
        var regex = NumberGroupRegex();
        var matches = regex.Matches(str);
        var result = new double[matches.Count];
        for (var i = 0; i < matches.Count; i++) result[i] = double.Parse(matches[i].Value);
        return result;
    }

    [GeneratedRegex(@"-?\d+")]
    private static partial Regex NumberGroupRegex();
}