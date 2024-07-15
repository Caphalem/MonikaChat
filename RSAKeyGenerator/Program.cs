using System.Security.Cryptography;

public class Program
{
	public static void Main(string[] args)
	{
		using (var rsa = new RSACryptoServiceProvider(2048))
		{
			rsa.PersistKeyInCsp = false;

			// Private Key in XML format
			string privateKey = rsa.ToXmlString(true);
			Console.WriteLine("Private Key (XML):");
			Console.WriteLine(privateKey);

			//rsa.ExportParameters(true);
			// Public Key in PEM format
			string publicKey = rsa.ExportSubjectPublicKeyInfoPem();
			Console.WriteLine("\nPublic Key (PEM):");
			Console.WriteLine(publicKey);
		}
	}
}