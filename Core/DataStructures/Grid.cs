using System.Collections;

namespace Core.DataStructures;

public class Grid<T>
{
    private readonly GridCell<T>[][] _grid;

    public int Width { get; }

    public int Height { get; }
    
    public GridCell<T>[][] Rows => _grid;
    public GridCell<T>[][] Columns => Transpose().Rows;
    public T[][] Values => _grid.Select(row => row.Where(cell => cell.HasValue).Select(cell => cell.Value!).ToArray()).ToArray();
    
    public Grid<T> Transpose()
    {
        var data = new T[Width][];
        for (var x = 0; x < Width; x++)
        {
            data[x] = new T[Height];
            for (var y = 0; y < Height; y++)
            {
                data[x][y] = _grid[y][x].Value!;
            }
        }
        return new Grid<T>(data);
    }

    private Grid(T[][] data)
    {
        Height = data.Length;
        Width = data[0].Length;
        _grid = new GridCell<T>[Height][];
        for (var y = 0; y < Height; y++)
        {
            _grid[y] = new GridCell<T>[Width];
            for (var x = 0; x < Width; x++)
            {
                _grid[y][x] = new GridCell<T>(this, data[y][x], x, y);
            }
        }
    }
    
    public GridCell<T> this[int x, int y] => _grid[y][x];
    
    public static Grid<T> Empty => new Grid<T>([]);

    public void Set(int x, int y, T value)
    {
        _grid[y][x].Value = value;
    }
    
    public GridCell<T> Get(int x, int y)
    {
        return _grid[y][x];
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
            for (var x = 0; x < width; x++)
            {
                data[y][x] = defaultValue;
            }
        }
        return new Grid<T>(data);
    }
    
    public static Grid<T> Parse(IEnumerable<string> lines, Func<string, IEnumerable<T>> parser)
    {
        var data = new T[lines.Count()][];
        for (var y = 0; y < lines.Count(); y++)
        {
            data[y] = parser(lines.ElementAt(y)).ToArray();
        }
        return new Grid<T>(data);
    }

    public Grid<T> Copy()
    {
        return new Grid<T>(_grid.Select(row => row.Select(cell => cell.Copy().Value!).ToArray()).ToArray());
    }
}