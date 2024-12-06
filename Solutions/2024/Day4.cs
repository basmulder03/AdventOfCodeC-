using Core;
using Core.Constants;
using Core.DataHelper;
using Core.DataStructures;

namespace Solutions._2024;

public class Day4 : IDay
{
    public int Part1(FileStream fileStream)
    {
        var grid = Parse(fileStream.ReadLines());

        return (
            from row in grid.Rows 
            from column in row 
            where column.HasValue && column.Value == 'X' 
            select FindXmas(column)).Sum();
    }

    public int Part2(FileStream fileStream)
    {
        var grid = Parse(fileStream.ReadLines());
        return (from row in grid.Rows from cell in row where cell.HasValue && cell.Value == 'A' select FindXMas(cell)).Sum();
    }

    private static Grid<char> Parse(List<string> lines)
    {
        return Grid<char>.Parse(lines, str => str.ToCharArray());
    }
    
    private static int FindXmas(GridCell<char> cell)
    {
        var xmasCounter = 0;
        const string word = "XMAS";

        foreach (var direction in GridDirectionsHelper.GridDirections)
        {
            var currentCell = (GridCell<char>)cell.Clone();
            var foundWord = true;
            foreach (var letter in word)
            {
                if (currentCell.HasValue && currentCell.Value == letter)
                {
                    currentCell = currentCell[direction];
                }
                else
                {
                    foundWord = false;
                    break;
                }
            }

            if (!foundWord) continue;
            xmasCounter++;
        }

        return xmasCounter;
    }

    private static int FindXMas(GridCell<char> cell)
    {
        var xmasCounter = GridDirectionsHelper.DiagonalDirections.Count(direction => cell[direction].HasValue && cell[direction].Value == 'M' && cell[direction.GetOppositeDirection()].HasValue && cell[direction.GetOppositeDirection()].Value == 'S');
        return xmasCounter > 1 ? 1 : 0;
    }
}