using System.Collections;
using Core.Constants;

namespace Core.DataStructures;

public class GridCell<T>(Grid<T> parent, T? value, int x, int y) : ICloneable, IEqualityComparer
{
    public T? Value { get; set; } = value;

    public int X { get; } = x;

    public int Y { get; } = y;

    public bool HasValue => IsValidCoordinate(X, Y) && Value != null;
    public bool IsEmpty => !HasValue;

    public GridCell<T> Up => GetNeighbor(GridDirection.Up);
    public GridCell<T> Down => GetNeighbor(GridDirection.Down);
    public GridCell<T> Left => GetNeighbor(GridDirection.Left);
    public GridCell<T> Right => GetNeighbor(GridDirection.Right);
    public GridCell<T> UpLeft => GetNeighbor(GridDirection.UpLeft);
    public GridCell<T> UpRight => GetNeighbor(GridDirection.UpRight);
    public GridCell<T> DownLeft => GetNeighbor(GridDirection.DownLeft);
    public GridCell<T> DownRight => GetNeighbor(GridDirection.DownRight);

    public GridCell<T> this[GridDirection direction] => GetNeighbor(direction);

    public GridCell<T>[] AllNeighbors =>
        new List<GridCell<T>> { Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight }
            .Where(cell => cell.HasValue).ToArray();

    public GridCell<T>[] CardinalNeighbors =>
        new List<GridCell<T>> { Up, Down, Left, Right }.Where(cell => cell.HasValue).ToArray();

    public GridCell<T>[] DiagonalNeighbors => new List<GridCell<T>> { UpLeft, UpRight, DownLeft, DownRight }
        .Where(cell => cell.HasValue).ToArray();

    public object Clone()
    {
        return Clone(parent);
    }

    public new bool Equals(object? x, object? y)
    {
        return x is GridCell<T> cellX && y is GridCell<T> cellY && cellX.X == cellY.X && cellX.Y == cellY.Y
               && (cellX.Value?.Equals(cellY.Value) ?? cellY.Value == null);
    }

    public int GetHashCode(object obj)
    {
        if (obj is not GridCell<T> cell) return 0;
        var valueHashCode = cell.Value?.GetHashCode() ?? -1;
        return cell.X ^ cell.Y ^ valueHashCode;
    }

    private static GridCell<T> Empty(Grid<T> parent, int x, int y)
    {
        return new GridCell<T>(parent, default, x, y);
    }

    private GridCell<T> GetNeighbor(GridDirection direction)
    {
        var (dx, dy) = direction.GetDirection();
        var newX = X + dx;
        var newY = Y + dy;
        return !IsValidCoordinate(newX, newY) ? Empty(parent, newX, newY) : parent[newX, newY];
    }

    public GridCell<T> Move(int xDiff, int yDiff)
    {
        var newX = X + xDiff;
        var newY = Y + yDiff;

        return !IsValidCoordinate(newX, newY) ? Empty(parent, newX, newY) : parent[newX, newY];
    }

    public object Clone(Grid<T> newParent)
    {
        var val = Value is ICloneable cloneable ? (T)cloneable.Clone() : Value;
        return new GridCell<T>(newParent, Value != null ? val : default, X, Y);
    }

    private bool IsValidCoordinate(int x, int y)
    {
        return x >= 0 && x < parent.Width && y >= 0 && y < parent.Height;
    }

    public static bool operator ==(GridCell<T> a, GridCell<T> b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(GridCell<T> a, GridCell<T> b)
    {
        return !a.Equals(b);
    }

    public override string ToString()
    {
        return $"X: {X}, Y: {Y}, Value: {Value}";
    }
}