namespace Core.Entities;

/// <summary>
///     Represents a 2D coordinate with X and Y components, accessible both by property and tuple deconstruction.
/// </summary>
public readonly record struct Coordinate(int X, int Y) : IComparable<Coordinate>
{
    /// <summary>
    ///     Deconstructs the coordinate into its X and Y components.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }

    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X + b.X, a.Y + b.Y);
    }

    public static Coordinate operator -(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X - b.X, a.Y - b.Y);
    }

    public static Coordinate operator *(Coordinate a, int scalar)
    {
        return new Coordinate(a.X * scalar, a.Y * scalar);
    }

    public static Coordinate operator /(Coordinate a, int scalar)
    {
        return new Coordinate(a.X / scalar, a.Y / scalar);
    }

    public int CompareTo(Coordinate other)
    {
        return X == other.X ? Y.CompareTo(other.Y) : X.CompareTo(other.X);
    }

    /// <summary>
    ///     Returns a string representation of the coordinate.
    /// </summary>
    /// <returns>A string in the format (X, Y).</returns>
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}