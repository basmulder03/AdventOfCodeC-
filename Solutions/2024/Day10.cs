﻿using Core;
using Core.DataHelper;
using Core.DataStructures;

namespace Solutions._2024;

public class Day10 : IDay
{
    public long Part1(FileStream fileStream)
    {
        var grid = Parse(fileStream);
        var startingCells = grid.Where(cell => cell.Value!.Height == 0).ToList();
        return startingCells.Sum(cell => DFS((Grid<Node>)grid.Clone(), cell));
    }

    public long Part2(FileStream fileStream)
    {
        var grid = Parse(fileStream);
        var startingCells = grid.Where(cell => cell.Value!.Height == 0).ToList();
        return startingCells.Sum(cell => DFS2((Grid<Node>)grid.Clone(), cell));
    }
    
    private static Grid<Node> Parse(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        return Grid<Node>.Parse(lines, s => s.ToCharArray().Select(c => new Node(c - '0', false)));
    }

    private static int DFS(Grid<Node> grid, GridCell<Node> cell)
    {
        var c = grid[cell.X, cell.Y];
        if (c.Value!.Visited || (c.Value.Visited && c.Value!.Height == 0)) return 0;
        c.Value!.Visited = true;
        if (c.Value!.Height == 9) return 1;
        return c.CardinalNeighbors.Where(neighbor => !neighbor.Value!.Visited && neighbor.Value!.Height - c.Value!.Height == 1)
                                  .Sum(neighbor => DFS(grid, neighbor));
    }

    private static int DFS2(Grid<Node> grid, GridCell<Node> cell)
    {
        var c = grid[cell.X, cell.Y];
        c.Value!.Visited = true;
        if (c.Value!.Height == 9) return 1;
        return c.CardinalNeighbors.Where(neighbor => neighbor.Value!.Height - c.Value!.Height == 1)
                                  .Sum(neighbor => DFS2(grid, neighbor));
    }

    private class Node(int height, bool visited) : ICloneable
    {
        public int Height { get; init; } = height;
        public bool Visited { get; set; } = visited;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}