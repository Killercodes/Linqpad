<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.Compression.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Namespace>System</Namespace>
  <Namespace>System.Collections</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Collections.Specialized</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.IO.Compression</Namespace>
  <Namespace>System.IO.Pipes</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Binary</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.ServiceModel.Description</Namespace>
  <Namespace>System.Text</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Web.Script.Serialization</Namespace>
  <Namespace>System.Xml.Serialization</Namespace>
  <Namespace>System.Xml.Xsl</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
{
	var ns = new NoSql("sfsdF");
	
	var rid = ns.Insert(new Dummy("one","this will be removed"));
	var aid = ns.Insert(new { F1Name = "aid",L1name = "aid value"});
	
	for(int i = 1;i<2;i++)
	{	ns.Insert(new Dummy("a"+i,"value"+i));
		ns.Insert(new Dummy("a"+i,"value"+i));
		ns.Insert(new Dummy2("b"+i,i,"a1","b1","c1","d1","e1","f1","g1"));
		ns.Insert(new { F1Name = "a"+i,L1name = "value"+i});
		ns.Insert(new Exception($"Exception no {i}"));
	}
	
	ns.GetAll<Dummy>().Dump();
	ns.Get<Dummy>(rid).Dump();
	ns.Get<object>(aid).Dump();
	ns.Update(rid, new Dummy("one","but now it's updated"));
	ns.GetAll<object>().Dump();
	
	//NoSql.ID.NewID().Dump();
	/*
	for(int j = 1;j<5;j++)
	ns.Insert(new Dummy2("b"+j,j));
	
	for(int j = 1;j<5;j++)
		ns.Insert("string" + j);
	*/
	//ns.Insert(new Dummy("c1","c2"));
	//ns.GetAll<Dummy>().Dump();//Where(d=>d.FName == "a1").Dump();
	
	//ns.GetAll<Exception>().Dump();
	
	//ns.Find<Dummy>(d=>d.FName == "a1").Dump();
	//ns.GetAll<Dummy2>().Where(d=>d.FName == "b1").Dump();
	//ns.GetAll<Dummy2>().Dump();
	//ns.GetAll<Collection>().Dump();
	ns.GetAll<Dummy2>().Where(d=> d.Id != null).Select(d=> new {d.additional,d.Id,d.FName,d.Num, d.CollectionName}) .Dump();
}

// Define other methods and classes here

public interface ExampleInterface
{
	string CollectionName {get;set;}
}
public class Dummy2:ExampleInterface
{
	public string FName {get;set;}
	public int Num {get;set;}
	public string CollectionName {get;set;}
	public Guid Id {get;private set;}
	public string[] additional;
	
	public Dummy2(string fnm,int lnm,params string[] strs)
	{
		Id = Guid.NewGuid();
		FName= fnm;
		Num = lnm;
		CollectionName = "Dummy2";
		additional = strs;
	}
}
public class Dummy:ExampleInterface
{
	public string FName {get;set;}
	public string LName {get;set;}
	public string CollectionName {get;set;}
	
	public Dummy(string fnm,string lnm)
	{
		FName= fnm;
		LName = lnm;
		CollectionName = "Dummy1";
	}
}

public class NoSql
{
	TraceSource log;
	SortedList<Guid,object> dic = new SortedList<Guid,object>();
	string FILENAME = null;
	

	//event
	public delegate void StatusUpdate(string  statusMessage);
	public event StatusUpdate Status;	
	private void OnStatus(string message)
	{
		if(Status != null){
			Status(message);
		}
	}
	
	public NoSql(string name)
	{
		log = new TraceSource("NoSql");
		
		FILENAME = string.Format("{0}.nsdb",name);
	}
	
	
	public T Get<T>(Guid id)
	{
		if(dic.ContainsKey(id))
		{
			return (T)dic[id];
		}
		
		return default(T);
	}
	
	//Reterive
	public List<T> GetAll<T>(string collectionName = null)
	{
		try
		{
			List<T> flattenList = new List<T>();
			
			foreach(var v in dic.Values)
			{
				try
				{
					flattenList.Add((T)v);
				}
				catch{}
			}
			//return dic.Values.ToList<T>();
			return flattenList;
		}
		catch(Exception e)
		{
			throw e;			
		}
	}
	
	//Create
	public Guid Insert(object rec)
	{

		var id = Guid.NewGuid();
		dic.Add(id,rec);
		_i_($"New Record[{id}] INSERTED");
		return id;
	}
	
	//update
	public void Update(Guid id,object rec)
	{
		if(dic.ContainsKey(id))
		{
			dic[id] = rec;
			_i_($"Record[{id}] UPDATED");
		}
	}
	

	
	
	//Delete
	public void Delete(Guid id)
	{
		dic.Remove(id);
		_i_($"Record[{id}] DELETED");
	}
	
	public void Truncate()
	{
		dic.Clear();
		_i_("Database TRUNCATED");
	}
	
	//internal
	private void _(string message)
	{
		log.TraceEvent(TraceEventType.Verbose,0,message);
	}
	
	private void _i_(string message)
	{
		log.TraceEvent(TraceEventType.Information,0,message);
	}
	
	private void _w_(string message)
	{
		log.TraceEvent(TraceEventType.Warning,0,message);
	}
	
	private void _e_(string message)
	{
		log.TraceEvent(TraceEventType.Error,0,message);
	}
	
}


public class Record
{
	//public Guid Id {get; private set;}
	//public string Name {get;set;}
	public Type DataType {get;private set;}
	public object Data {get;set;}
	public DateTime ModifiedOn {get;private set;}
	
	public Record(object data)
	{
		//this.Id = Guid.NewGuid();
		//this.Name = name;
		this.DataType = data.GetType();
		this.Data = data;
		this.ModifiedOn = DateTime.Now;
	}
}

