using System.Collections;
using Core.Constants;
using Core.Entities;

namespace Core.DataStructures;

/// <summary>
/// Represents a single cell in a grid, containing a value and its coordinates.
/// </summary>
/// <typeparam name="T"> The type of the value stored in the cell. </typeparam>
public class GridCell<T> : ICloneable, IEqualityComparer
{
    private readonly Coordinate _coordinate;
    private readonly Grid<T> _parent;

    /// <summary>
    /// Initializes a new instance of the GridCell class.
    /// </summary>
    /// <param name="parent"> The parent grid. </param>
    /// <param name="value"> The value stored in the cell. </param>
    /// <param name="x"> The x-coordinate of the cell. </param>
    /// <param name="y"> The y-coordinate of the cell. </param>
    public GridCell(Grid<T> parent, T value, int x, int y)
    {
        _parent = parent;
        Value = value;
        _coordinate = new Coordinate(x, y);
    }

    /// <summary>
    /// Gets or sets the value of the cell.
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Gets the x-coordinate of the cell.
    /// </summary>
    public int X => _coordinate.X;

    /// <summary>
    /// Gets the y-coordinate of the cell.
    /// </summary>
    public int Y => _coordinate.Y;

    /// <summary>
    /// Gets the coordinate of the cell.
    /// </summary>
    public Coordinate Coordinate => _coordinate;

    public GridCell<T>[] AllNeighbors =>
    [
        ..CardinalNeighbors,
        ..DiagonalNeighbors
    ];

    public GridCell<T>[] CardinalNeighbors =>
        new[]
        {
            TryMove(GridDirection.Up, out var up) ? up : null,
            TryMove(GridDirection.Down, out var down) ? down : null,
            TryMove(GridDirection.Left, out var left) ? left : null,
            TryMove(GridDirection.Right, out var right) ? right : null
        }.Where(cell => cell != null).ToArray()!;

    public GridCell<T>[] DiagonalNeighbors =>
        new[]
        {
            TryMove(GridDirection.UpLeft, out var upLeft) ? upLeft : null,
            TryMove(GridDirection.UpRight, out var upRight) ? upRight : null,
            TryMove(GridDirection.DownLeft, out var downLeft) ? downLeft : null,
            TryMove(GridDirection.DownRight, out var downRight) ? downRight : null
        }.Where(cell => cell != null).ToArray()!;

    /// <summary>
    /// Gets the neighboring cell based on the specified x and y offsets.
    /// </summary>
    /// <param name="dx"> The x-offset. </param>
    /// <param name="dy"> The y-offset. </param>
    /// <returns> The neighboring cell. </returns>
    public GridCell<T> this[int dx, int dy] => Move(dx, dy);

    /// <summary>
    /// Gets the neighboring cell based on the specified coordinate offset.
    /// </summary>
    /// <param name="offset"> The coordinate offset. </param>
    /// <returns> The neighboring cell. </returns>
    public GridCell<T> this[Coordinate offset] => Move(offset);

    /// <summary>
    /// Gets the neighboring cell based on the specified grid direction.
    /// </summary>
    /// <param name="direction"> The grid direction. </param>
    /// <returns> The neighboring cell. </returns>
    public GridCell<T> this[GridDirection direction] => Move(direction);

    /// <summary>
    /// Creates a shallow copy of the cell.
    /// </summary>
    /// <returns> A new GridCell instance with the same data. </returns>
    public object Clone()
    {
        return new GridCell<T>(_parent, Value, X, Y);
    }

    /// <inheritdoc />
    public new bool Equals(object? x, object? y)
    {
        if (x is not GridCell<T> cellX || y is not GridCell<T> cellY) return false;
        return cellX.X == cellY.X && cellX.Y == cellY.Y && EqualityComparer<T>.Default.Equals(cellX.Value, cellY.Value);
    }

    /// <inheritdoc />
    public int GetHashCode(object obj)
    {
        return obj is not GridCell<T> cell ? 0 : HashCode.Combine(cell.X, cell.Y, cell.Value);
    }

    public override bool Equals(object? other)
    {
        return Equals(this, other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_coordinate, Value);
    }

    /// <summary>
    /// Moves to a neighboring cell based on the specified offsets.
    /// </summary>
    /// <param name="dx"> The x-offset. </param>
    /// <param name="dy"> The y-offset. </param>
    /// <returns> The neighboring cell. </returns>
    /// <exception cref="IndexOutOfRangeException"> Thrown if the target cell is out of bounds. </exception>
    public GridCell<T> Move(int dx, int dy)
    {
        var newX = X + dx;
        var newY = Y + dy;
        return _parent.IsValidCoordinate(newX, newY) ? _parent[newX, newY] : throw new IndexOutOfRangeException();
    }

    /// <summary>
    /// Moves to a neighboring cell based on the specified coordinate offset.
    /// </summary>
    /// <param name="offset"> The coordinate offset. </param>
    /// <returns> The neighboring cell. </returns>
    public GridCell<T> Move(Coordinate offset)
    {
        return Move(offset.X, offset.Y);
    }

    /// <summary>
    /// Moves to a neighboring cell based on the specified grid direction.
    /// </summary>
    /// <param name="direction"> The grid direction. </param>
    /// <returns> The neighboring cell. </returns>
    public GridCell<T> Move(GridDirection direction)
    {
        return Move(direction.GetDirectionOffset());
    }

    /// <summary>
    /// Attempts to move to a neighboring cell based on the specified offsets.
    /// </summary>
    /// <param name="dx"> The x-offset. </param>
    /// <param name="dy"> The y-offset. </param>
    /// <param name="neighbor"> The neighboring cell if the move is successful. </param>
    /// <returns> True if the move is successful; otherwise, false. </returns>
    public bool TryMove(int dx, int dy, out GridCell<T>? neighbor)
    {
        var newX = X + dx;
        var newY = Y + dy;
        if (_parent.IsValidCoordinate(newX, newY))
        {
            neighbor = _parent[newX, newY];
            return true;
        }

        neighbor = null;
        return false;
    }

    /// <summary>
    /// Attempts to move to a neighboring cell based on the specified coordinate offset.
    /// </summary>
    /// <param name="offset"> The coordinate offset. </param>
    /// <param name="neighbor"> The neighboring cell if the move is successful. </param>
    /// <returns> True if the move is successful; otherwise, false. </returns>
    public bool TryMove(Coordinate offset, out GridCell<T>? neighbor)
    {
        return TryMove(offset.X, offset.Y, out neighbor);
    }

    /// <summary>
    /// Attempts to move to a neighboring cell based on the specified grid direction.
    /// </summary>
    /// <param name="direction"> The grid direction. </param>
    /// <param name="neighbor"> The neighboring cell if the move is successful. </param>
    /// <returns> True if the move is successful; otherwise, false. </returns>
    public bool TryMove(GridDirection direction, out GridCell<T>? neighbor)
    {
        return TryMove(direction.GetDirectionOffset(), out neighbor);
    }

    /// <summary>
    /// Returns a string representation of the cell.
    /// </summary>
    /// <returns> A string containing the cell's coordinates and value. </returns>
    public override string ToString()
    {
        return $"(X:{X}, Y:{Y}) => {Value}";
    }
}