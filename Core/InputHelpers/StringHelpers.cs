namespace Core.InputHelpers;

public static class StringHelpers
{
    private static readonly string[] PossibleNewLines = ["\r\n", "\r", "\n"];

    public static List<string> ReadLines(this string input)
    {
        return input.Split(PossibleNewLines, StringSplitOptions.RemoveEmptyEntries).ToList();
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
        var doubleNewLines = new[] { "\r\n\r\n", "\r\r", "\n\n" };
        return input.Split(doubleNewLines, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ReadLines()).ToList();
    }
}