using System.Security.Cryptography;
using System.Text;

public class MessageSignature : IShow
{
    public void Show()
    {
        Console.WriteLine("***** Message Signature *****");

        var message = "To be or not to be, that is the question, whether tis nobler in the...";
        Console.WriteLine("Message to be verified: " + message);

        // 1. Create a public/private key pair
        RSAParameters privateAndPublicKey, publicKeyOnly;
        using (var rsaAlg = RSA.Create())
        {
            privateAndPublicKey = rsaAlg.ExportParameters(includePrivateParameters: true);
            publicKeyOnly = rsaAlg.ExportParameters(includePrivateParameters:false);
        }

        // 2. Sender: Sign message using private key
        var signature = Sign(message, privateAndPublicKey);
        Console.WriteLine("Message signature: " + signature.ToHex());

        // 3. Receiver: Verify message using public key
        var isVerified = Verify(message, signature, publicKeyOnly);
        Console.WriteLine("Is message verified: " + (isVerified ? "Yes" : "No"));

        Console.Write(Environment.NewLine);
    }

    byte[] Sign(string message, RSAParameters privateAndPublicKey)
    {
        using var rsaAlg = RSA.Create(privateAndPublicKey);
        return rsaAlg.SignData(Encoding.UTF8.GetBytes(message), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

    bool Verify(string message, byte[] signature, RSAParameters publicKeyOnly)
    {
        using var rsaAlg = RSA.Create(publicKeyOnly);
        return rsaAlg.VerifyData(Encoding.UTF8.GetBytes(message), signature, HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1);
    }
}