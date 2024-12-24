using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2024;

public class Day5 : BaseDay
{
    public override long Part1(string input)
    {
        var lines = input.ReadGroups();
        var (orderingRules, rulePages) = Parse(lines);

        var correctUpdatesCounter =
            (from rulePage in rulePages
                where IsValidOrdering(orderingRules, rulePage)
                let centerIndex = (int)Math.Ceiling(rulePage.Count / 2.0) - 1
                select rulePage[centerIndex]).Sum();

        return correctUpdatesCounter;
    }

    public override long Part2(string input)
    {
        var lines = input.ReadGroups();
        var (orderingRules, rulePages) = Parse(lines);

        var resultCounter = (
            from rulePage in rulePages
            select rulePage.ToList()
            into page
            where !IsValidOrdering(orderingRules, page)
            select CorrectOrder(orderingRules, page)
            into page
            let centerIndex = (int)Math.Ceiling(page.Count / 2.0) - 1
            select page[centerIndex]).Sum();

        return resultCounter;
    }

    private static (Dictionary<int, List<int>>, List<List<int>>) Parse(List<List<string>> lines)
    {
        var orderingRules = new Dictionary<int, List<int>>();
        var rulePages = new List<List<int>>();

        foreach (var line in lines[0])
        {
                var parts = line.Split('|');
                var first = int.Parse(parts[0]);
                var second = int.Parse(parts[1]);
                if (!orderingRules.TryGetValue(first, out var value))
                {
                    value = [];
                    orderingRules[first] = value;
                }

                value.Add(second);
        }

        foreach (var line in lines[1])
        {
            rulePages.Add(line.Split(',').Select(int.Parse).ToList());
        }

        return (orderingRules, rulePages);
    }

    private static bool IsValidOrdering(Dictionary<int, List<int>> orderingRules, List<int> rulePage)
    {
        var visitedPages = new HashSet<int>();
        foreach (var update in rulePage)
        {
            visitedPages.Add(update);
            if (orderingRules.TryGetValue(update, out var mustBeBefore) &&
                mustBeBefore.Any(visitedPages.Contains)) return false;
        }

        return true;
    }

    private static List<int> CorrectOrder(Dictionary<int, List<int>> orderingRules, List<int> page)
    {
        var visitedPages = new HashSet<int>();
        var correctOrder = new List<int>();
        foreach (var update in page)
        {
            visitedPages.Add(update);
            if (orderingRules.TryGetValue(update, out var mustBeBefore) && mustBeBefore.Any(visitedPages.Contains))
            {
                var index = correctOrder.FindIndex(x => mustBeBefore.Contains(x));
                correctOrder.Insert(index, update);
            }
            else
            {
                correctOrder.Add(update);
            }
        }

        return correctOrder;
    }
}