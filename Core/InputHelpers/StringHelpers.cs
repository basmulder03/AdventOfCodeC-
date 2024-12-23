namespace Core.InputHelpers;

public static class StringHelpers
{
    public static List<string> ReadLines(this string input)
    {
        return input.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static List<int> ReadLinesAsInt(this string input)
    {
        var lines = input.ReadLines();
        return lines.Select(int.Parse).ToList();
    }

    public static List<long> ReadLinesAsLong(this string input)
    {
        var lines = input.ReadLines();
        return lines.Select(long.Parse).ToList();
    }

    public static List<List<string>> ReadGroups(this string input)
    {
        return input.Split($"{Environment.NewLine}{Environment.NewLine}").Select(x => x.ReadLines()).ToList();
    }
}