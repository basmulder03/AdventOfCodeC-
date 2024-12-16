using Core.Constants;
using Core.DataStructures;
using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2024;

public class Day6 : IBaseDay
{
    public long Part1(string input)
    {
        var lines = input.ReadLines();
        var grid = ParseInput(lines);
        var currentCell = grid.First(node => node.Value.IsVisited);

        var currentDirection = GridDirection.Up;
        while (currentCell != null && currentCell.Value != null)
        {
            if (!currentCell.TryMove(currentDirection, out var nextCell)) break;

            var nextDirection = currentDirection;
            while (nextCell.Value!.IsBlocked)
            {
                nextDirection = nextDirection.Rotate90DegreesClockwise();
                if (!currentCell.TryMove(nextDirection, out nextCell)) break;
            }

            if (nextCell == null || nextCell.Value == null) break;

            nextCell.Value.IsVisited = true;
            nextCell.Value.Directions.Add(nextDirection);
            currentCell = nextCell;
            currentDirection = nextDirection;
        }

        return grid.Rows.SelectMany(row => row).Count(node => node.Value!.IsVisited);
    }

    public long Part2(string input)
    {
        var lines = input.ReadLines();
        var grid = ParseInput(lines);
        var loopingCount = 0;
        var visitedCells = GetNormalRoute((Grid<Node>)grid.Clone());

        foreach (var visitedCell in visitedCells)
        {
            var clonedGrid = (Grid<Node>)grid.Clone();
            var currentCellToBlock = clonedGrid[visitedCell.X, visitedCell.Y];
            if (!currentCellToBlock.Value!.IsBlocked)
            {
                currentCellToBlock.Value.IsBlocked = true;

                var currentCell = clonedGrid.First(node => node.Value!.IsStartingNode);
                var currentDirection = GridDirection.Up;
                while (currentCell != null && currentCell.Value != null)
                {
                    if (!currentCell.TryMove(currentDirection, out var nextCell)) break;

                    var nextDirection = currentDirection;
                    while (nextCell.Value!.IsBlocked)
                    {
                        nextDirection = nextDirection.Rotate90DegreesClockwise();
                        if (!currentCell.TryMove(nextDirection, out nextCell)) break;
                    }

                    if (nextCell == null || nextCell.Value == null) break;

                    if (nextCell.Value.IsVisited && nextCell.Value.Directions.Contains(nextDirection))
                    {
                        loopingCount++;
                        break;
                    }

                    nextCell.Value.IsVisited = true;
                    nextCell.Value.Directions.Add(nextDirection);
                    currentCell = nextCell;
                    currentDirection = nextDirection;
                }
            }
        }

        return loopingCount;
    }

    private static Grid<Node> ParseInput(IEnumerable<string> lines)
    {
        var data = lines.Select(line => line.ToCharArray().Select(c => new Node
        {
            Directions = c == '^' ? [GridDirection.Up] : [],
            IsVisited = c == '^',
            IsBlocked = c == '#',
            IsStartingNode = c == '^'
        }).ToArray()).ToArray();
        return Grid<Node>.FromData(data);
    }

    private static IEnumerable<GridCell<Node>> GetNormalRoute(Grid<Node> grid)
    {
        var currentCell = grid.First(node => node.Value!.IsVisited);
        var currentDirection = GridDirection.Up;
        while (currentCell != null && currentCell.Value != null)
        {
            if (!currentCell.TryMove(currentDirection, out var nextCell)) break;

            var nextDirection = currentDirection;
            while (nextCell.Value!.IsBlocked)
            {
                nextDirection = nextDirection.Rotate90DegreesClockwise();
                if (!currentCell.TryMove(nextDirection, out nextCell)) break;
            }

            if (nextCell == null || nextCell.Value == null) break;

            nextCell.Value.IsVisited = true;
            nextCell.Value.Directions.Add(nextDirection);
            currentCell = nextCell;
            currentDirection = nextDirection;
        }

        return grid.Where(node => node.Value!.IsVisited);
    }

    private class Node : ICloneable
    {
        public bool IsBlocked { get; set; }
        public bool IsVisited { get; set; }
        public HashSet<GridDirection> Directions { get; set; } = new();
        public bool IsStartingNode { get; init; }

        public object Clone()
        {
            var node = (Node)MemberwiseClone();
            node.Directions = [];
            if (node.IsStartingNode) node.Directions.Add(GridDirection.Up);
            return node;
        }
    }
}