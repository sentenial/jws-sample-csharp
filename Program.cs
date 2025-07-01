public class Program
{
    public static void Main()
    {

        //from the website
        string expected = "ewogICJhbGciOiAiUlMyNTYiLAogICJraWQiOiAiMTMzNzQ3MTQxMjU1IiwKICAiaWF0IjogMCwKICAiaXNzIjogIkM9R0IsIEw9TG9uZG9uLCBPVT1OdWFwYXkgQVBJLCBPPU51YXBheSwgQ049eWJvcXlheTkycSIsCiAgImI2NCI6IGZhbHNlLAogICJjcml0IjogWwogICAgImlhdCIsCiAgICAiaXNzIiwKICAgICJiNjQiCiAgXQp9..d_cZ46lwNiaFHAu_saC-Zz4rSzNbevWirO94EmBlbOwkB1L78vGbAnNjUsmFSU7t_HhL-cyMiQUDyRWswsEnlDljJsRi8s8ft48ipy2SMuZrjPpyYYMgink8nZZK7l-eFJcTiS9ZWezAAXF_IJFXSTO5ax9z6xty3zTNPNMV9W7aH8fEAvbUIiueOhH5xNHcsuqlOGygKdFz2rbjTGffoE_6zS4Dry-uX5mts2duLorobUimGsdlUcSM6P6vZEtcXaJCdjrT9tuFMh4CkX9nqk19Bq2z3i-SX4JCPvhD2r3ghRmX0gG08UcvyFVbrnVZJnpl4MU8V4Nr3-2M5URZOg";

        string privateKeyPath = "sample-private.key"; // Path to your private key
        var privateKey = SignatureUtils.LoadTestPrivateKey(privateKeyPath);
        
        string payload = "{}"; // Your payload
        string kid = "133747141255"; // Key ID
        // object iat = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); // Issued at timestamp
        object iat = 0; // Issued at timestamp
        string iss = "C=GB, L=London, OU=Nuapay API, O=Nuapay, CN=yboqyay92q"; // Issuer

        string signature = SignatureUtils.CreateSignature(privateKey, payload, kid, iat, iss);

        if(signature.Equals(expected) ) {
            Console.WriteLine("The signatures match");
        }
        else {
            Console.WriteLine("The signatures do not match");
        }
        Console.WriteLine("Expected " + expected);
        Console.WriteLine("Produced " + signature);
    }
}
