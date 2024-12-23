﻿using Core.DataStructures;
using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2024;

public class Day10 : BaseDay
{
    public override long Part1(string input)
    {
        var grid = Parse(input);
        var startingCells = grid.Where(cell => cell.Value!.Height == 0).ToList();
        return startingCells.Sum(cell => DFS((Grid<Node>)grid.Clone(), cell));
    }

    public override long Part2(string input)
    {
        var grid = Parse(input);
        var startingCells = grid.Where(cell => cell.Value!.Height == 0).ToList();
        return startingCells.Sum(cell => DFS2((Grid<Node>)grid.Clone(), cell));
    }

    private static Grid<Node> Parse(string input)
    {
        var lines = input.ReadLines();
        var data = lines.Select(line => line.ToCharArray().Select(c => new Node(c - '0', false)).ToArray()).ToArray();
        return Grid<Node>.FromData(data);
    }

    private static int DFS(Grid<Node> grid, GridCell<Node> cell)
    {
        var c = grid[cell.X, cell.Y];
        if (c.Value!.Visited || (c.Value.Visited && c.Value!.Height == 0)) return 0;
        c.Value!.Visited = true;
        if (c.Value!.Height == 9) return 1;
        return c.CardinalNeighbors.Where(neighbor =>
                neighbor.Value != null && !neighbor.Value!.Visited && neighbor.Value!.Height - c.Value!.Height == 1)
            .Sum(neighbor => DFS(grid, neighbor));
    }

    private static int DFS2(Grid<Node> grid, GridCell<Node> cell)
    {
        var c = grid[cell.X, cell.Y];
        c.Value!.Visited = true;
        if (c.Value!.Height == 9) return 1;
        return c.CardinalNeighbors
            .Where(neighbor => neighbor.Value != null && neighbor.Value!.Height - c.Value!.Height == 1)
            .Sum(neighbor => DFS2(grid, neighbor));
    }

    private class Node : ICloneable
    {
        public Node(int height, bool visited)
        {
            Height = height;
            Visited = visited;
        }

        public int Height { get; }
        public bool Visited { get; set; }

        public object Clone()
        {
            return new Node(Height, Visited);
        }
    }
}