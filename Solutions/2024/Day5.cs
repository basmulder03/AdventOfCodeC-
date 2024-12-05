using Core;
using Core.DataHelper;

namespace Solutions._2024;

public class Day5 : IDay
{
    public string Part1(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var (orderingRules, rulePages) = Parse(lines);        

        var correctUpdatesCounter = 
            (from rulePage in rulePages 
                where IsValidOrdering(orderingRules, rulePage) 
                let centerIndex = (int)Math.Ceiling(rulePage.Count / 2.0) - 1 
                select rulePage[centerIndex]).Sum();

        return correctUpdatesCounter.ToString();
    }

    public string Part2(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
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

        return resultCounter.ToString();
    }

    private (Dictionary<int, List<int>>, List<List<int>>) Parse(List<string> lines)
    {
        var orderingRules = new Dictionary<int, List<int>>();
        var rulePages = new List<List<int>>();
        var parsePages = false;
        
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                parsePages = true;
                continue;
            }

            if (!parsePages)
            {
                var parts = line.Split('|');
                var first = int.Parse(parts[0]);
                var second = int.Parse(parts[1]);
                if (!orderingRules.ContainsKey(first))
                {
                    orderingRules[first] = new List<int>();
                }
                orderingRules[first].Add(second);
            }
            else
            {
                rulePages.Add(line.Split(',').Select(int.Parse).ToList());
            }
        }
        
        return (orderingRules, rulePages);
    }
    
    private static bool IsValidOrdering(Dictionary<int, List<int>> orderingRules, List<int> rulePage)
    {
        var visitedPages = new HashSet<int>();
        foreach (var update in rulePage)
        {
            visitedPages.Add(update);
            if (orderingRules.TryGetValue(update, out var mustBeBefore) && mustBeBefore.Any(visitedPages.Contains))
            {
                return false;
            }
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