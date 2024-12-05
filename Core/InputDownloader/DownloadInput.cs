using System.Net;

namespace Core.InputDownloader;

public static class DownloadInput
{
    /// <summary>
    /// Downloads the input for the specified year and day and writes it to the specified file.
    /// </summary>
    /// <param name="year">The year to retrieve the data for.</param>
    /// <param name="day">The day to retrieve the data for.</param>
    /// <param name="pathToWriteInputTo">The path to write the input to.</param>
    public static async Task ForYear(int year, int day, string pathToWriteInputTo)
    {
        if (Path.Exists(pathToWriteInputTo))
        {
            Console.WriteLine("Input already exists.");
            return;
        }
        
        var token = TokenHelper.GetToken();
        if (token == null)
        {
            Console.WriteLine("No token found. Please enter your token:");
            token = Console.ReadLine();
            TokenHelper.SetToken(token);
        }

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Cookie", $"session={token}");
        client.DefaultRequestHeaders.Add("User-Agent", "AdventOfCode-PuzzleInputLoader/1.0 (https://github.com/basmulder03/AdventOfCodeC-; by basmulder03)");
        var response = await client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input");
        if (response.IsSuccessStatusCode)
        {
            WriteInputToFile(await response.Content.ReadAsStringAsync(), pathToWriteInputTo);
            return;
        }
        
        // If the token is invalid, request a new one and try again
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            Console.WriteLine("Invalid token. Please enter your token:");
            token = Console.ReadLine();
            TokenHelper.SetToken(token);
            client.DefaultRequestHeaders.Remove("Cookie");
            client.DefaultRequestHeaders.Add("Cookie", $"session={token}");
            response = await client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input");
            if (response.IsSuccessStatusCode)
            {
                WriteInputToFile(await response.Content.ReadAsStringAsync(), pathToWriteInputTo);
                return;
            }
        }
        
        Console.WriteLine("Failed to download input.");
        Console.WriteLine(response.StatusCode);
    }
    
    private static void WriteInputToFile(string input, string pathToWriteInputTo)
    {
        File.WriteAllText(pathToWriteInputTo, input);
        Console.WriteLine("Input downloaded and written to file.");
        
        // Show the input in the console
        Console.WriteLine(input);
    }
}