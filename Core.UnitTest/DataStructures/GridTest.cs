using Core.DataStructures;
using Core.Entities;
using Shouldly;

namespace Core.UnitTest.DataStructures;

[TestClass]
public class GridTests
{
    [TestMethod]
    public void Grid_ShouldInitializeWithData()
    {
        // Arrange
        var data = new[]
        {
            new[] { 1, 2, 3 },
            new[] { 4, 5, 6 },
            new[] { 7, 8, 9 }
        };

        // Act
        var grid = Grid<int>.FromData(data);

        // Assert
        grid.Width.ShouldBe(3);
        grid.Height.ShouldBe(3);
        grid[0, 0].Value.ShouldBe(1);
        grid[2, 2].Value.ShouldBe(9);
    }

    [TestMethod]
    public void Grid_ShouldThrowException_ForInvalidCoordinate()
    {
        // Arrange
        var grid = Grid<int>.FromData(3, 3, 0);

        // Act & Assert
        Should.Throw<IndexOutOfRangeException>(() => grid[3, 3]);
    }

    [TestMethod]
    public void Grid_ShouldCloneProperly()
    {
        // Arrange
        var grid = Grid<int>.FromData([
            [1, 2],
            [3, 4]
        ]);

        // Act
        var clone = (Grid<int>)grid.Clone();

        // Assert
        clone.ShouldNotBeSameAs(grid);
        clone.Equals(grid).ShouldBeTrue();
        clone[0, 0].Value.ShouldBe(1);
    }

    [TestMethod]
    public void Grid_Transpose_ShouldSwapRowsAndColumns()
    {
        // Arrange
        var grid = Grid<int>.FromData([
            [1, 2, 3],
            [4, 5, 6]
        ]);

        // Act
        var transposed = grid.Transpose();

        // Assert
        transposed.Width.ShouldBe(2);
        transposed.Height.ShouldBe(3);
        transposed[0, 0].Value.ShouldBe(1);
        transposed[1, 0].Value.ShouldBe(4);
        transposed[1, 2].Value.ShouldBe(6);
    }

    [TestMethod]
    public void Grid_Generate_ShouldCreateCustomGrid()
    {
        // Arrange & Act
        var grid = Grid<Coordinate>.Generate(3, 2, (x, y) => new Coordinate(x, y));

        // Assert
        grid.Width.ShouldBe(3);
        grid.Height.ShouldBe(2);
        grid[0, 0].Value.ShouldBe(new Coordinate(0, 0));
        grid[1, 0].Value.ShouldBe(new Coordinate(1, 0));
        grid[2, 1].Value.ShouldBe(new Coordinate(2, 1));
    }

    [TestMethod]
    public void Grid_ToString_ShouldFormatCorrectly()
    {
        // Arrange
        var grid = Grid<int>.FromData([
            [1, 2],
            [3, 4]
        ]);

        // Act
        var result = grid.ToString(value => value.ToString());

        // Assert
        result.ShouldBe("12\n34\n", StringCompareShould.IgnoreLineEndings);
    }

    [TestMethod]
    public void Grid_Equals_ShouldReturnTrueForIdenticalGrids()
    {
        // Arrange
        var grid1 = Grid<int>.FromData([
            [1, 2, 3],
            [4, 5, 6]
        ]);

        var grid2 = Grid<int>.FromData([
            [1, 2, 3],
            [4, 5, 6]
        ]);

        // Act & Assert
        grid1.Equals(grid2).ShouldBeTrue();
    }

    [TestMethod]
    public void Grid_Equals_ShouldReturnFalseForDifferentGrids()
    {
        // Arrange
        var grid1 = Grid<int>.FromData([
            [1, 2, 3],
            [4, 5, 6]
        ]);

        var grid2 = Grid<int>.FromData([
            [1, 2, 0],
            [4, 5, 6]
        ]);

        // Act & Assert
        grid1.Equals(grid2).ShouldBeFalse();
    }
}