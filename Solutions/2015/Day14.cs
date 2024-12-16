using Core.Extensions;
using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2015;

public class Day14 : IBaseDay
{
    public long Part1(string input)
    {
        var lines = input.ReadLines();
        var reindeer = ParseReindeer(lines);
        return reindeer.Values.Select(r => CalculateDistance(r, 2503)).Max();
    }

    public long Part2(string input)
    {
        var lines = input.ReadLines();
        var reindeer = ParseReindeer(lines);
        var scores = new Dictionary<string, int>();
        for (var i = 1; i <= 2503; i++)
        {
            var distances = reindeer.ToDictionary(r => r.Key, r => CalculateDistance(r.Value, i));
            var maxDistance = distances.Values.Max();
            foreach (var (name, distance) in distances)
                if (distance == maxDistance)
                    scores[name] = scores.GetValueOrDefault(name) + 1;
        }

        return scores.Values.Max();
    }

    private static Dictionary<string, Dictionary<string, int>> ParseReindeer(List<string> lines)
    {
        var dict = new Dictionary<string, Dictionary<string, int>>();
        foreach (var line in lines)
        {
            var parts = line.Split(" ");
            var name = parts[0];
            dict[name] = new Dictionary<string, int>
            {
                ["speed"] = int.Parse(parts[3]),
                ["flyTime"] = int.Parse(parts[6]),
                ["restTime"] = int.Parse(parts[13])
            };
        }

        return dict;
    }

    private static int CalculateDistance(Dictionary<string, int> reindeer, int time)
    {
        var currentTime = 0;
        var speed = reindeer["speed"];
        var flyTime = reindeer["flyTime"];
        var restTime = reindeer["restTime"];
        var distance = 0;
        while (currentTime < time)
        {
            // Fly
            var timeLeft = time - currentTime;
            var timeToFly = flyTime.ButNotGreaterThan(timeLeft);
            distance += speed * timeToFly;
            currentTime += timeToFly;

            // Break if time is up
            if (currentTime >= time) break;

            // Rest
            timeLeft = time - currentTime;
            var timeToRest = restTime.ButNotGreaterThan(timeLeft);
            currentTime += timeToRest;

            // Break if time is up
            if (currentTime >= time) break;
        }

        return distance;
    }
}