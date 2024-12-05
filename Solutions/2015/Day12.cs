using System.Text.Json;
using System.Text.Json.Nodes;
using Core;
using Core.DataHelper;
using Core.StringHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Solutions._2015;

public class Day12 : IDay
{
    public int Part1(FileStream fileStream)
    {
        var json = fileStream.ReadSingleLine();
        return json.GetInts().Sum();
    }

    public int Part2(FileStream fileStream)
    {
        var jsonString = fileStream.ReadSingleLine();
        var json = JToken.Parse(jsonString);
        return GetSum(json);
    }

    private static int GetSum(JToken json)
    {
        if (json.Type == JTokenType.Object && !json.Children<JProperty>().Any(p => p.Value.Type == JTokenType.String && p.Value.ToString() == "red"))
        {
            return json.Children<JProperty>().Sum(p => GetSum(p.Value));
        }

        return json.Type switch
        {
            JTokenType.Array => json.Sum(GetSum),
            JTokenType.Integer => json.Value<int>(),
            _ => 0
        };
    }
}