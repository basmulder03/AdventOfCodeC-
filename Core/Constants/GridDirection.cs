using Core.Entities;

namespace Core.Constants;

/// <summary>
/// Represents possible directions in a 2D grid.
/// </summary>
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

/// <summary>
/// Provides helper methods and constants for grid directions.
/// </summary>
public static class GridDirectionsHelper
{
    private static readonly Dictionary<GridDirection, Coordinate> DirectionOffsets = new()
    {
        { GridDirection.Up, new Coordinate(0, -1) },
        { GridDirection.Down, new Coordinate(0, 1) },
        { GridDirection.Left, new Coordinate(-1, 0) },
        { GridDirection.Right, new Coordinate(1, 0) },
        { GridDirection.UpLeft, new Coordinate(-1, -1) },
        { GridDirection.UpRight, new Coordinate(1, -1) },
        { GridDirection.DownLeft, new Coordinate(-1, 1) },
        { GridDirection.DownRight, new Coordinate(1, 1) }
    };

    /// <summary>
    /// Gets all possible directions in the grid.
    /// </summary>
    public static readonly GridDirection[] AllDirections = Enum.GetValues<GridDirection>();

    /// <summary>
    /// Gets all cardinal directions in the grid.
    /// </summary>
    public static readonly GridDirection[] CardinalDirections =
    [
        GridDirection.Up, GridDirection.Down, GridDirection.Left, GridDirection.Right
    ];

    /// <summary>
    /// Gets all diagonal directions in the grid.
    /// </summary>
    public static readonly GridDirection[] DiagonalDirections =
    [
        GridDirection.UpLeft, GridDirection.UpRight, GridDirection.DownLeft, GridDirection.DownRight
    ];

    /// <summary>
    /// Gets the coordinate offset corresponding to the specified direction.
    /// </summary>
    /// <param name="direction">The grid direction.</param>
    /// <returns>A <see cref="Coordinate"/> representing the offset.</returns>
    public static Coordinate GetDirectionOffset(this GridDirection direction)
    {
        return DirectionOffsets[direction];
    }

    /// <summary>
    /// Gets the opposite direction of the specified direction.
    /// </summary>
    /// <param name="direction">The grid direction.</param>
    /// <returns>The opposite <see cref="GridDirection"/>.</returns>
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

    /// <summary>
    /// Rotates the direction 90 degrees clockwise.
    /// </summary>
    /// <param name="direction">The grid direction.</param>
    /// <returns>The rotated <see cref="GridDirection"/>.</returns>
    public static GridDirection Rotate90DegreesClockwise(this GridDirection direction)
    {
        return direction switch
        {
            GridDirection.Up => GridDirection.Right,
            GridDirection.Right => GridDirection.Down,
            GridDirection.Down => GridDirection.Left,
            GridDirection.Left => GridDirection.Up,
            GridDirection.UpLeft => GridDirection.UpRight,
            GridDirection.UpRight => GridDirection.DownRight,
            GridDirection.DownRight => GridDirection.DownLeft,
            GridDirection.DownLeft => GridDirection.UpLeft,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    /// <summary>
    /// Rotates the direction 90 degrees counterclockwise.
    /// </summary>
    /// <param name="direction">The grid direction.</param>
    /// <returns>The rotated <see cref="GridDirection"/>.</returns>
    public static GridDirection Rotate90DegreesCounterClockwise(this GridDirection direction)
    {
        return direction switch
        {
            GridDirection.Up => GridDirection.Left,
            GridDirection.Left => GridDirection.Down,
            GridDirection.Down => GridDirection.Right,
            GridDirection.Right => GridDirection.Up,
            GridDirection.UpLeft => GridDirection.DownLeft,
            GridDirection.DownLeft => GridDirection.DownRight,
            GridDirection.DownRight => GridDirection.UpRight,
            GridDirection.UpRight => GridDirection.UpLeft,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    /// <summary>
    /// Rotates the direction 45 degrees clockwise.
    /// </summary>
    /// <param name="direction">The grid direction.</param>
    /// <returns>The rotated <see cref="GridDirection"/>.</returns>
    public static GridDirection Rotate45DegreesClockwise(this GridDirection direction)
    {
        return direction switch
        {
            GridDirection.Up => GridDirection.UpRight,
            GridDirection.UpRight => GridDirection.Right,
            GridDirection.Right => GridDirection.DownRight,
            GridDirection.DownRight => GridDirection.Down,
            GridDirection.Down => GridDirection.DownLeft,
            GridDirection.DownLeft => GridDirection.Left,
            GridDirection.Left => GridDirection.UpLeft,
            GridDirection.UpLeft => GridDirection.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    /// <summary>
    /// Rotates the direction 45 degrees counterclockwise.
    /// </summary>
    /// <param name="direction">The grid direction.</param>
    /// <returns>The rotated <see cref="GridDirection"/>.</returns>
    public static GridDirection Rotate45DegreesCounterClockwise(this GridDirection direction)
    {
        return direction switch
        {
            GridDirection.Up => GridDirection.UpLeft,
            GridDirection.UpLeft => GridDirection.Left,
            GridDirection.Left => GridDirection.DownLeft,
            GridDirection.DownLeft => GridDirection.Down,
            GridDirection.Down => GridDirection.DownRight,
            GridDirection.DownRight => GridDirection.Right,
            GridDirection.Right => GridDirection.UpRight,
            GridDirection.UpRight => GridDirection.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}