using Core;
using Core.DataHelper;

namespace Solutions._2015;

public class Day2 : IDay
{
    public int Part1(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();

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

    public int Part2(FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
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