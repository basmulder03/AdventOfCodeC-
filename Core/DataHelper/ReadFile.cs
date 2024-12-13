using System.Reflection;

namespace Core.DataHelper;

public static class ReadFile
{
    public static List<string> ReadLines(this FileStream fileStream)
    {
        using var reader = new StreamReader(fileStream);
        var lines = new List<string>();
        while (!reader.EndOfStream) lines.Add(reader.ReadLine());

        return lines;
    }

    public static string ReadSingleLine(this FileStream fileStream)
    {
        using var reader = new StreamReader(fileStream);
        return reader.ReadLine() ?? string.Empty;
    }

    public static List<char> ReadSingleLineAsChars(this FileStream fileStream)
    {
        var line = fileStream.ReadSingleLine();
        return line.ToCharArray().ToList();
    }

    public static List<int> ReadLinesAsInt(this FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        return lines.Select(int.Parse).ToList();
    }

    public static List<long> ReadLinesAsLong(this FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        return lines.Select(long.Parse).ToList();
    }

    public static FileStream GetFileStream(string path)
    {
        var callerPath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
        var fullPath = Path.Combine(callerPath, path);
        return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
    }
    
    public static List<List<string>> ReadGroups(this FileStream fileStream)
    {
        var lines = fileStream.ReadLines();
        var groups = new List<List<string>>();
        var group = new List<string>();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                groups.Add(group);
                group = new List<string>();
            }
            else
            {
                group.Add(line);
            }
        }

        groups.Add(group);
        return groups;
    }
}