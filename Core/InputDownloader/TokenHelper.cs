namespace Core.InputDownloader;

public class TokenHelper
{
    /// <summary>
    ///     Gets the token from the .aoc file in the Users home directory
    /// </summary>
    /// <returns>The token if the token exists.</returns>
    public static string? GetToken()
    {
        // Open the file, if it exists
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".aoc");
        if (!File.Exists(path))
        {
            Console.WriteLine("No token file found.");
            return null;
        }

        // Read the file
        var token = File.ReadAllText(path).Trim();
        if (!string.IsNullOrWhiteSpace(token)) return token;
        Console.WriteLine("Token file is empty.");
        return null;
    }

    /// <summary>
    ///     Saves the token to the .aoc file in the Users home directory
    ///     Replaces the file contents if the file already exists.
    /// </summary>
    /// <param name="token"></param>
    public static void SetToken(string token)
    {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".aoc");
        File.WriteAllText(path, token);
        if (!File.GetAttributes(path).HasFlag(FileAttributes.Hidden))
            File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
    }
}