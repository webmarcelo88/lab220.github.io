using System.Security.Cryptography;
using System.Text;

namespace Lab200.Helpers;

public static class Encoders
{
    public static string Encode(string input)
    {
        string? result = string.Empty;

        using (var md5 = MD5.Create())
        {
            var output = md5.ComputeHash(Encoding.Default.GetBytes(input.ToLower()));
            result = BitConverter.ToString(output).Replace("-", "").ToLower();
        }

        return result;
    }

    public static string Encode64(string input)
    {
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(input);

        string? result = Convert.ToBase64String(bytesToEncode);
        return result;
    }
}
