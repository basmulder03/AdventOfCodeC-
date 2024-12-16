using Core.Interfaces;

namespace Solutions._2015;

public class Day11 : IBaseDay
{
    public long Part1(string input)
    {
        Console.Write(GetNextValidPassword(input));
        return 0;
    }

    public long Part2(string input)
    {
        Console.Write(GetNextValidPassword(IncrementPassword(GetNextValidPassword(input))));
        return 0;
    }

    private static string IncrementPassword(string password)
    {
        var passwordArray = password.ToCharArray();
        for (var i = passwordArray.Length - 1; i >= 0; i--)
            if (passwordArray[i] == 'z')
            {
                passwordArray[i] = 'a';
            }
            else
            {
                passwordArray[i]++;
                break;
            }

        return new string(passwordArray);
    }

    private static string GetNextValidPassword(string password)
    {
        var currentPassword = password;
        while (true)
        {
            currentPassword = IncrementPassword(currentPassword);

            // Check for invalid characters
            if (currentPassword.Contains('i') || currentPassword.Contains('o') ||
                currentPassword.Contains('l')) continue;

            // Check for increasing straight of at least three letters
            var hasStraight = false;
            for (var i = 0; i < currentPassword.Length - 2; i++)
            {
                if (currentPassword[i] + 1 != currentPassword[i + 1] ||
                    currentPassword[i + 1] + 1 != currentPassword[i + 2]) continue;
                hasStraight = true;
                break;
            }

            // Check for two different, non-overlapping pairs of letters
            var pairCount = 0;
            var lastPairIndex = -1;
            for (var i = 0; i < currentPassword.Length - 1; i++)
            {
                if (i == lastPairIndex) continue;

                if (currentPassword[i] != currentPassword[i + 1]) continue;
                pairCount++;
                i++;
                lastPairIndex = i;
            }

            if (hasStraight && pairCount >= 2) return currentPassword;
        }
    }
}