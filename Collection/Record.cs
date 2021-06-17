void Main()
{
	Table t = new Table("employee");
	t.Status += (s) => Console.WriteLine(s);
	
	Record rec = new Record();
	rec["fn"]= "vinod:";
	rec["ln"] = "srivastav";
	rec["ag"] = 34;
	t.AddRecord(rec);
	t["one"] = rec;
	
	t.iTable.Dump();
	
	//t.iTable.Where(r=> r.fn == "vinod").Dump();
	//t.Compress<Table>(@"C:\Sw-Install\LinqPad\New folder\Table");
	//t.Save(@"C:\Sw-Install\LinqPad\New folder\Table2");
	
	//Table t2 = Table.Load(@"C:\Sw-Install\LinqPad\New folder\Table2");
	//t2["one"].Dump();
}

// Define other methods and classes here

[Serializable]
public class Table
{
	//public
	public Dictionary<string,Record> iTable;
	public string TableName {get;private set;}
	
	//event
	public delegate void StatusUpdate(string  statusMessage);
	public event StatusUpdate Status;	
	private void _(string message)
	{
		if(Status != null){
			Status(message);
		}
	}
	private void _(string Format,params object[] args)
	{
		if(Status != null){
			Status(String.Format(Format,args));
		}
	}
	
	
	public Table(string name)
	{
		_(" Creating Table: {0} ",name); 
		this.TableName = name;
		iTable = new Dictionary<string,Record>();
		_(" Creating Table: {0} ",name);
	}
	
	public Record this[string Key]
	{
		get
		{
			return iTable[Key];
		}
		
		set
		{
			iTable[Key] = value;
		}
	}
	
	public bool AddRecord(Record record)
	{
		try
		{
			string id = Guid.NewGuid().ToString("N");
			iTable[id] = record;
			_(" Adding New Record: {0} ",id);
			return true;
		}
		catch(Exception e)
		{
			_("Error: {0}",e.Message);
			return false;
		}
	}
	
	public void Save(string file)
	{
		BinaryFormatter bf = new BinaryFormatter();
        using(FileStream fileStream = new FileStream(file+ ".Table", FileMode.Create, FileAccess.Write))
        using(GZipStream compressionStream = new GZipStream(fileStream, CompressionLevel.Optimal))
		{
			bf.Serialize(compressionStream, this);
		}
	}
	
	public static Table Load(string name)
	{
		BinaryFormatter bf = new BinaryFormatter();
		Table Tobject;
        using(FileStream fileStream = new FileStream(name + ".Table", FileMode.Open, FileAccess.Read))
        using(GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress))
        {
			Tobject = (Table)bf.Deserialize(compressionStream);
		}
		
        return Tobject;
	}
}

[Serializable]
public class Record
{
	public Dictionary<string,object> Column = new Dictionary<string,object>();
	
	public Record()
	{
		Column = new Dictionary<string,object>();
		//Column["id"] = Guid.NewGuid().ToString("N");
	}
	
	public object this[string Key]
	{
		get
		{
			return Column[Key];
		}
		
		set
		{
			Column[Key] = value;
		}
	}
	
	
	
	
}
