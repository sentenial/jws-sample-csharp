using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;


public class SignatureUtils
{
    private static readonly string[] CRIT = new string[] { "iat", "iss", "b64" };
    private static readonly string alg = "RS256"; // JWS Algorithm name

    public static RSA LoadTestPrivateKey(string pathToPrivateKey)
    {
        string pem = File.ReadAllText(pathToPrivateKey);

        // Remove the PEM header and footer
        string base64 = pem.Replace("-----BEGIN PRIVATE KEY-----", "")
                           .Replace("-----END PRIVATE KEY-----", "")
                           .Replace("\n", "")
                           .Trim();

        // Convert the base64 string to a byte array
        byte[] privateKeyBytes = Convert.FromBase64String(base64);

        // Create an RSA instance
        RSA rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _); // Import the private key

        return rsa;
    }

    public static string CreateSignature(RSA privateKey, string payload, string kid, object iat, string iss)
    {
        var map = new Dictionary<string, object>
        {
            { "alg", alg },
            { "kid", kid },
            { "iat", iat },
            { "iss", iss },
            { "b64", false },
            { "crit", CRIT }
        };

        string header = Base64UrlEncode(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(map, Formatting.Indented)));

        Console.WriteLine("header :" + header);

        var os = new MemoryStream();
        os.Write(Encoding.UTF8.GetBytes(header));
        os.WriteByte(0x2E); // ASCII for "."
        os.Write(Encoding.UTF8.GetBytes(payload));

        // Create a signature
        byte[] signatureBytes = SignData(privateKey, os.ToArray());
        string signature = Base64UrlEncode(signatureBytes);
        return $"{header}..{signature}";
    }

    private static byte[] SignData(RSA privateKey, byte[] dataToSign)
    {

        return privateKey.SignData(dataToSign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }
    private static string Base64UrlEncode(byte[] input)
    {
        return Convert.ToBase64String(input)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

}