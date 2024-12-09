using System.Security.Cryptography;
using System.Text;

namespace Core.Hashing;

public class MD5Hasher : IHasher
{
    public string Hash(string input)
    {
        // Hash the input with MD5
        using var md5 = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        // Convert the byte array to hexadecimal string
        var sb = new StringBuilder();
        foreach (var t in hashBytes) sb.Append(t.ToString("X2"));

        return sb.ToString();
    }
}