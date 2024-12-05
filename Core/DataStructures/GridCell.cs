using Core.Constants;

namespace Core.DataStructures;

public class GridCell<T>(Grid<T> parent, T value, int x, int y)
{
    private readonly Grid<T> _parent = parent;
    private readonly int _x = x;
    private readonly int _y = y;
    
    public T Value { get; set; } = value;
    public int X => _x;
    public int Y => _y;
    
    private GridCell<T?> GetNeighbor(GridDirections direction)
    {
        var (dx, dy) = GridDirectionsHelper.GetDirection(direction);
        var newX = _x + dx;
        var newY = _y + dy;
        if (newX < 0 || newX >= _parent.Width || newY < 0 || newY >= _parent.Height)
        {
            return new GridCell<T?>(_parent!, default, _x, _y);
        }
        return _parent[newX, newY]!;
    }
    
    public GridCell<T?> Up => GetNeighbor(GridDirections.Up);
    public GridCell<T?> Down => GetNeighbor(GridDirections.Down);
    public GridCell<T?> Left => GetNeighbor(GridDirections.Left);
    public GridCell<T?> Right => GetNeighbor(GridDirections.Right);
    public GridCell<T?> UpLeft => GetNeighbor(GridDirections.UpLeft);
    public GridCell<T?> UpRight => GetNeighbor(GridDirections.UpRight);
    public GridCell<T?> DownLeft => GetNeighbor(GridDirections.DownLeft);
    public GridCell<T?> DownRight => GetNeighbor(GridDirections.DownRight);
}