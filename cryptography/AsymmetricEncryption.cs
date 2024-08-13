using System.Security.Cryptography;
using System.Text;

public class AsymmetricEncryption : IShow
{
    public void Show()
    {
        Console.WriteLine("***** Asymmetric Encryption *****");

        var unencryptedMessage = "To be or not to be, that is the question, whether tis nobler in the...";

        Console.WriteLine("Unencrypted Message: " + unencryptedMessage);

        // 1. Create a public/private key pair
        RSAParameters privateAndPublicKey, publicKeyOnly;
        using (var rsa = RSA.Create())
        {
            privateAndPublicKey = rsa.ExportParameters(includePrivateParameters: true);
            publicKeyOnly = rsa.ExportParameters(false);
        }

        // 2. Sender: Encrypt message using public key
        var encryptedMessage = Encrypt(unencryptedMessage, publicKeyOnly);
        Console.WriteLine("Encrypted Message: " + encryptedMessage.ToHex());

        // 3. Receiver: Decrypt message using private key
        var decryptedMessage = Decrypt(encryptedMessage, privateAndPublicKey);
        Console.WriteLine("Decrypted Message: " + decryptedMessage);
    }

    static byte[] Encrypt(string unencryptedMessage, RSAParameters publicKeyOnly)
    {
        using var rsaAlg = RSA.Create(publicKeyOnly);
        return rsaAlg.Encrypt(Encoding.UTF8.GetBytes(unencryptedMessage), RSAEncryptionPadding.Pkcs1);
    }

    static string Decrypt(byte[] encryptedMessage, RSAParameters privateAndPublicKey)
    {
        using var rsaAlg = RSA.Create(privateAndPublicKey);
        var decryptedMessage = rsaAlg.Decrypt(encryptedMessage, RSAEncryptionPadding.Pkcs1);
        return Encoding.UTF8.GetString(decryptedMessage);
    }
}