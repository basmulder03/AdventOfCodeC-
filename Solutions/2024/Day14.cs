using Core.Algorithms;
using Core.Entities;
using Core.Extensions;
using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2024;

public class Day14 : BaseDay
{
    public static int GridWidth { get; set; } = 101;
    public static int GridHeight { get; set; } = 103;

    public override long Part1(string input)
    {
        var robots = ParseInput(input);
        var movedRobots = robots.Select(robot => CalculatePosition(robot.Item1, robot.Item2, 100))
            .ToList();
        var groupedRobots = movedRobots.GroupBy(position => position)
            .ToDictionary(group => group.Key, group => group.Count());

        var xMidpoint = Math.Floor(GridWidth / 2f);
        var yMidpoint = Math.Floor(GridHeight / 2f);

        var quadrant1Count = CountRobotsInQuadrant(c => c.X < xMidpoint && c.Y < yMidpoint);
        var quadrant2Count = CountRobotsInQuadrant(c => c.X > xMidpoint && c.Y < yMidpoint);
        var quadrant3Count = CountRobotsInQuadrant(c => c.X > xMidpoint && c.Y > yMidpoint);
        var quadrant4Count = CountRobotsInQuadrant(c => c.X < xMidpoint && c.Y > yMidpoint);

        return quadrant1Count * quadrant2Count * quadrant3Count * quadrant4Count;

        int CountRobotsInQuadrant(Func<Coordinate, bool> predicate)
        {
            return groupedRobots.Where(group => predicate(group.Key)).Sum(group => group.Value);
        }
    }

    public override long Part2(string input)
    {
        var robots = ParseInput(input);
        var xCycleStep = FindMaxDensityStep(robots, GridWidth, position => position.X);
        var yCycleStep = FindMaxDensityStep(robots, GridHeight, position => position.Y);

        List<ChineseRemainderTheoremTerm> crtParameters =
        [
            new(xCycleStep, GridWidth),
            new(yCycleStep, GridHeight)
        ];

        var alignmentStep = ChineseRemainderTheorem.Solve(crtParameters);
        return alignmentStep;
    }

    private static List<(Coordinate, Coordinate)> ParseInput(string input)
    {
        var lines = input.ReadLines();
        return lines.Select(line =>
        {
            var values = line.GetInts();
            var initialPosition = new Coordinate(values[0], values[1]);
            var velocity = new Coordinate(values[2], values[3]);
            return (initialPosition, velocity);
        }).ToList();
    }

    private static Coordinate CalculatePosition(Coordinate position, Coordinate velocity, int time)
    {
        var newPosition = position + velocity * time;
        return new Coordinate(newPosition.X.Modulo(GridWidth), newPosition.Y.Modulo(GridHeight));
    }

    private static int FindMaxDensityStep(List<(Coordinate, Coordinate)> robots, int cycleLength,
        Func<Coordinate, int> coordinateSelector)
    {
        var maxDensity = 0;
        var maxDensityStep = 0;

        for (var step = 0; step < cycleLength; step++)
        {
            var densityBuckets = new int[cycleLength];
            foreach (var position in robots.Select(robot => CalculatePosition(robot.Item1, robot.Item2, step)))
            {
                densityBuckets[coordinateSelector(position)]++;
            }

            var currentMaxDensity = densityBuckets.Max();
            if (currentMaxDensity <= maxDensity) continue;
            maxDensity = currentMaxDensity;
            maxDensityStep = step;
        }

        return maxDensityStep;
    }
}