using Core;
using Core.DataHelper;
using Core.DataStructures;
using Core.Extensions;

namespace Solutions._2024;

public class Day8 : IDay
{
    public long Part1(FileStream fileStream)
    {
        var grid = Parse(fileStream);
        var antennas = grid.Where(node => node != '.').GroupBy(node => node.Value);
        var antiNodes = new HashSet<GridCell<char>>();
        
        foreach (var antennaGroup in antennas)
        {
            foreach (var antennaA in antennaGroup)
            {
                foreach (var antennaB in antennaGroup)
                {
                    if (antennaA == antennaB)
                    {
                        continue;
                    }
                    
                    var yDiff = antennaA.Y - antennaB.Y;
                    var xDiff = antennaA.X - antennaB.X;

                    var antiNode = antennaA.Move(xDiff, yDiff);
                    if (antiNode.HasValue)
                    {
                        antiNodes.Add(antiNode);
                    }
                }
            }
        }

        return antiNodes.Count;
    }

    public long Part2(FileStream fileStream)
    {
        var grid = Parse(fileStream);
        var antennas = grid.Where(node => node != '.').GroupBy(node => node.Value);
        var antiNodes = new HashSet<GridCell<char>>();
        
        foreach (var antennaGroup in antennas)
        {
            foreach (var antennaA in antennaGroup)
            {
                antiNodes.Add(antennaA);
                foreach (var antennaB in antennaGroup)
                {
                    if (antennaA == antennaB)
                    {
                        continue;
                    }
                    
                    var yDiff = antennaA.Y - antennaB.Y;
                    var xDiff = antennaA.X - antennaB.X;

                    var antiNode = antennaA.Move(xDiff, yDiff);
                    
                    while (antiNode.HasValue)
                    {
                        antiNodes.Add(antiNode);
                        antiNode = antiNode.Move(xDiff, yDiff);
                    }
                }
            }
        }
        return antiNodes.Count;
    }

    private static Grid<char> Parse(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        return Grid<char>.Parse(lines, line => line.ToCharArray());
    }
}