void Main()
{
	var xs = "ab,cd,ef,gh,ij,ab";
	
	var xsa = xs.Split(',');
	
	List<string> lst = new List<string>(xsa);
	lst.Dump();
	
	var dictionary = new Dictionary<string, Data>();
	for (int i = 0; i < xsa.Length; i++)
	{
		Data d =  new Data(xsa[i],null,null);
	    dictionary[xsa[i]] = d;
	}
	dictionary.Dump();
	
	DataTable table = new DataTable("abc");
	table.Columns.Add(new DataColumn("QuotationCode"));
	table.Columns.Add(new DataColumn("IsOpportunity"));
	table.Columns.Add(new DataColumn("IsWon"));
	
	for (int i = 0; i < xsa.Length; i++)
	{
		DataRow newRow = table.NewRow();
	    newRow["QuotationCode"] = xsa[i];
		newRow["IsOpportunity"] = "item " + i*2;
		newRow["IsWon"] = false;
		
	    table.Rows.Add(newRow);
	}
	
	table.Dump();
	
	DataRow[] result1 = table.Select("IsWon = false AND QuotationCode = 'cd'");
	
	result1.Dump();
	
	
}

// Define other methods and classes here

public struct Data
{
	public string QuotationCode;
	public string IsOpportunity;// {get;set;}
	public string IsWon;// {get;set;}
	
	public Data(string one,string two, string three)
	{
		this.QuotationCode = one;
		IsOpportunity = two;
		this.IsWon = three;
	}
}
