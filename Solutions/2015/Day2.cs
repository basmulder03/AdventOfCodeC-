using Core;
using Core.DataHelper;

namespace Solutions._2015;

public class Day2 : IDay
{
    public long Part1(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var total = 0;
        
        foreach (var line in lines)
        {
            var dimensions = line.Split('x');
            var l = int.Parse(dimensions[0]);
            var w = int.Parse(dimensions[1]);
            var h = int.Parse(dimensions[2]);
            var lw = l * w;
            var wh = w * h;
            var hl = h * l;
            var smallest = Math.Min(lw, Math.Min(wh, hl));
            total += 2 * lw + 2 * wh + 2 * hl + smallest;
        }
        
        return total;
    }

    public long Part2(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var total = 0;
        
        foreach (var line in lines)
        {
            var dimensions = line.Split('x');
            var l = int.Parse(dimensions[0]);
            var w = int.Parse(dimensions[1]);
            var h = int.Parse(dimensions[2]);
            var smallestPerimeter = Math.Min(2 * l + 2 * w, Math.Min(2 * w + 2 * h, 2 * h + 2 * l));
            var volume = l * w * h;
            total += smallestPerimeter + volume;
        }
        
        return total;
    }
}