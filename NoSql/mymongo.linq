<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Dynamic</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
{
	var ns = new NoSql("EX-");
	
	for(int i=0;i<10;i++)
		ns.Insert(Record(i));
	
	ns.GetCollection<ExpandoObject>().Dump();
	
	ns.GetCollection().Where(a => a.GetAttribute<int>("Num") == 5).First().SetAttribute("yomi","sfdfs");
	ns.GetCollection().Where(a => a.GetAttribute<int>("Num") == 5).Dump();
	
	ns.All().Select(w=> ((ExpandoObject)w.Value).GetAttribute<int>("Num")).dump();
	
	ns.All().Dump();

	
	
}

// Define other methods and classes here
public System.Dynamic.ExpandoObject Record(int i)
{
	dynamic user = new System.Dynamic.ExpandoObject();
	user.Num = i;
	user.Name = "John Doe";
	user.Age = Guid.NewGuid();
	user.HomeTown = "New York";
	user.Id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString("x");
	user.Address = new System.Dynamic.ExpandoObject();
	user.Address.Line1 = "#B-304 Prabhar Residency";
	user.Address.Line2 = "HM KrishnaReddy Layout";
	
	return user;
}


//mongo
public class NoSql
{
	public static class ID
	{
		public static string NewID()
		{
			Guid _internalId = Guid.NewGuid();
			//var _id =_internalId.ToString();
			string _id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString("x");
			Thread.Sleep(1);
			return _id;
		}
	}


	SortedList<string,object> dic = new SortedList<string,object>();
	string FILE_NM = null;
	public string Name {get;set;}
	
	public NoSql(string name)
	{
		Name = name;
		FILE_NM = string.Format("{0}.db",name);
	}
	
	public List<T> GetCollection<T>(string collectionName = null)
	{
		try
		{
			List<T> flattenList = new List<T>();//;dic.Values.ToList<T>();//dic.SelectMany( x => x.Value );
		
			//	List<T> flattenList = dic.Values.ToList().Cast<T>().ToList();
			
			//flattenList.AddRange(dic.Values.ToList<T>());
		
			//if(collectionName == null)
			{
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

			
		}
		catch(Exception e)
		{
			throw;			
		}
	}
	
	public List<System.Dynamic.ExpandoObject> GetCollection(string collectionName = null)
	{
		try
		{
			List<System.Dynamic.ExpandoObject> flattenList = new List<System.Dynamic.ExpandoObject>(); //;dic.Values.ToList<T>();//dic.SelectMany( x => x.Value );

			foreach(var v in dic.Values)
			{
				try
				{
					flattenList.Add((System.Dynamic.ExpandoObject)v);
				}
				catch{}
			}
			//return dic.Values.ToList<T>();
			return flattenList;
		}
		catch(Exception exc)
		{
			throw;			
		}
	}
	
	public void Insert(object rec)
	{
		dic.Add($"{Name}{ID.NewID()}",rec);		
	}
	
	public void Update(object rec)
	{
		
	}
	public object Find<T>(Func<T, bool> condition)
	{
		//condition.Dump();
		var thislist = dic.ToList();//GetCollection<T>();
		thislist.Dump();
		Guid md ;//dic.Keys;//.Select(condition);
		
		//thislist.Select(condition).Dump();
		//dic.Values.Select(condition).Dump();
		
		foreach(var d in dic)
		{
			
		}
		//md.Dump();
		return null;
	}
	public void Remove(){}
	
	//internal
	
	public SortedList<string,object> All()
	{
		return dic;	
	}
}

public class Table : Dictionary<string, object>
{
}


public static class Extnsion
{
	public static Dictionary<string,object> AsKV(this System.Dynamic.ExpandoObject exp)
	{
		return new Dictionary<string,object>(exp);
	
	}
	
	public static T GetAttribute<T>(this System.Dynamic.ExpandoObject exp,string key)
	{
		var d =  new Dictionary<string,object>(exp);
		T t = (T)d[key];
		return t;
	}
	
	public static void SetAttribute<T>(this System.Dynamic.ExpandoObject exp,string key, T value)
	{
		var d =  exp as IDictionary<string, Object>;;
		//d[key] = value;
		//dynamic exp2 = d;
		d.Add(key, value);
	}
	
	public static string Epoch(this object exp)
	{
		return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString("x");
	}
	
}

