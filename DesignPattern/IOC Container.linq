<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.DataVisualization.dll</Reference>
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
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Windows.Forms.DataVisualization.Charting</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
{
	var c = new Container("container");
	c.Register<IHuman,Person>();
	c.Register<IHuman,Employee>();
	
	//var p = c.Resolve<IHuman>();
	//p.Dump();
	
	//var e = c.Resolve<IHuman>();
	//e.Dump();
	
	var v = c.Resolve<CreatHuman>();
	v.Dump();
}

// Define other methods and classes here

public class CreatHuman
{
	public IHuman h;
	public CreatHuman(IHuman hum)
	{
		h = hum;
	}
}

public interface IHuman
{
	string Name {get;set;}
}

public class Person:IHuman
{
	public string Name {get;set;} = "Person";
	
	public Person(string name)
	{
		this.Name = name;
	}
}

public class Employee: IHuman
{
	public string Name {get;set;} = "Employee";
	
	public Employee(string name)
	{
		this.Name = name;
	}
}


public class Container
{
    private readonly System.Collections.Generic.Dictionary<Type, Type> map = new System.Collections.Generic.Dictionary<Type, Type>();
    public string Name { get; private set; }
	
    public Container(string containerName)
    {
        Name = containerName;
        ($"New instance of {Name} created").Dump();
    }

    // Register the mapping for inversion of control
	public void Register(Type type)
	{
		map.Add(type, type);
		($"Registering {type.Name}").Dump();
	}
	
    public void Register<From,To>()
    {
        try
        {
			if(map.ContainsKey(typeof(From)))
			{
				map[typeof(From)] = typeof(To);
			}
			else{
            	map.Add(typeof(From), typeof(To));
            }
			
			System.Diagnostics.Trace.TraceInformation("Registering {0} for {1}", typeof(From).Name, typeof(To).Name);
        }
        catch(Exception registerException)
        {
            System.Diagnostics.Trace.TraceError("Mapping Exception", registerException);
            throw new Exception("Mapping Exception",registerException);
        }
    }


    // Resolves the Instance 
    public T Resolve<T>(params object[] args)
    {
        return (T)Resolve(typeof(T),args);
    }

    private object Resolve(Type type,params object[] args)
    {
        Type resolvedType = null;
        try
        {
            resolvedType = map[type];
            ($"[!] Resolving {type.Name}").Dump();
        }
        catch(Exception resolveException)
        {
            ("[x] Could't resolve type").Dump();
            //throw new Exception("Could't resolve type", resolveException);
			resolvedType = type;
        }
		

        var ctor = resolvedType.GetConstructors().First();
        var ctorParameters = ctor.GetParameters();
        if(ctorParameters.Length ==0)
        {
            ("[!] Constructor have no parameters").Dump();
            return Activator.CreateInstance(resolvedType);
        }
		else
		{
	        var parameters = new System.Collections.Generic.List<object>();
	        ($"[!] Constructor found to have {ctorParameters.Length} parameters").Dump();

	        foreach (var p in ctorParameters)
	        {
	            parameters.Add(Resolve(p.ParameterType));
	        }

	        return ctor.Invoke(args.ToArray());
		}
    }
}