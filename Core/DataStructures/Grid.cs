using System.Collections;
using System.Text;

namespace Core.DataStructures;

public class Grid<T> : ICloneable, IEnumerable<GridCell<T>>, IEqualityComparer
{
    private Grid(T[][] data)
    {
        Height = data.Length;
        Width = data[0].Length;
        Rows = new GridCell<T>[Height][];
        for (var y = 0; y < Height; y++)
        {
            Rows[y] = new GridCell<T>[Width];
            for (var x = 0; x < Width; x++) Rows[y][x] = new GridCell<T>(this, data[y][x], x, y);
        }
    }

    public int Width { get; }

    public int Height { get; }

    public GridCell<T>[][] Rows { get; }

    public GridCell<T>[][] Columns => Transpose().Rows;

    public T[][] Values => Rows.Select(row => row.Where(cell => cell.HasValue).Select(cell => cell.Value!).ToArray())
        .ToArray();

    public GridCell<T> this[int x, int y] => Rows[y][x];
    public GridCell<T> this[GridCell<T> cell] => Rows[cell.Y][cell.X];

    public static Grid<T> Empty => new([]);

    public object Clone()
    {
        var data = new T[Height][];
        for (var y = 0; y < Height; y++)
        {
            data[y] = new T[Width];
            for (var x = 0; x < Width; x++)
            {
                var val = Rows[y][x].Value is ICloneable cloneable ? (T)cloneable.Clone() : Rows[y][x].Value;
                data[y][x] = val!;
            }
        }

        return new Grid<T>(data);
    }

    public IEnumerator<GridCell<T>> GetEnumerator()
    {
        return (from row in Rows from cell in row select cell).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Grid<T> Transpose()
    {
        var data = new T[Width][];
        for (var x = 0; x < Width; x++)
        {
            data[x] = new T[Height];
            for (var y = 0; y < Height; y++) data[x][y] = Rows[y][x].Value!;
        }

        return new Grid<T>(data);
    }

    private void Set(int x, int y, T value)
    {
        Rows[y][x].Value = value;
    }

    private GridCell<T> Get(int x, int y)
    {
        return Rows[y][x];
    }

    public static Grid<T> FromData(T[][] data)
    {
        return new Grid<T>(data);
    }

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

    public static Grid<T> Parse(IEnumerable<string> lines, Func<string, IEnumerable<T>> parser)
    {
        var data = new T[lines.Count()][];
        for (var y = 0; y < lines.Count(); y++) data[y] = parser(lines.ElementAt(y)).ToArray();
        return new Grid<T>(data);
    }

    public string ToString(Func<T, string> formatter)
    {
        var sb = new StringBuilder();
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++) sb.Append(formatter(Rows[y][x].Value!));
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public static bool operator ==(Grid<T> a, Grid<T> b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Grid<T> a, Grid<T> b)
    {
        return !a.Equals(b);
    }
    
    
    public new bool Equals(object? a, object? b)
    {
        return a is Grid<T> gridA && b is Grid<T> gridB && gridA.Equals(gridB);
    }

    public int GetHashCode(object obj)
    {
        return ((Grid<T>)obj).GetHashCode();
    }
    
    protected bool Equals(Grid<T> other)
    {
        return Width == other.Width && Height == other.Height && Rows.Equals(other.Rows);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Grid<T>)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Width;
            hashCode = (hashCode * 397) ^ Height;
            hashCode = (hashCode * 397) ^ Rows.GetHashCode();
            return hashCode;
        }
    }
}