<Query Kind="Program" />

void Main()
{
	@"The Adapter Design Pattern converts the interface of a class into another interface clients expect.
	This design pattern lets classes work together that couldn‘t otherwise because of incompatible interfaces.".Dump("Adapter Design Pattern");
	
	// Create adapter and place a request
    Target target = new Adapter();
    target.Request();
}

// Define other methods and classes here
/// <summary>
/// The 'Target' class
/// </summary>
public class Target
{
    public virtual void Request()
    {
        Console.WriteLine("Called Target Request()");
    }
}

/// <summary>
/// The 'Adapter' class
/// </summary>
public class Adapter : Target
{
    private Adaptee adaptee = new Adaptee();
    public override void Request()
    {
        // Possibly do some other work
        // and then call SpecificRequest
        adaptee.SpecificRequest();
    }
}

/// <summary>
/// The 'Adaptee' class
/// </summary>
public class Adaptee
{
    public void SpecificRequest()
    {
        Console.WriteLine("Called SpecificRequest()");
    }
}