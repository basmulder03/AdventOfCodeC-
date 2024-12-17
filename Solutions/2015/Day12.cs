using Core.Extensions;
using Core.Interfaces;
using Newtonsoft.Json.Linq;

namespace Solutions._2015;

public class Day12 : IBaseDay
{
    public long Part1(string input)
    {
        return input.GetInts().Sum();
    }

    public long Part2(string input)
    {
        var json = JToken.Parse(input);
        return GetSum(json);
    }

    private static int GetSum(JToken json)
    {
        if (json.Type == JTokenType.Object && !json.Children<JProperty>()
                .Any(p => p.Value.Type == JTokenType.String && p.Value.ToString() == "red"))
            return json.Children<JProperty>().Sum(p => GetSum(p.Value));

        return json.Type switch
        {
            JTokenType.Array => json.Sum(GetSum),
            JTokenType.Integer => json.Value<int>(),
            _ => 0
        };
    }
}