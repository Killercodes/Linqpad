void Main()
{
	var m = new CollectionOf<Color>();

	m[0] = new Color(){ Name="black" };
	m[1] = new Color(){ Name="red" };
	m[2] = new Color(){ Name="green" };
	m[3] = new Color(){ Name="blue" };
	m.Add(new Color(){ Name="yello" });
	m.Add(new Color(){ Name="cyan" });
	m.Add(new Color(){ Name="magenta" });
	
	for(int i =0;i<=90;i++)
		m.Add(new Color { Name = "red"+i, Age = i });
		
	m[30].Name="transparent" ;
	
	var r = m.Where(f=>f.Name == "red").First();
	r.Dump();
	
	var t = from a in m where a.Name == "red" select a;
	t.Dump();
	
	m.Where(f=>f.Name == "red").Select(c => {c.Age = 99; return c;}).ToList();
	var t2 = (from a in m where a.Name == "green" select new Color() { Age =89 }).ToList();
	t2.Dump();
	
	
	m.Find(d=> d.Age>5 && d.Age <9).Dump("Find");
	m.Dump();
	
	m.Where(f=>f.Name == "red").Dump();
	
	
}

// Define other methods and classes here
// Element class.  
public class Color  
{  
    public string Name { get; set; }  public int Age {get;set;}
	public int Age2 {get;set;}
	public int Age3 {get;set;}
}

public class CollectionOf<T> : IEnumerable<T>
{
    public List<T> mylist = new List<T>();
	
    public T this[int index]  
    {  
        get { return mylist[index]; }
        set
		{
			//myList.Count.Dump();
			mylist.Insert(index, value);
			//myList.Count().Dump();
		}
    } 
	
	public void Add(T t)
	{
		mylist.Insert(Count(), t);
	}
	
	public int Count()
	{
		return mylist.Count();
	}
	
	public IEnumerable<T> Find(Func<T, bool> condition)
	{
		var selectedItems = this.mylist.Where(condition);
		return selectedItems;
	}

	

    public IEnumerator<T> GetEnumerator()
    {
        return mylist.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
	
	/*IEnumerator IEnumerable<T>.GetEnumerator()
    {
        return this.GetEnumerator();
    }*/
}
