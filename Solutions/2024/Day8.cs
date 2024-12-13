using Core.DataHelper;
using Core.DataStructures;
using Core.Interfaces;

namespace Solutions._2024;

public class Day8 : BaseDay
{
    public long Part1(FileStream fileStream)
    {
        var grid = Parse(fileStream);
        var antennas = grid.Where(node => node.Value != '.').GroupBy(node => node.Value);
        var antiNodes = new HashSet<GridCell<char>>();

        foreach (var antennaGroup in antennas)
        {
            foreach (var antennaA in antennaGroup)
            {
                foreach (var antennaB in antennaGroup)
                {
                    if (antennaA == antennaB) continue;

                    var diff = antennaA.Coordinate - antennaB.Coordinate;

                    if (antennaA.TryMove(diff, out var antiNode)) antiNodes.Add(antiNode!);
                }
            }
        }

        return antiNodes.Count;
    }

    public long Part2(FileStream fileStream)
    {
        var grid = Parse(fileStream);
        var antennas = grid.Where(node => node.Value != '.').GroupBy(node => node.Value);
        var antiNodes = new HashSet<GridCell<char>>();

        foreach (var antennaGroup in antennas)
        {
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
        }

        return antiNodes.Count;
    }

    private static Grid<char> Parse(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var data = lines.Select(line => line.ToCharArray()).ToArray();
        return Grid<char>.FromData(data);
    }
}
