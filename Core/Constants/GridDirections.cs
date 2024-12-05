namespace Core.Constants;

public enum GridDirections
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
    public static (int x, int y) GetDirection(GridDirections direction)
    {
        return direction switch
        {
            GridDirections.Up => (0, -1),
            GridDirections.Down => (0, 1),
            GridDirections.Left => (-1, 0),
            GridDirections.Right => (1, 0),
            GridDirections.UpLeft => (-1, -1),
            GridDirections.UpRight => (1, -1),
            GridDirections.DownLeft => (-1, 1),
            GridDirections.DownRight => (1, 1),
            _ => (0, 0)
        };
    }
}