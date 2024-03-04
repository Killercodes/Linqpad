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
	var r = new Reactor();
	var s1 = new Service1();
	r.Add(s1);
	r.Start();
}

// Define other methods and classes here
public interface IService
{
    void Start();
    void Stop();
}

public class Service1: IService , IPollable
{
	public TimeSpan PollTimeSpan {get; private set;}
	
	
	public Service1()
	{
		PollTimeSpan = new TimeSpan(100000);
	}
	
	public void Start()
	{
		$"{DateTime.Now.ToString("o")} Start".Dump();
	}
	
	public void Stop()
	{
		$"{DateTime.Now.ToString("o")} Stop".Dump();
	}
	
	public void Poll()
	{
		var wrkload = new Random().Next(60000);
		$"{DateTime.Now.ToString("o")} Poll [{wrkload}]".Dump();
		
		Thread.Sleep(wrkload);
		$"{DateTime.Now.ToString("o")} Poll [Finish]".Dump();
	}
}

public interface IPollable
{
    TimeSpan PollTimeSpan { get; }
    void Poll();
}



public class Reactor
{
    // Whether the reactor has started
    public bool Started { get; private set; }

    // A cancellation token to be able to request cancellation events
    public CancellationToken CancellationToken { get; private set; }

    private ISet<IService> services;
    private IDictionary<long, ISet<IPollable>> pollableTimers;

    public Reactor()
    {
        CancellationToken = new CancellationToken();
        services = new HashSet<IService>();
    }

    /// <summary>
    /// Start main processing loop
    /// </summary>
    /// <remarks>
    /// This algorithm uses a sorted dictionary to determine what pollable needs to be ran next.
    /// This results in a O(log n) implementation instead of a typical O(n) when looping through each pollable
    /// </remarks>
    public void Start()
    {
        pollableTimers = new SortedDictionary<long, ISet<IPollable>>();
        Started = true;
        long currentTime = DateTime.UtcNow.Ticks;

        // Populate sorted dictionary with initial runtimes
        foreach(IService service in services)
        {
            service.Start();

            if(service is IPollable)
            {
                IPollable pollable = service as IPollable;
                Schedule(pollable, currentTime + pollable.PollTimeSpan.Ticks);
            }
            
        }

        while(!CancellationToken.IsCancellationRequested)
        {
            // See whats next
            var kvp = pollableTimers.First();
            pollableTimers.Remove(kvp);

            long nextTime = kvp.Key;
            ISet<IPollable> pollables = kvp.Value;

            // See how long we need to wait to run it, if at all
            currentTime = DateTime.UtcNow.Ticks;
            if (nextTime > currentTime)
            {
                Thread.Sleep(TimeSpan.FromTicks(nextTime - currentTime));
            }

            // Poll the pollable, readd it to the sorted dictionary so we can continue
            while(pollables.Count > 0)
            {
                IPollable pollable = pollables.First();
                pollables.Remove(pollable);
                pollable.Poll();
                Schedule(pollable, nextTime + pollable.PollTimeSpan.Ticks);
            }
        }
    }

    private void Schedule(IPollable pollable, long tick)
    {
        ISet<IPollable> pollables;
        
        if(!pollableTimers.TryGetValue(tick, out pollables))
        {
            pollables = new HashSet<IPollable>();
            pollableTimers[tick] = pollables;
        }

        pollables.Add(pollable);
    }

    /// <summary>
    /// Register the service
    /// </summary>
    /// <param name="service">The service to register</param>
    public void Add(IService service)
    {
        services.Add(service);

        // If the service is a pollable, also do some bookkeeping to schedule the pollable when necesary
        if(service is IPollable && Started)
        {
            IPollable pollable = service as IPollable;
            Schedule(pollable, DateTime.UtcNow.Ticks + pollable.PollTimeSpan.Ticks);
        }
    }

    public void Remove(IService service)
    {
        services.Remove(service);

        // If the service is a pollable, also do some bookkeeping and remove the pollable from scheduling
        if(service is IPollable && Started)
        {
            IPollable pollable = service as IPollable;
            var item = pollableTimers.First(kvp => kvp.Value == pollable);
            pollableTimers.Remove(item);
        }
    }
}


