﻿using Core.Extensions;
using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2015;

public class Day13 : BaseDay
{
    public override long Part1(string input)
    {
        var happiness = ParseInput(input);
        return CalculateMaxHappiness(happiness);
    }

    public override long Part2(string input)
    {
        var happiness = ParseInput(input);
        AddSelfToHappiness(happiness);
        return CalculateMaxHappiness(happiness);
    }

    private static Dictionary<string, Dictionary<string, int>> ParseInput(string input)
    {
        var lines = input.ReadLines();
        var happiness = new Dictionary<string, Dictionary<string, int>>();
        foreach (var line in lines)
        {
            var parts = line.Split(" ");
            var person = parts[0];
            var neighbor = parts[^1].TrimEnd('.');
            var value = int.Parse(parts[3]) * (parts[2] == "gain" ? 1 : -1);
            if (!happiness.TryGetValue(person, out Dictionary<string, int>? happinessValue))
            {
                happinessValue = new Dictionary<string, int>();
                happiness[person] = happinessValue;
            }

            happinessValue[neighbor] = value;
        }

        return happiness;
    }

    private static void AddSelfToHappiness(Dictionary<string, Dictionary<string, int>> happiness)
    {
        var people = happiness.Keys.ToList();
        happiness["Me"] = new Dictionary<string, int>();
        foreach (var person in people)
        {
            happiness["Me"][person] = 0;
            happiness[person]["Me"] = 0;
        }
    }

    private static int CalculateMaxHappiness(Dictionary<string, Dictionary<string, int>> happiness)
    {
        var people = happiness.Keys.ToList();
        var permutations = people.Permutations();
        var maxHappiness = int.MinValue;

        foreach (var permutation in permutations)
        {
            var happinessChange = 0;
            for (var i = 0; i < permutation.Count(); i++)
            {
                var person = permutation.ElementAt(i);
                var left = permutation.ElementAt(i == 0 ? permutation.Count() - 1 : i - 1);
                var right = permutation.ElementAt(i == permutation.Count() - 1 ? 0 : i + 1);
                happinessChange += happiness[person][left] + happiness[person][right];
            }

            maxHappiness = Math.Max(maxHappiness, happinessChange);
        }

        return maxHappiness;
    }
}