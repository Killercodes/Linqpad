# Short Guid
> This code will compress the Guid making it shorter and easy to transfer over url and other application. Since it's a Guid which is unique the Encoded Guid differs in every call makit it ideal to generate unique Id's for Programming.

```cs
void Main()
{
	var id = Guid.NewGuid();
	id.Dump("Guid");
	var id_str = Encode(id);
	id_str.Dump("Guid Encoded");
		
	var id_de = Decode(id_str);
	id_de.Dump("Guid Decoded");
}

// Define other methods and classes here
public class ShortGuid
{
	public static string Encode(Guid guid)
	{
	    string encoded = Convert.ToBase64String(guid.ToByteArray());

	    encoded = encoded
	        .Replace("/", "_")
	        .Replace("+", "-");
	    return encoded.Substring(0, 22);
	}

	public static Guid Decode(string value)
	{
	    // avoid parsing larger strings/blobs
	    if (value.Length != 22)
	    {
	        throw new ArgumentException("A ShortGuid must be exactly 22 characters long. Receive a character string.");
	    }

	    string base64 = value
	        .Replace("_", "/")
	        .Replace("-", "+") + "==";

	    byte[] blob = Convert.FromBase64String(base64);
	    var guid = new Guid(blob);

	    var sanityCheck = Encode(guid);
	    if (sanityCheck != value)
	    {
	        throw new FormatException(
	            @"Invalid strict ShortGuid encoded string. The string '{value}' is valid URL-safe Base64, " +
	            @"but failed a round-trip test expecting '{sanityCheck}'."
	        );
	    }

	    return guid;
	}
}
```
