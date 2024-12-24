using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2024;

public class Day24 : BaseDay
{
    public override long Part1(string input)
    {
        var splitInput = input.ReadGroups();
        var knownValues = splitInput[0].Select(x => x.Split(": ")).ToDictionary(x => x[0], x => x[1] == "1");
        var unknownValues = splitInput[1].Select(x => x.Split(" -> ")).ToDictionary(x => x[1], x => x[0]);
        foreach (var key in unknownValues.Keys)
        {
            if (knownValues.ContainsKey(key)) continue;
            knownValues[key] = Result(key, knownValues, unknownValues);
        }

        var orderedKeysThatStartWithZ = knownValues.Where(x => x.Key.StartsWith("z")).OrderByDescending(x => x.Key)
            .Select(x => x.Value);
        return ConvertBinaryToBase10(orderedKeysThatStartWithZ);
    }

    public override string Part2String(string input)
    {
        var splitInput = input.ReadGroups();
        var knownValues = splitInput[0].Select(x => x.Split(": ")).ToDictionary(x => x[0], x => x[1] == "1");
        var unknownValues = splitInput[1].Select(x => x.Split(" -> ")).ToDictionary(x => x[1], x => x[0]);
        foreach (var key in unknownValues.Keys)
        {
            if (knownValues.ContainsKey(key)) continue;
            knownValues[key] = Result(key, knownValues, unknownValues);
        }

        var binaryAs = knownValues.Where(x => x.Key.StartsWith("a")).OrderByDescending(x => x.Key).ToDictionary(x => x.Key[1..], x => x.Value);
        var binaryBs = knownValues.Where(x => x.Key.StartsWith("b")).OrderByDescending(x => x.Key).ToDictionary(x => x.Key[1..], x => x.Value);
        var binaryZs = knownValues.Where(x => x.Key.StartsWith("z")).OrderByDescending(x => x.Key).ToDictionary(x => x.Key[1..], x => x.Value);

        for (var i = 0; i < binaryAs.Count; i++)
        {
            var beforeKey = binaryAs.Keys.ElementAt(i);
            var binaryA = binaryAs[beforeKey];
            var binaryB = binaryBs[beforeKey];
            var binaryZ = binaryZs[i];
            if (binaryA != binaryB)
            {
                binaryZ = binaryA;
            }
        }
        
        return "";
    }

    private static bool Result(string valueToGet, Dictionary<string, bool> knownValues,
        Dictionary<string, string> unknownValues)
    {
        if (knownValues.TryGetValue(valueToGet, out var knownValue)) return knownValue;

        if (!unknownValues.TryGetValue(valueToGet, out var unknownValue)) return false;

        var splitOperation = unknownValue.Split(" ");
        var startOperation = Result(splitOperation[0], knownValues, unknownValues);
        var endOperation = Result(splitOperation[2], knownValues, unknownValues);
        var result = splitOperation[1] switch
        {
            "AND" => startOperation & endOperation,
            "OR" => startOperation | endOperation,
            "XOR" => startOperation ^ endOperation,
            _ => throw new NotImplementedException()
        };
        knownValues[valueToGet] = result;
        return result;
    }

    private static long ConvertBinaryToBase10(IEnumerable<bool> booleanValues)
    {
        var convertToBinary = string.Join("", booleanValues.Select(x => x ? "1" : "0"));
        return Convert.ToInt64(convertToBinary, 2);
    }
}