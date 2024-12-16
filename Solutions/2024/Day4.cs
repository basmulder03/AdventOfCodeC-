using Core.Constants;
using Core.DataStructures;
using Core.InputHelpers;
using Core.Interfaces;

namespace Solutions._2024;

public class Day4 : IBaseDay
{
    public long Part1(string input)
    {
        var grid = Parse(input.ReadLines());

        return (
            from cell in grid
            where cell.Value == 'X'
            select FindXmas(cell)).Sum();
    }

    public long Part2(string input)
    {
        var grid = Parse(input.ReadLines());

        return (
            from cell in grid
            where cell.Value == 'A'
            select FindXMas(cell)).Sum();
    }

    private static Grid<char> Parse(IEnumerable<string> lines)
    {
        var data = lines.Select(str => str.ToCharArray()).ToArray();
        return Grid<char>.FromData(data);
    }

    private static int FindXmas(GridCell<char> cell)
    {
        var xmasCounter = 0;
        const string word = "MAS";

        foreach (var direction in GridDirectionsHelper.AllDirections)
        {
            var currentCell = cell;
            var foundWord = true;
            foreach (var letter in word)
            {
                if (!currentCell.TryMove(direction, out var nextCell))
                {
                    foundWord = false;
                    break;
                }

                if (letter != nextCell!.Value)
                {
                    foundWord = false;
                    break;
                }

                currentCell = nextCell;
            }

            if (foundWord) xmasCounter++;
        }

        return xmasCounter;
    }

    private static int FindXMas(GridCell<char> cell)
    {
        var xmasCounter = 0;
        foreach (var direction in GridDirectionsHelper.DiagonalDirections)
            if (cell.TryMove(direction, out var nextCell) &&
                nextCell!.Value == 'M' &&
                cell.TryMove(direction.GetOppositeDirection(), out var oppositeCell) &&
                oppositeCell!.Value == 'S')
                xmasCounter++;

        return xmasCounter > 1 ? 1 : 0;
    }
}