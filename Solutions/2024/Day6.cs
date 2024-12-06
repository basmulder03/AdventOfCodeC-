using Core;
using Core.Constants;
using Core.DataHelper;
using Core.DataStructures;

namespace Solutions._2024;

public class Day6 : IDay
{
    public int Part1(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var grid = ParseInput(lines);
        var currentCell = grid.First(node => node.IsVisited);

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

            if (!nextCell.HasValue)
            {
                currentCell = nextCell;
                break;
            }
            
            nextCell.Value!.IsVisited = true;
            nextCell.Value!.Directions.Add(nextDirection);
            currentCell = nextCell;
            currentDirection = nextDirection;
        }
        
        // Console.WriteLine(grid.ToString(node =>
        // {
        //     var currDirectionChar = node.CurrentDirection switch
        //     {
        //         GridDirection.Up => '^',
        //         GridDirection.Right => '>',
        //         GridDirection.Down => 'v',
        //         GridDirection.Left => '<',
        //         _ => throw new ArgumentOutOfRangeException()
        //     };
        //     return (node.IsVisited ? currDirectionChar : node.IsBlocked ? '#' : '.').ToString();
        // }));

        return grid.Rows.SelectMany(row => row).Count(node => node.Value!.IsVisited);
    }

    public int Part2(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var grid = ParseInput(lines);
        var loopingCount = 0;
        
        var ogGrid = (Grid<Node>)grid.Clone();
        var currentCell = ogGrid.First(node => node.IsVisited);

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

            if (!nextCell.HasValue)
            {
                currentCell = nextCell;
                break;
            }
            
            nextCell.Value!.IsVisited = true;
            nextCell.Value!.Directions.Add(nextDirection);
            currentCell = nextCell;
            currentDirection = nextDirection;
        }

        var visitedNodes = ogGrid.Where(node => node is { IsVisited: true, IsStartingNode: false }).ToList();

        foreach (var node in visitedNodes)
        {
            var copy = (Grid<Node>)grid.Clone();
            var nodeCopy = copy.Get(node.X, node.Y);
            nodeCopy.Value!.IsBlocked = true;
            if (PathLoops(copy))
            {
                Console.WriteLine("Path loops");
                loopingCount++;
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
                nodes[i] = new Node {IsBlocked = charArray[i] == '#', IsVisited = charArray[i] == '^', IsStartingNode = charArray[i] == '^'};
                if (nodes[i].IsStartingNode)
                {
                    nodes[i].Directions.Add(GridDirection.Up);
                }
            }

            return nodes;
        });
    }

    private static bool PathLoops(Grid<Node> grid)
    {
        var start = grid.First(node => node.IsVisited);
        var currentCell = start;
        var currentDirection = GridDirection.Up;
        while (currentCell.HasValue)
        {
            var nextCell = currentCell[currentDirection];
            if (IsLoop(nextCell, currentDirection)) return true;
            var nextDirection = currentDirection;
            
            while (nextCell.HasValue && nextCell.Value!.IsBlocked)
            {
                nextDirection = nextDirection.Rotate90DegreesClockwise();
                if (IsLoop(nextCell, nextDirection)) return true;
                nextCell = currentCell[nextDirection];
            }

            if (!nextCell.HasValue)
            {
                currentCell = nextCell;
                break;
            }
            
            nextCell.Value!.IsVisited = true;
            nextCell.Value!.Directions.Add(nextDirection);
            currentCell = nextCell;
            currentDirection = nextDirection;
        }

        return false;
    }

    private static bool IsLoop(GridCell<Node> newNode, GridDirection direction)
    {
        return newNode.HasValue
               && newNode.Value!.IsVisited
               && newNode.Value!.Directions.Contains(direction);
    }

    private class Node : ICloneable
    {
        public bool IsBlocked { get; set; } = false;
        public bool IsVisited { get; set; } = false;
        public HashSet<GridDirection> Directions { get; } = new();
        public bool IsStartingNode { get; set; } = false;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}