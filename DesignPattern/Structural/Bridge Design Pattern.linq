<Query Kind="Program" />

void Main()
{
	@"The Bridge Design Pattern decouples an abstraction from its implementation so that the two can vary independently."
	.Dump("Bridge Design Pattern");
	
	Abstraction ab = new RefinedAbstraction();
	
    // Set implementation and call
    ab.Implementor = new ConcreteImplementorA();
    ab.Operation();
	
    // Change implemention and call
    ab.Implementor = new ConcreteImplementorB();
    ab.Operation();

}

// Define other methods and classes here
/// <summary>
/// The 'Abstraction' class
/// </summary>
public class Abstraction
{
    protected Implementor implementor;
	
    public Implementor Implementor
    {
        set { implementor = value; }
    }
	
    public virtual void Operation()
    {
        implementor.Operation();
    }
}

/// <summary>
/// The 'Implementor' abstract class
/// </summary>
public abstract class Implementor
{
    public abstract void Operation();
}
/// <summary>
/// The 'RefinedAbstraction' class
/// </summary>
public class RefinedAbstraction : Abstraction
{
    public override void Operation()
    {
        implementor.Operation();
    }
}

/// <summary>
/// The 'ConcreteImplementorA' class
/// </summary>
public class ConcreteImplementorA : Implementor
{
    public override void Operation()
    {
        Console.WriteLine("ConcreteImplementorA Operation");
    }
}

/// <summary>
/// The 'ConcreteImplementorB' class
/// </summary>
public class ConcreteImplementorB : Implementor
{
    public override void Operation()
    {
        Console.WriteLine("ConcreteImplementorB Operation");
    }
}