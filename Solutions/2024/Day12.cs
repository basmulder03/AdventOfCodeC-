using Core.Constants;
using Core.DataHelper;
using Core.DataStructures;
using Core.Interfaces;

namespace Solutions._2024;

public class Day12 : BaseDay
{
    public long Part1(FileStream fileStream)
    {
        return CalculateTotal(fileStream, SizeOfPlot);
    }

    public long Part2(FileStream fileStream)
    {
        return CalculateTotal(fileStream, CalculatePerimeter);
    }

    private static long CalculateTotal(FileStream fileStream, Func<GridCell<Node>, (int, int)> calculationMethod)
    {
        var grid = Parse(fileStream);
        var total = 0;

        foreach (var cell in grid)
        {
            if (cell.Value!.Visited) continue;

            var (perimeter, plots) = calculationMethod(cell);
            total += perimeter * plots;
        }

        return total;
    }

    private static Grid<Node> Parse(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var data = lines
            .Select(line => line.ToCharArray().Select(c => new Node { Type = c, Visited = false }).ToArray()).ToArray();
        return Grid<Node>.FromData(data);
    }

    private static (int, int) SizeOfPlot(GridCell<Node> startCell)
    {
        startCell.Value!.Visited = true;
        var newPerimeter = 0;
        var newPlots = 1;

        foreach (var direction in GridDirectionsHelper.CardinalDirections)
        {
            UpdatePerimeterAndPlots(startCell, direction, ref newPerimeter, ref newPlots, SizeOfPlot);
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

    private static void UpdatePerimeterAndPlots(GridCell<Node> startCell, GridDirection direction, ref int newPerimeter,
        ref int newPlots, Func<GridCell<Node>, (int, int)> calculationMethod)
    {
        if (startCell.TryMove(direction, out var cardinal) && cardinal.Value != null)
        {
            if (cardinal.Value.Type == startCell.Value.Type && cardinal.Value.Visited) return;

            if (cardinal.Value.Type == startCell.Value.Type)
            {
                var (perimeterSize, plots) = calculationMethod(cardinal);
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

    private static bool InnerCorner(GridCell<Node> cell, GridDirection direction)
    {
        if (!cell.TryMove(direction, out var neighbor) || neighbor.Value == null ||
            neighbor.Value.Type == cell.Value!.Type) return false;

        var clockwise = direction.Rotate45DegreesClockwise();
        var counterClockwise = direction.Rotate45DegreesCounterClockwise();

        var cardinal1 = cell.TryMove(clockwise, out var c1) && c1.Value != null && c1.Value.Type == cell.Value.Type;
        var cardinal2 = cell.TryMove(counterClockwise, out var c2) && c2.Value != null &&
                        c2.Value.Type == cell.Value.Type;

        return cardinal1 && cardinal2;
    }

    private static bool OuterCorner(GridCell<Node> cell, GridDirection direction)
    {
        var clockwise = direction.Rotate45DegreesClockwise();
        var counterClockwise = direction.Rotate45DegreesCounterClockwise();

        var cardinal1 = !(cell.TryMove(clockwise, out var c1) && c1.Value != null && c1.Value.Type == cell.Value.Type);
        var cardinal2 = !(cell.TryMove(counterClockwise, out var c2) && c2.Value != null &&
                          c2.Value.Type == cell.Value.Type);

        return cardinal1 && cardinal2;
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
