<Query Kind="Program" />

void Main()
{
	@"The State design pattern allows an object to alter its behavior when its internal state changes.
	The object will appear to change its class.".Dump("State design pattern");
	
	// Setup context in a state
    var context = new Context(new ConcreteStateA());
    // Issue requests, which toggles state
    context.Request();
    context.Request();
    context.Request();
    context.Request();
	
}

// Define other methods and classes here
/// <summary>
/// The 'State' abstract class
/// </summary>
public abstract class State
{
    public abstract void Handle(Context context);
}

/// <summary>
/// A 'ConcreteState' class
/// </summary>
public class ConcreteStateA : State
{
    public override void Handle(Context context)
    {
        context.State = new ConcreteStateB();
    }
}

/// <summary>
/// A 'ConcreteState' class
/// </summary>
public class ConcreteStateB : State
{
    public override void Handle(Context context)
    {
        context.State = new ConcreteStateA();
    }
}

/// <summary>
/// The 'Context' class
/// </summary>
public class Context
{
    State state;
    // Constructor
    public Context(State state)
    {
        this.State = state;
    }
    // Gets or sets the state
    public State State
    {
        get { return state; }
        set
        {
            state = value;
            Console.WriteLine("State: " + state.GetType().Name);
        }
    }
    public void Request()
    {
        state.Handle(this);
    }
}