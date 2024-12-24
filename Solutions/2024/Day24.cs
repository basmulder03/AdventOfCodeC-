using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2024;

public class Day24 : BaseDay
{
    public override long Part1(string input)
    {
        var splitInput = input.ReadGroups();
        var knownValues = splitInput[0]
            .Select(x => x.Split(": "))
            .ToDictionary(x => x[0], x => x[1] == "1");
        var unknownValues = splitInput[1]
            .Select(x => x.Split(" -> "))
            .ToDictionary(x => x[1], x => x[0]);

        foreach (var key in unknownValues.Keys.Where(key => !knownValues.ContainsKey(key)))
        {
            knownValues[key] = EvaluateResult(key, knownValues, unknownValues);
        }

        var orderedKeysThatStartWithZ = knownValues
            .Where(x => x.Key.StartsWith('z'))
            .OrderByDescending(x => x.Key)
            .Select(x => x.Value);

        return ConvertBinaryToDecimal(orderedKeysThatStartWithZ);
    }

    public override string Part2String(string input)
    {
        var splitInput = input.ReadGroups();
        var configurations = splitInput[1].ToList();

        var swaps = CheckParallelAdders(configurations);

        return string.Join(",", swaps.OrderBy(x => x));
    }

    private static bool EvaluateResult(string valueToGet, Dictionary<string, bool> knownValues,
        Dictionary<string, string> unknownValues)
    {
        if (knownValues.TryGetValue(valueToGet, out var knownValue)) return knownValue;

        if (!unknownValues.TryGetValue(valueToGet, out var unknownValue)) return false;

        var splitOperation = unknownValue.Split(" ");
        var startOperation = EvaluateResult(splitOperation[0], knownValues, unknownValues);
        var endOperation = EvaluateResult(splitOperation[2], knownValues, unknownValues);

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

    private static long ConvertBinaryToDecimal(IEnumerable<bool> booleanValues)
    {
        var binaryString = string.Join("", booleanValues.Select(x => x ? "1" : "0"));
        return Convert.ToInt64(binaryString, 2);
    }

    private static List<string> CheckParallelAdders(List<string> configurations)
    {
        string? currentCarryWire = null;
        var swaps = new List<string>();
        var bit = 0;

        while (true)
        {
            var xWire = $"x{bit:D2}";
            var yWire = $"y{bit:D2}";
            var zWire = $"z{bit:D2}";

            if (bit == 0)
            {
                currentCarryWire = FindGate(xWire, yWire, "AND", configurations);
            }
            else
            {
                var abXorGate = FindGate(xWire, yWire, "XOR", configurations);
                var abAndGate = FindGate(xWire, yWire, "AND", configurations);

                var cinAbXorGate = FindGate(abXorGate!, currentCarryWire!, "XOR", configurations);
                if (cinAbXorGate == null)
                {
                    swaps.Add(abXorGate!);
                    swaps.Add(abAndGate!);
                    configurations = SwapOutputWires(abXorGate!, abAndGate!, configurations);
                    bit = 0;
                    continue;
                }

                if (cinAbXorGate != zWire)
                {
                    swaps.Add(cinAbXorGate);
                    swaps.Add(zWire);
                    configurations = SwapOutputWires(cinAbXorGate, zWire, configurations);
                    bit = 0;
                    continue;
                }

                var cinAbAndGate = FindGate(abXorGate!, currentCarryWire!, "AND", configurations);
                currentCarryWire = FindGate(abAndGate!, cinAbAndGate!, "OR", configurations);
            }

            bit++;
            if (bit >= 45) break;
        }

        return swaps;
    }

    private static string? FindGate(string xWire, string yWire, string gateType, List<string> configurations)
    {
        var subStrA = $"{xWire} {gateType} {yWire} -> ";
        var subStrB = $"{yWire} {gateType} {xWire} -> ";

        return configurations
            .FirstOrDefault(config => config.StartsWith(subStrA) || config.StartsWith(subStrB))?
            .Split(" -> ")[^1];
    }

    private static List<string> SwapOutputWires(string wireA, string wireB, List<string> configurations)
    {
        return configurations.Select(config =>
        {
            var parts = config.Split(" -> ");
            var inputWires = parts[0];
            var outputWire = parts[1];

            if (outputWire == wireA) return $"{inputWires} -> {wireB}";
            return outputWire == wireB ? $"{inputWires} -> {wireA}" : config;
        }).ToList();
    }
}
