using System.Security.Cryptography;

public class SymmetricEncryption : IShow
{
    public void Show()
    {
        Console.WriteLine("***** Symmetric Encryption *****");

        var unencryptedMessage = "To be or not to be, that is the question, whether tis nobler in the...";

        Console.WriteLine("Unencrypted Message: " + unencryptedMessage);

        // 1. Create a key (shared key between sender and receiver)
        byte[] key, iv;
        using (Aes aesAlg = Aes.Create())
        {
            key = aesAlg.Key;
            iv = aesAlg.IV;
        }

        Console.WriteLine("Key: " + key.ToHex());
        Console.WriteLine("IV: " + iv.ToHex());

        // 2. Sender: Encrypt message using key
        var encryptedMessage = Encrypt(unencryptedMessage, key, iv);
        Console.WriteLine("Encrypted Message: " + encryptedMessage.ToHex());

        // 3. Receiver: Decrypt message using key
        var decryptedMessage = Decrypt(encryptedMessage, key, iv);
        Console.WriteLine("Decrypted Message: " + decryptedMessage);

        Console.Write(Environment.NewLine);
    }

    static byte[] Encrypt(string message, byte[] key, byte[] iv)
    {
        using var aesAlg = Aes.Create();
        aesAlg.Key = key;
        aesAlg.IV = iv;

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(message);
        }

        return ms.ToArray();
    }

    static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
    {
        using var aesAlg = Aes.Create();
        aesAlg.Key = key;
        aesAlg.IV = iv;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var ms = new MemoryStream(cipherText);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
}