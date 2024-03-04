<Query Kind="Program" />

void Main()
{
	@"The Decorator design pattern attaches additional responsibilities to an object dynamically.
	This pattern provide a flexible alternative to subclassing for extending functionality."
	.Dump("Decorator Design Pattern");
	
	// Create ConcreteComponent and two Decorators
    ConcreteComponent c = new ConcreteComponent();
    ConcreteDecoratorA d1 = new ConcreteDecoratorA();
    ConcreteDecoratorB d2 = new ConcreteDecoratorB();
	
    // Link decorators
    d1.SetComponent(c);
    d2.SetComponent(d1);
    d2.Operation();
}

// Define other methods and classes here
/// <summary>
/// The 'Component' abstract class
/// </summary>
public abstract class Component
{
    public abstract void Operation();
}

/// <summary>
/// The 'ConcreteComponent' class
/// </summary>
public class ConcreteComponent : Component
{
    public override void Operation()
    {
        Console.WriteLine("ConcreteComponent.Operation()");
    }
}
/// <summary>
/// The 'Decorator' abstract class
/// </summary>
public abstract class Decorator : Component
{
    protected Component component;
	
    public void SetComponent(Component component)
    {
        this.component = component;
    }
	
    public override void Operation()
    {
        if (component != null)
        {
            component.Operation();
        }
    }
}

/// <summary>
/// The 'ConcreteDecoratorA' class
/// </summary>
public class ConcreteDecoratorA : Decorator
{
    public override void Operation()
    {
        base.Operation();
        Console.WriteLine("ConcreteDecoratorA.Operation()");
    }
}

/// <summary>
/// The 'ConcreteDecoratorB' class
/// </summary>
public class ConcreteDecoratorB : Decorator
{
    public override void Operation()
    {
        base.Operation();
        AddedBehavior();
        Console.WriteLine("ConcreteDecoratorB.Operation()");
    }
    void AddedBehavior()
    {
    }
}