public class Cache<T>
{
	public T Data;
	string FILE_NM = null;
	public Type DataType {get;private set;}
	public bool Compress { get; set;}
	public bool EncodeData { get; set;} 
	public bool InMemory { get; set;}
	
	private readonly string DIR_NM = "Cache";
	
	public Cache(string uniquename)
	{
		if (!Directory.Exists(DIR_NM))               
            Directory.CreateDirectory(DIR_NM);		
			
		FILE_NM = string.Format("{0}/{1}",DIR_NM,uniquename);
		this.Compress = true;
		this.EncodeData = true;
		this.DataType = typeof(T);
		this.InMemory = false;
	}
	
	private string Base64Encode(string plainText) 
	{
	  var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
	  return System.Convert.ToBase64String(plainTextBytes);
	}

	private string Base64Decode(string base64EncodedData) 
	{
	  var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
	  return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
	}
	
	private string JsonSerialize(T data)
	{
		JavaScriptSerializer js = new JavaScriptSerializer();  
		string jsonData = js.Serialize(data);
		return jsonData;
	}
	
	private T JsonDeserialize(string jsonData)
	{
		JavaScriptSerializer js = new JavaScriptSerializer();  
		T TObject = js.Deserialize<T>(jsonData);  
		return TObject;
	}
	
	public void Store(T data) 
	{
		JavaScriptSerializer js = new JavaScriptSerializer(); 
		js.MaxJsonLength = Int32.MaxValue;
		string jsonData = js.Serialize(data);
		
		if(EncodeData == true)
			jsonData = Base64Encode(jsonData);
		
		byte[] encoded = Encoding.UTF8.GetBytes(jsonData);
		
		if(Compress)
		{
			using(FileStream fStream = new FileStream(FILE_NM, FileMode.Create, FileAccess.Write))
		    using(GZipStream cStream = new GZipStream(fStream, CompressionLevel.Optimal))
			{
				cStream.Write(encoded, 0, encoded.Length);
				cStream.Flush();
			}
		}
		else
		{
			using(FileStream fStream = new FileStream(FILE_NM, FileMode.Create, FileAccess.Write))		    
			{
				fStream.Write(encoded, 0, encoded.Length);
				fStream.Flush();
			}
		}
		
	}
	
	
	public object GetObj() 
	{
		JavaScriptSerializer js = new JavaScriptSerializer();  
		js.MaxJsonLength = Int32.MaxValue;
		
	    object Tobject = null;
		
		
		if(Compress)
		{
			
		    using(FileStream fStream = new FileStream(FILE_NM, FileMode.Open, FileAccess.Read))
		    using(GZipStream cStream = new GZipStream(fStream, CompressionMode.Decompress))
			using (var mStream = new MemoryStream())
		    {
				cStream.CopyTo(mStream);
				
				var result = getString(mStream);
				if(EncodeData == true)
					result = Base64Decode(result);
				Tobject = js.Deserialize<object>(result);
		    }
		}
		else
		{
			using (var mStream = new MemoryStream())
		    using(FileStream fStream = new FileStream(FILE_NM, FileMode.Open, FileAccess.Read))		   
		    {
				fStream.CopyTo(mStream);
				var result = getString(mStream);
				if(EncodeData == true)
					result = Base64Decode(result);
				Tobject = js.Deserialize<object>(result);
		    }
		}
		
	    return Tobject;
	}
	
	public T Get() 
	{
		JavaScriptSerializer js = new JavaScriptSerializer();  
		js.MaxJsonLength = Int32.MaxValue;
		
	    T Tobject = default(T);;
		
		
		if(Compress)
		{
			
		    using(FileStream fStream = new FileStream(FILE_NM, FileMode.Open, FileAccess.Read))
		    using(GZipStream cStream = new GZipStream(fStream, CompressionMode.Decompress))
			using (var mStream = new MemoryStream())
		    {
				cStream.CopyTo(mStream);
				
				var result = getString(mStream);
				if(EncodeData == true)
					result = Base64Decode(result);
				Tobject = js.Deserialize<T>(result);
		    }
		}
		else
		{
			using (var mStream = new MemoryStream())
		    using(FileStream fStream = new FileStream(FILE_NM, FileMode.Open, FileAccess.Read))		   
		    {
				fStream.CopyTo(mStream);
				var result = getString(mStream);
				if(EncodeData == true)
					result = Base64Decode(result);
				Tobject = js.Deserialize<T>(result);
		    }
		}
		
	    return Tobject;
	}
	
	private string getString(MemoryStream BASE)
    {
        BASE.Position = 0;
        StreamReader R = new StreamReader(BASE);
        return R.ReadToEnd();
    }
	
	/* Binary serilization discarded as it needs serialized attribute
	public void Set(T data) 
	{
		
	    BinaryFormatter bf = new BinaryFormatter();;
	    using(FileStream fileStream = new FileStream(FILE_NM, FileMode.Create, FileAccess.Write))
	    using(GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Compress))
	    {
	        bf.Serialize(compressionStream, data);
			//compressionStream.Write(
	    }
	}

	public  T Get() 
	{
	    BinaryFormatter bf = new BinaryFormatter();;
	    T Tobject;
	    using(FileStream fileStream = new FileStream(FILE_NM, FileMode.Open, FileAccess.Read))
	    using(GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress))
	    {
	        Tobject = (T)bf.Deserialize(compressionStream);
	    }

	    return Tobject;
	}*/
	
	~Cache()
	{
		if(InMemory == true && !string.IsNullOrEmpty(FILE_NM))
			File.Delete(FILE_NM);
		//File.Move(FILE_NM, FILE_NM + " Archive" + DateTime.Now.ToString("hhmmssfff"));
	}

}
