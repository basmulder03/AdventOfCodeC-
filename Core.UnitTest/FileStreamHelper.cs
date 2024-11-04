namespace Core.UnitTest;

public class FileStreamHelper
{
    public static FileStream GetFileStream(string data)
    {
        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, data);
        return new FileStream(tempFile, FileMode.Open, FileAccess.Read, FileShare.Delete);
    }
}