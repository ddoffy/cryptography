using System.Security.Cryptography;
using System.Text;

public class Cryptography : IShow
{
    public void Show()
    {
        Console.WriteLine("***** Cryptography *****");

        var unencryptedMessage = "To be or not to be, that is the question, whether tis nobler in the...";

        Console.WriteLine("Unencrypted Message: " + unencryptedMessage);

        // 1. Compute hash
        var hash = ComputeHash(unencryptedMessage);
        Console.WriteLine("Hash: " + hash);

        Console.Write(Environment.NewLine);
    }

    static string ComputeHash(string msg)
    {
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(msg));
        return hash.ToHex();
    }
}