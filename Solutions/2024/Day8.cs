using Core.DataStructures;
using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2024;

public class Day8 : IBaseDay
{
    public long Part1(string input)
    {
        var grid = Parse(input);
        var antennas = grid.Where(node => node.Value != '.').GroupBy(node => node.Value);
        var antiNodes = new HashSet<GridCell<char>>();

        foreach (var antennaGroup in antennas)
        foreach (var antennaA in antennaGroup)
        foreach (var antennaB in antennaGroup)
        {
            if (antennaA == antennaB) continue;

            var diff = antennaA.Coordinate - antennaB.Coordinate;

            if (antennaA.TryMove(diff, out var antiNode)) antiNodes.Add(antiNode!);
        }

        return antiNodes.Count;
    }

    public long Part2(string input)
    {
        var grid = Parse(input);
        var antennas = grid.Where(node => node.Value != '.').GroupBy(node => node.Value);
        var antiNodes = new HashSet<GridCell<char>>();

        foreach (var antennaGroup in antennas)
        foreach (var antennaA in antennaGroup)
        {
            antiNodes.Add(antennaA);
            foreach (var antennaB in antennaGroup)
            {
                if (antennaA == antennaB) continue;

                var diff = antennaA.Coordinate - antennaB.Coordinate;
                antennaA.TryMove(diff, out var antiNode);

                while (antiNode != null)
                {
                    antiNodes.Add(antiNode);
                    antiNode.TryMove(diff, out antiNode);
                }
            }
        }

        return antiNodes.Count;
    }

    private static Grid<char> Parse(string input)
    {
        var lines = input.ReadLines();
        var data = lines.Select(line => line.ToCharArray()).ToArray();
        return Grid<char>.FromData(data);
    }
}