using System.Collections;
using System.Text;

namespace Core.DataStructures;

/// <summary>
///     Represents a 2D grid structure containing elements of type T.
///     Provides various utility methods for manipulating and accessing grid data.
/// </summary>
/// <typeparam name="T">The type of elements stored in the grid.</typeparam>
public class Grid<T> : ICloneable, IEnumerable<GridCell<T>>, IEqualityComparer
{
    /// <summary>
    ///     Initializes a new instance of the Grid class with the given 2D data array.
    /// </summary>
    /// <param name="data">A 2D array of data to populate the grid.</param>
    private Grid(T[][] data)
    {
        Height = data.Length;
        Width = Height > 0 ? data[0].Length : 0;
        Rows = new GridCell<T>[Height][];
        for (var y = 0; y < Height; y++)
        {
            Rows[y] = new GridCell<T>[Width];
            for (var x = 0; x < Width; x++) Rows[y][x] = new GridCell<T>(this, data[y][x], x, y);
        }
    }

    /// <summary>
    ///     Gets the width of the grid.
    /// </summary>
    public int Width { get; }

    /// <summary>
    ///     Gets the height of the grid.
    /// </summary>
    public int Height { get; }

    /// <summary>
    ///     Gets the rows of the grid as a 2D array of GridCell objects.
    /// </summary>
    public GridCell<T>[][] Rows { get; }

    /// <summary>
    ///     Gets the values of the grid as a 2D array of elements of type T.
    /// </summary>
    public T[][] Values => Rows.Select(row => row.Select(cell => cell.Value).ToArray()).ToArray();

    /// <summary>
    ///     Gets the GridCell at the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <returns>The GridCell at the specified coordinates.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown if the coordinates are out of bounds.</exception>
    public GridCell<T> this[int x, int y] =>
        IsValidCoordinate(x, y) ? Rows[y][x] : throw new IndexOutOfRangeException();

    /// <summary>
    ///     Gets an empty grid instance.
    /// </summary>
    public static Grid<T> Empty => new([]);

    /// <summary>
    ///     Creates a deep clone of the grid.
    /// </summary>
    /// <returns>A new grid instance with the same data.</returns>
    public object Clone()
    {
        var data = new T[Height][];
        for (var y = 0; y < Height; y++)
        {
            data[y] = new T[Width];
            for (var x = 0; x < Width; x++)
            {
                var val = Rows[y][x].Value;
                data[y][x] = val is ICloneable cloneable ? (T)cloneable.Clone() : val;
            }
        }

        return new Grid<T>(data);
    }

    /// <summary>
    ///     Returns an enumerator that iterates through all cells in the grid.
    /// </summary>
    /// <returns>An enumerator for the grid cells.</returns>
    public IEnumerator<GridCell<T>> GetEnumerator()
    {
        return Rows.SelectMany(row => row).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public new bool Equals(object? a, object? b)
    {
        return a is Grid<T> gridA && b is Grid<T> gridB && gridA.Equals(gridB);
    }

    /// <inheritdoc />
    public int GetHashCode(object obj)
    {
        return obj is Grid<T> grid ? grid.GetHashCode() : 0;
    }

    /// <summary>
    ///     Transposes the grid, swapping rows and columns.
    /// </summary>
    /// <returns>A new grid instance with transposed rows and columns.</returns>
    public Grid<T> Transpose()
    {
        var data = new T[Width][];
        for (var x = 0; x < Width; x++)
        {
            data[x] = new T[Height];
            for (var y = 0; y < Height; y++) data[x][y] = Rows[y][x].Value;
        }

        return new Grid<T>(data);
    }

    /// <summary>
    ///     Creates a grid from the given 2D data array.
    /// </summary>
    /// <param name="data">The 2D data array.</param>
    /// <returns>A new grid instance.</returns>
    public static Grid<T> FromData(T[][] data)
    {
        return new Grid<T>(data);
    }

    /// <summary>
    ///     Creates a grid with the specified dimensions and fills it with a default value.
    /// </summary>
    /// <param name="width">The width of the grid.</param>
    /// <param name="height">The height of the grid.</param>
    /// <param name="defaultValue">The default value to fill the grid with.</param>
    /// <returns>A new grid instance.</returns>
    public static Grid<T> FromData(int width, int height, T defaultValue)
    {
        var data = new T[height][];
        for (var y = 0; y < height; y++)
        {
            data[y] = new T[width];
            for (var x = 0; x < width; x++) data[y][x] = defaultValue;
        }

        return new Grid<T>(data);
    }

    /// <summary>
    ///     Generates a grid with the specified dimensions using a custom initializer function.
    /// </summary>
    /// <param name="width">The width of the grid.</param>
    /// <param name="height">The height of the grid.</param>
    /// <param name="initializer">A function to generate values based on coordinates.</param>
    /// <returns>A new grid instance.</returns>
    public static Grid<T> Generate(int width, int height, Func<int, int, T> initializer)
    {
        var data = new T[height][];
        for (var y = 0; y < height; y++)
        {
            data[y] = new T[width];
            for (var x = 0; x < width; x++) data[y][x] = initializer(x, y);
        }

        return new Grid<T>(data);
    }

    /// <summary>
    /// Converts the grid to a string representation.
    /// </summary>
    /// <returns>The string representation</returns>
    public override string ToString()
    {
        return ToString(T => T!.ToString());
    }

    /// <summary>
    ///     Converts the grid to a string representation using a custom formatter for the elements.
    /// </summary>
    /// <param name="formatter">A function to format each element.</param>
    /// <returns>A string representation of the grid.</returns>
    public string ToString(Func<T, string?> formatter)
    {
        var sb = new StringBuilder();
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++) sb.Append(formatter(Rows[y][x].Value));

            sb.AppendLine();
        }

        return sb.ToString();
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is not Grid<T> other || Width != other.Width || Height != other.Height) return false;
        return this.SequenceEqual(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Width, Height, Rows);
    }

    /// <summary>
    ///     Validates if the specified coordinates are within the grid bounds.
    /// </summary>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    /// <returns>True if the coordinates are valid; otherwise, false.</returns>
    public bool IsValidCoordinate(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
}