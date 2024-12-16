using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2015;

public class Day2 : IBaseDay
{
    public long Part1(string input)
    {
        var lines = input.ReadLines();

        return (from line in lines
            select line.Split('x')
            into dimensions
            let l = int.Parse(dimensions[0])
            let w = int.Parse(dimensions[1])
            let h = int.Parse(dimensions[2])
            let lw = l * w
            let wh = w * h
            let hl = h * l
            let smallest = Math.Min(lw, Math.Min(wh, hl))
            select 2 * lw + 2 * wh + 2 * hl + smallest).Sum();
    }

    public long Part2(string input)
    {
        var lines = input.ReadLines();
        var total = (from line in lines
            select line.Split('x')
            into dimensions
            let l = int.Parse(dimensions[0])
            let w = int.Parse(dimensions[1])
            let h = int.Parse(dimensions[2])
            let smallestPerimeter = Math.Min(2 * l + 2 * w, Math.Min(2 * w + 2 * h, 2 * h + 2 * l))
            let volume = l * w * h
            select smallestPerimeter + volume).Sum();

        return total;
    }
}