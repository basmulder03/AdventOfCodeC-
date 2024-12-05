using System.Text.RegularExpressions;

namespace Core.StringHelpers;

public static partial class Extensions
{
    public static int[] GetInts(this string str)
    {
        var regex = NumberGroupRegex();
        var matches = regex.Matches(str);
        var result = new int[matches.Count];
        for (var i = 0; i < matches.Count; i++)
        {
            result[i] = int.Parse(matches[i].Value);
        }
        return result;
    }

    [GeneratedRegex(@"-?\d+")]
    private static partial Regex NumberGroupRegex();
}