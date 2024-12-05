namespace Core.Constants;

public enum GridDirection
{
    Up,
    Down,
    Left,
    Right,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}

public static class GridDirectionsHelper
{
    public static GridDirection[] GridDirections =
    [
        GridDirection.Up,
        GridDirection.Down,
        GridDirection.Left,
        GridDirection.Right,
        GridDirection.UpLeft,
        GridDirection.UpRight,
        GridDirection.DownLeft,
        GridDirection.DownRight
    ];
    
    public static GridDirection[] CardinalDirections = new GridDirection[]
    {
        GridDirection.Up,
        GridDirection.Down,
        GridDirection.Left,
        GridDirection.Right
    };
    
    public static GridDirection[] DiagonalDirections = new GridDirection[]
    {
        GridDirection.UpLeft,
        GridDirection.UpRight,
        GridDirection.DownLeft,
        GridDirection.DownRight
    };
    
    public static (int x, int y) GetDirection(this GridDirection direction)
    {
        return direction switch
        {
            GridDirection.Up => (0, -1),
            GridDirection.Down => (0, 1),
            GridDirection.Left => (-1, 0),
            GridDirection.Right => (1, 0),
            GridDirection.UpLeft => (-1, -1),
            GridDirection.UpRight => (1, -1),
            GridDirection.DownLeft => (-1, 1),
            GridDirection.DownRight => (1, 1),
            _ => (0, 0)
        };
    }

    public static GridDirection GetOppositeDirection(this GridDirection direction)
    {
        return direction switch
        {
            GridDirection.Up => GridDirection.Down,
            GridDirection.Down => GridDirection.Up,
            GridDirection.Left => GridDirection.Right,
            GridDirection.Right => GridDirection.Left,
            GridDirection.UpLeft => GridDirection.DownRight,
            GridDirection.UpRight => GridDirection.DownLeft,
            GridDirection.DownLeft => GridDirection.UpRight,
            GridDirection.DownRight => GridDirection.UpLeft,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}