<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	Console.WriteLine("Please enter the string for hashing:");
            string plaintext = Console.ReadLine();
 
            string passwordHashMD5 = ComputeHash(plaintext, "MD5");
            string passwordHashSha1 =
                   ComputeHash(plaintext, "SHA1");
            string passwordHashSha256 =
                   ComputeHash(plaintext, "SHA256");
            string passwordHashSha384 =
                   ComputeHash(plaintext, "SHA384");
            string passwordHashSha512 =
                   ComputeHash(plaintext, "SHA512");
 
            Console.WriteLine("Original String   : {0}", plaintext);
            Console.WriteLine("Hash values :\r\n");
            Console.WriteLine("MD5   : {0}", passwordHashMD5);
            Console.WriteLine("SHA1  : {0}", passwordHashSha1);
            Console.WriteLine("SHA256: {0}", passwordHashSha256);
            Console.WriteLine("SHA384: {0}", passwordHashSha384);
            Console.WriteLine("SHA512: {0}", passwordHashSha512);
            Console.WriteLine("");
}

// Define other methods and classes here
public static string ComputeHash(string plainText,
                                     string hashAlgorithm,
                                     byte[] saltBytes = null)
{
   // If salt is not specified, generate it on the fly.
   if (saltBytes == null)
   {
       // Define min and max salt sizes.
       int minSaltSize = 4;
       int maxSaltSize = 8;

       // Generate a random number for the size of the salt.
       Random random = new Random();
       int saltSize = random.Next(minSaltSize, maxSaltSize);

       // Allocate a byte array, which will hold the salt.
       saltBytes = new byte[saltSize];

       // Initialize a random number generator.
       RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

       // Fill the salt with cryptographically strong byte values.
       rng.GetNonZeroBytes(saltBytes);
   }

   // Convert plain text into a byte array.
   byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

   // Allocate array, which will hold plain text and salt.
   byte[] plainTextWithSaltBytes =
           new byte[plainTextBytes.Length + saltBytes.Length];

   // Copy plain text bytes into resulting array.
   for (int i = 0; i < plainTextBytes.Length; i++)
       plainTextWithSaltBytes[i] = plainTextBytes[i];

   // Append salt bytes to the resulting array.
   for (int i = 0; i < saltBytes.Length; i++)
       plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];


   HashAlgorithm hash;

   // Make sure hashing algorithm name is specified.
   if (hashAlgorithm == null)
       hashAlgorithm = "";

   // Initialize appropriate hashing algorithm class.
   switch (hashAlgorithm.ToUpper())
   {
       case "SHA1":
           hash = new SHA1Managed();
           break;

       case "SHA256":
           hash = new SHA256Managed();
           break;

       case "SHA384":
           hash = new SHA384Managed();
           break;

       case "SHA512":
           hash = new SHA512Managed();
           break;

       default:
           hash = new MD5CryptoServiceProvider();
           break;
   }

   // Compute hash value of our plain text with appended salt.
   byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

   // Create array which will hold hash and original salt bytes.
   byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                       saltBytes.Length];

   // Copy hash bytes into resulting array.
   for (int i = 0; i < hashBytes.Length; i++)
       hashWithSaltBytes[i] = hashBytes[i];

   // Append salt bytes to the result.
   for (int i = 0; i < saltBytes.Length; i++)
       hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

   // Convert result into a base64-encoded string.
   string hashValue = Convert.ToBase64String(hashWithSaltBytes);

   // Return the result.
   return hashValue;
}