# Get WHOIS Info

Get the whois info

```cs
void Main()
{
	var whoisText = Whois.Lookup("microsoft.com");
    Console.WriteLine(whoisText);
}

// Define other methods and classes here
public class Whois
    {
        private const int Whois_Server_Default_PortNumber = 43;
        private const string Domain_Record_Type = "domain";
        private const string DotCom_Whois_Server = "whois.verisign-grs.com";

        /// <summary>
        /// Retrieves whois information
        /// </summary>
        /// <param name="domainName">The registrar or domain or name server whose whois information to be retrieved</param>
        /// <param name="recordType">The type of record i.e a domain, nameserver or a registrar</param>
        /// <returns></returns>
        public static string Lookup(string domainName)
        {
            using (TcpClient whoisClient = new TcpClient())
            {
                whoisClient.Connect(DotCom_Whois_Server, Whois_Server_Default_PortNumber);

                string domainQuery = Domain_Record_Type + " " + domainName + "\r\n";
                byte[] domainQueryBytes = Encoding.ASCII.GetBytes(domainQuery.ToCharArray());

                Stream whoisStream = whoisClient.GetStream();
                whoisStream.Write(domainQueryBytes, 0, domainQueryBytes.Length);

                StreamReader whoisStreamReader = new StreamReader(whoisClient.GetStream(), Encoding.ASCII);

                string streamOutputContent = "";
                List<string> whoisData = new List<string>();
                while (null != (streamOutputContent = whoisStreamReader.ReadLine()))
                {
                    whoisData.Add(streamOutputContent);
                }

                whoisClient.Close();

                return String.Join(Environment.NewLine, whoisData);
            }            
        }        
    }
```
