using Core;
using Core.Constants;
using Core.DataHelper;
using Core.DataStructures;

namespace Solutions._2024;

public class Day12 : IDay
{
    public long Part1(FileStream fileStream)
    {
        var grid = Parse(fileStream);
        var total = 0;

        foreach (var cell in grid)
        {
            if (cell.Value!.Visited) continue;

            var (perimeter, plots) = SizeOfPlot(cell);
            total += perimeter * plots;
        }

        return total;
    }

    public long Part2(FileStream fileStream)
    {
        var grid = Parse(fileStream);
        var total = 0;

        foreach (var cell in grid)
        {
            if (cell.Value!.Visited) continue;

            var (perimeter, plots) = CalculatePerimeter(cell);
            total += perimeter * plots;
        }

        return total;
    }

    private static Grid<Node> Parse(FileStream fileStream)
    {
        return Grid<Node>.Parse(fileStream.ReadLines(),
            str => str.ToCharArray().Select(c => new Node { Type = c, Visited = false }).ToArray());
    }

    private static (int, int) SizeOfPlot(GridCell<Node> startCell)
    {
        startCell.Value!.Visited = true;
        var newPerimeter = 0;
        var newPlots = 1;

        foreach (var direction in GridDirectionsHelper.CardinalDirections)
        {
            var cardinal = startCell[direction];
            if (cardinal.HasValue)
            {
                if (cardinal.Value!.Type == startCell.Value!.Type && cardinal.Value!.Visited) continue;

                if (cardinal.Value!.Type == startCell.Value!.Type)
                {
                    var (perimeterSize, plots) = SizeOfPlot(cardinal);
                    newPerimeter += perimeterSize;
                    newPlots += plots;
                }
                else
                {
                    newPerimeter++;
                }
            }
            else
            {
                newPerimeter++;
            }
        }

        return (newPerimeter, newPlots);
    }

    private static (int perimeter, int plots) CalculatePerimeter(GridCell<Node> startCell)
    {
        startCell.Value!.Visited = true;
        var corners = GridDirectionsHelper.DiagonalDirections.Count(direction =>
            InnerCorner(startCell, direction) || OuterCorner(startCell, direction));
        var newPlotCount = 1;

        foreach (var neighbor in startCell.CardinalNeighbors)
        {
            if (neighbor.Value!.Visited || neighbor.Value!.Type != startCell.Value!.Type) continue;

            var (perimeter, plots) = CalculatePerimeter(neighbor);
            corners += perimeter;
            newPlotCount += plots;
        }

        return (corners, newPlotCount);
    }

    private static bool InnerCorner(GridCell<Node> cell, GridDirection direction)
    {
        var neighbor = cell[direction];
        if (!neighbor.HasValue || neighbor.Value!.Type == cell.Value!.Type) return false;

        var cardinal1 = cell[direction.Rotate45DegreesClockwise()];
        var cardinal2 = cell[direction.Rotate45DegreesCounterClockwise()];

        return cell.Value!.Type == cardinal1.Value!.Type && cell.Value!.Type == cardinal2.Value!.Type;
    }

    private static bool OuterCorner(GridCell<Node> cell, GridDirection direction)
    {
        var cardinal1 = cell[direction.Rotate45DegreesClockwise()];
        var cardinal2 = cell[direction.Rotate45DegreesCounterClockwise()];

        return (!cardinal1.HasValue || cell.Value!.Type != cardinal1.Value!.Type) &&
               (!cardinal2.HasValue || cell.Value!.Type != cardinal2.Value!.Type);
    }

    private class Node : ICloneable
    {
        public char Type { get; set; }
        public bool Visited { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{Type} (Visited={(Visited ? "Y" : "N")})";
        }
    }
}