using Core;
using Core.Constants;
using Core.DataHelper;
using Core.DataStructures;

namespace Solutions._2024;

public class Day6 : IDay
{
    public long Part1(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var grid = ParseInput(lines);
        var currentCell = grid.First(node => node.Value!.IsVisited);

        var currentDirection = GridDirection.Up;
        while (currentCell.HasValue)
        {
            var nextCell = currentCell[currentDirection];
            var nextDirection = currentDirection;
            while (nextCell.HasValue && nextCell.Value!.IsBlocked)
            {
                nextDirection = nextDirection.Rotate90DegreesClockwise();
                nextCell = currentCell[nextDirection];
            }

            if (!nextCell.HasValue) break;

            nextCell.Value!.IsVisited = true;
            nextCell.Value!.Directions.Add(nextDirection);
            currentCell = nextCell;
            currentDirection = nextDirection;
        }

        return grid.Rows.SelectMany(row => row).Count(node => node.Value!.IsVisited);
    }

    public long Part2(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var grid = ParseInput(lines);
        var loopingCount = 0;
        var visitedCells = GetNormalRoute(grid);

        foreach (var visitedCell in visitedCells)
        {
            var clonedGrid = (Grid<Node>)grid.Clone();
            var currentCellToBlock = clonedGrid[visitedCell.X, visitedCell.Y];
            if (!currentCellToBlock.HasValue || currentCellToBlock.Value!.IsBlocked) continue;

            currentCellToBlock.Value!.IsBlocked = true;

            var currentCell = clonedGrid.First(node => node.Value!.IsStartingNode);
            var currentDirection = GridDirection.Up;
            while (currentCell.HasValue)
            {
                var nextCell = currentCell[currentDirection];
                var nextDirection = currentDirection;

                while (nextCell.HasValue && nextCell.Value!.IsBlocked)
                {
                    nextDirection = nextDirection.Rotate90DegreesClockwise();
                    nextCell = currentCell[nextDirection];
                }

                if (nextCell.IsEmpty) break;

                if (nextCell.HasValue && nextCell.Value!.IsVisited &&
                    nextCell.Value!.Directions.Contains(nextDirection))
                {
                    loopingCount++;
                    break;
                }

                nextCell.Value!.IsVisited = true;
                nextCell.Value!.Directions.Add(nextDirection);
                currentCell = nextCell;
                currentDirection = nextDirection;
            }
        }

        return loopingCount;
    }

    private static Grid<Node> ParseInput(List<string> lines)
    {
        return Grid<Node>.Parse(lines, s =>
        {
            var charArray = s.ToCharArray();
            var nodes = new Node[charArray.Length];
            for (var i = 0; i < charArray.Length; i++)
            {
                nodes[i] = new Node
                {
                    IsBlocked = charArray[i] == '#', IsVisited = charArray[i] == '^',
                    IsStartingNode = charArray[i] == '^'
                };
                if (nodes[i].IsStartingNode) nodes[i].Directions.Add(GridDirection.Up);
            }

            return nodes;
        });
    }

    private static IEnumerable<GridCell<Node>> GetNormalRoute(Grid<Node> grid)
    {
        var currentCell = grid.First(node => node.Value!.IsVisited);
        var currentDirection = GridDirection.Up;
        while (currentCell.HasValue)
        {
            var nextCell = currentCell[currentDirection];
            var nextDirection = currentDirection;
            while (nextCell.HasValue && nextCell.Value!.IsBlocked)
            {
                nextDirection = nextDirection.Rotate90DegreesClockwise();
                nextCell = currentCell[nextDirection];
            }

            if (!nextCell.HasValue) break;

            nextCell.Value!.IsVisited = true;
            nextCell.Value!.Directions.Add(nextDirection);
            currentCell = nextCell;
            currentDirection = nextDirection;
        }

        return grid.Where(node => node.Value!.IsVisited);
    }

    private class Node : ICloneable
    {
        public bool IsBlocked { get; set; }
        public bool IsVisited { get; set; }
        public HashSet<GridDirection> Directions { get; private set; } = [];
        public bool IsStartingNode { get; init; }

        public object Clone()
        {
            var clone = (Node)MemberwiseClone();
            clone.Directions = [];
            if (clone.IsStartingNode) clone.Directions.Add(GridDirection.Up);

            return clone;
        }
    }
}