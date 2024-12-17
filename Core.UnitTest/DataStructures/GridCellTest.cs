using Core.Constants;
using Core.DataStructures;
using Shouldly;

namespace Core.UnitTest.DataStructures;

[TestClass]
public class GridCellTests
{
    private Grid<int> _grid = null!;

    [TestInitialize]
    public void Setup()
    {
        var data = new[]
        {
            new[] { 1, 2, 3 },
            new[] { 4, 5, 6 },
            new[] { 7, 8, 9 }
        };
        _grid = Grid<int>.FromData(data);
    }

    [TestMethod]
    public void GridCell_ShouldInitializeCorrectly()
    {
        // Arrange
        var cell = _grid[1, 1];

        // Assert
        cell.X.ShouldBe(1);
        cell.Y.ShouldBe(1);
        cell.Value.ShouldBe(5);
    }

    [TestMethod]
    public void GridCell_ShouldReturnCardinalNeighbors()
    {
        // Arrange
        var cell = _grid[1, 1];

        // Act
        var neighbors = cell.CardinalNeighbors;

        // Assert
        neighbors.Length.ShouldBe(4);
        neighbors.ShouldContain(c => c.Value == 2); // Up
        neighbors.ShouldContain(c => c.Value == 4); // Left
        neighbors.ShouldContain(c => c.Value == 6); // Right
        neighbors.ShouldContain(c => c.Value == 8); // Down
    }

    [TestMethod]
    public void GridCell_ShouldReturnDiagonalNeighbors()
    {
        // Arrange
        var cell = _grid[1, 1];

        // Act
        var neighbors = cell.DiagonalNeighbors;

        // Assert
        neighbors.Length.ShouldBe(4);
        neighbors.ShouldContain(c => c.Value == 1); // UpLeft
        neighbors.ShouldContain(c => c.Value == 3); // UpRight
        neighbors.ShouldContain(c => c.Value == 7); // DownLeft
        neighbors.ShouldContain(c => c.Value == 9); // DownRight
    }

    [TestMethod]
    public void GridCell_ShouldMoveInValidDirection()
    {
        // Arrange
        var cell = _grid[1, 1];

        // Act
        var movedCell = cell.Move(GridDirection.Up);

        // Assert
        movedCell.Value.ShouldBe(2);
        movedCell.X.ShouldBe(1);
        movedCell.Y.ShouldBe(0);
    }

    [TestMethod]
    public void GridCell_ShouldThrowOnInvalidMove()
    {
        // Arrange
        var cell = _grid[0, 0];

        // Act & Assert
        Should.Throw<IndexOutOfRangeException>(() => cell.Move(GridDirection.UpLeft));
    }

    [TestMethod]
    public void GridCell_TryMove_ShouldReturnTrueForValidMove()
    {
        // Arrange
        var cell = _grid[1, 1];

        // Act
        var success = cell.TryMove(GridDirection.Left, out var neighbor);

        // Assert
        success.ShouldBeTrue();
        neighbor.ShouldNotBeNull();
        neighbor.Value.ShouldBe(4);
    }

    [TestMethod]
    public void GridCell_TryMove_ShouldReturnFalseForInvalidMove()
    {
        // Arrange
        var cell = _grid[0, 0];

        // Act
        var success = cell.TryMove(GridDirection.UpLeft, out var neighbor);

        // Assert
        success.ShouldBeFalse();
        neighbor.ShouldBeNull();
    }

    [TestMethod]
    public void GridCell_Clone_ShouldCreateIdenticalCell()
    {
        // Arrange
        var originalCell = _grid[1, 1];

        // Act
        var clonedCell = (GridCell<int>)originalCell.Clone();

        // Assert
        clonedCell.ShouldNotBeSameAs(originalCell);
        clonedCell.X.ShouldBe(originalCell.X);
        clonedCell.Y.ShouldBe(originalCell.Y);
        clonedCell.Value.ShouldBe(originalCell.Value);
    }

    [TestMethod]
    public void GridCell_Equals_ShouldReturnTrueForIdenticalCells()
    {
        // Arrange
        var cell1 = _grid[1, 1];
        var cell2 = _grid[1, 1];

        // Act & Assert
        cell1.Equals(cell2).ShouldBeTrue();
    }

    [TestMethod]
    public void GridCell_Equals_ShouldReturnFalseForDifferentCells()
    {
        // Arrange
        var cell1 = _grid[1, 1];
        var cell2 = _grid[0, 0];

        // Act & Assert
        cell1.Equals(cell2).ShouldBeFalse();
    }

    [TestMethod]
    public void GridCell_ToString_ShouldReturnFormattedString()
    {
        // Arrange
        var cell = _grid[1, 1];

        // Act
        var result = cell.ToString();

        // Assert
        result.ShouldBe("(X:1, Y:1) => 5");
    }
}