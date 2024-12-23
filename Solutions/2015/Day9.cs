﻿using Core.Extensions;
using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2015;

public class Day9 : BaseDay
{
    public override long Part1(string input)
    {
        var lines = input.ReadLines();
        var distances = new Dictionary<(string, string), int>();
        var cities = new HashSet<string>();
        foreach (var line in lines)
        {
            var parts = line.Split(" ");
            var city1 = parts[0];
            var city2 = parts[2];
            var distance = int.Parse(parts[4]);
            distances[(city1, city2)] = distance;
            distances[(city2, city1)] = distance;
            cities.Add(city1);
            cities.Add(city2);
        }

        var permutations = cities.Permutations();

        var minDistance = int.MaxValue;
        foreach (var permutation in permutations)
        {
            var distance = 0;
            for (var i = 0; i < permutation.Count() - 1; i++)
                distance += distances[(permutation.ElementAt(i), permutation.ElementAt(i + 1))];
            minDistance = Math.Min(minDistance, distance);
        }

        return minDistance;
    }

    public override long Part2(string input)
    {
        var lines = input.ReadLines();
        var distances = new Dictionary<(string, string), int>();
        var cities = new HashSet<string>();
        foreach (var line in lines)
        {
            var parts = line.Split(" ");
            var city1 = parts[0];
            var city2 = parts[2];
            var distance = int.Parse(parts[4]);
            distances[(city1, city2)] = distance;
            distances[(city2, city1)] = distance;
            cities.Add(city1);
            cities.Add(city2);
        }

        var permutations = cities.Permutations();

        var maxDistance = int.MinValue;
        foreach (var permutation in permutations)
        {
            var distance = 0;
            for (var i = 0; i < permutation.Count() - 1; i++)
                distance += distances[(permutation.ElementAt(i), permutation.ElementAt(i + 1))];
            maxDistance = Math.Max(maxDistance, distance);
        }

        return maxDistance;
    }
}