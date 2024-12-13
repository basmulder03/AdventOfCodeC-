using Core.DataHelper;
using Core.Interfaces;

namespace Solutions._2015;

public class Day7 : BaseDay
{
    private readonly Dictionary<string, Func<int, int, int>> _operations = new()
    {
        { "AND", (a, b) => a & b },
        { "OR", (a, b) => a | b },
        { "LSHIFT", (a, b) => a << b },
        { "RSHIFT", (a, b) => a >> b },
        { "NOT", (a, _) => ~a }
    };

    public long Part1(FileStream fileStream)
    {
        var (wiresDict, wires) = ParseInput(fileStream);
        const string wireToFind = "a";
        return FindValue(wireToFind, wires, wiresDict);
    }

    public long Part2(FileStream fileStream)
    {
        var (wiresDict, wires) = ParseInput(fileStream);
        const string wireToFind = "a";
        var part1Result = FindValue(wireToFind, wires, wiresDict);

        wiresDict["b"] = part1Result.ToString();
        wires.Clear();

        return FindValue(wireToFind, wires, wiresDict);
    }

    private static (Dictionary<string, string> wiresDict, Dictionary<string, int> wires) ParseInput(
        FileStream fileStream)
    {
        var lines = fileStream.ReadLines().ToList();
        var wiresDict = new Dictionary<string, string>();
        var wires = new Dictionary<string, int>();

        foreach (var parts in lines.Select(s => s.Split(" -> "))) wiresDict[parts[1]] = parts[0];

        return (wiresDict, wires);
    }

    private int FindValue(string key, Dictionary<string, int> wires, Dictionary<string, string> wiresDict)
    {
        if (int.TryParse(key, out var value)) return value;

        if (wires.TryGetValue(key, out var findValue)) return findValue;

        var statement = wiresDict[key];
        var parts = statement.Split(" ");

        var result = parts.Length switch
        {
            1 => FindValue(parts[0], wires, wiresDict),
            2 => _operations["NOT"](FindValue(parts[1], wires, wiresDict), -1),
            3 => _operations[parts[1]](FindValue(parts[0], wires, wiresDict), FindValue(parts[2], wires, wiresDict)),
            _ => 0
        };

        wires[key] = result;
        return result;
    }
}