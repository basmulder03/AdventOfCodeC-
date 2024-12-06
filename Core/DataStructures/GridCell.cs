using Core.Constants;

namespace Core.DataStructures;

public class GridCell<T>(Grid<T> parent, T? value, int x, int y) : ICloneable
{
    public T? Value { get; set; } = value;
    
    public int X { get; } = x;

    public int Y { get; } = y;
    
    public bool HasValue => Value != null;
    public bool IsEmpty => !HasValue;

    private GridCell<T> GetNeighbor(GridDirection direction)
    {
        var (dx, dy) = direction.GetDirection();
        var newX = X + dx;
        var newY = Y + dy;
        if (newX < 0 || newX >= parent.Width || newY < 0 || newY >= parent.Height)
        {
            return new GridCell<T>(parent, default, X, Y);
        }
        return parent[newX, newY];
    }
    
    public GridCell<T> Up => GetNeighbor(GridDirection.Up);
    public GridCell<T> Down => GetNeighbor(GridDirection.Down);
    public GridCell<T> Left => GetNeighbor(GridDirection.Left);
    public GridCell<T> Right => GetNeighbor(GridDirection.Right);
    public GridCell<T> UpLeft => GetNeighbor(GridDirection.UpLeft);
    public GridCell<T> UpRight => GetNeighbor(GridDirection.UpRight);
    public GridCell<T> DownLeft => GetNeighbor(GridDirection.DownLeft);
    public GridCell<T> DownRight => GetNeighbor(GridDirection.DownRight);
    
    public GridCell<T> this[GridDirection direction] => GetNeighbor(direction);
    public GridCell<T>[] AllNeighbors => new List<GridCell<T>>{Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight}.Where(cell => cell.HasValue).ToArray();
    public GridCell<T>[] CardinalNeighbors => new List<GridCell<T>>{Up, Down, Left, Right}.Where(cell => cell.HasValue).ToArray();
    public GridCell<T>[] DiagonalNeighbors => new List<GridCell<T>>{UpLeft, UpRight, DownLeft, DownRight}.Where(cell => cell.HasValue).ToArray();
    
    public object Clone()
    {
        var val = Value is ICloneable cloneable ? (T)cloneable.Clone() : Value;
        return new GridCell<T>((Grid<T>)parent.Clone(), Value != null ? val : default, X, Y);
    }
}