<Query Kind="Program" />

void Main()
{
	@"The Flyweight Design Pattern uses sharing to support large numbers of fine-grained objects efficiently."
	.Dump("Flyweight Design Pattern");
	
	// Arbitrary extrinsic state
    int extrinsicstate = 22;
    FlyweightFactory factory = new FlyweightFactory();
	
    // Work with different flyweight instances
    Flyweight fx = factory.GetFlyweight("X");
    fx.Operation(--extrinsicstate);
	
    Flyweight fy = factory.GetFlyweight("Y");
    fy.Operation(--extrinsicstate);
	
    Flyweight fz = factory.GetFlyweight("Z");
    fz.Operation(--extrinsicstate);
	
    UnsharedConcreteFlyweight fu = new UnsharedConcreteFlyweight();
    fu.Operation(--extrinsicstate);
}

// Define other methods and classes here

/// The 'FlyweightFactory' class
public class FlyweightFactory
{
    private Dictionary<string, Flyweight> flyweights { get; set; } = new Dictionary<string, Flyweight>();
    // Constructor
    public FlyweightFactory()
    {
        flyweights.Add("X", new ConcreteFlyweight());
        flyweights.Add("Y", new ConcreteFlyweight());
        flyweights.Add("Z", new ConcreteFlyweight());
    }
    public Flyweight GetFlyweight(string key)
    {
        return ((Flyweight)flyweights[key]);
    }
}


/// The 'Flyweight' abstract class
public abstract class Flyweight
{
    public abstract void Operation(int extrinsicstate);
}


/// The 'ConcreteFlyweight' class
public class ConcreteFlyweight : Flyweight
{
    public override void Operation(int extrinsicstate)
    {
        Console.WriteLine("ConcreteFlyweight: " + extrinsicstate);
    }
}


/// The 'UnsharedConcreteFlyweight' class
public class UnsharedConcreteFlyweight : Flyweight
{
    public override void Operation(int extrinsicstate)
    {
        Console.WriteLine("UnsharedConcreteFlyweight: " +
            extrinsicstate);
    }
}