<Query Kind="Program" />

void Main()
{
	@"The Mediator Design Pattern defines an object that encapsulates how a set of objects interact.
	Mediator promotes loose coupling by keeping objects from referring to each other explicitly,
	and it lets you vary their interaction independently.".Dump("Mediator Design Pattern");
	
	ConcreteMediator m = new ConcreteMediator();
    ConcreteColleague1 c1 = new ConcreteColleague1(m);
    ConcreteColleague2 c2 = new ConcreteColleague2(m);
    m.Colleague1 = c1;
    m.Colleague2 = c2;
	
    c1.Send("How are you?");
    c2.Send("Fine, thanks");
}

// Define other methods and classes here
/// <summary>
/// The 'Mediator' abstract class
/// </summary>
public abstract class Mediator
{
    public abstract void Send(string message,Colleague colleague);
}

/// <summary>
/// The 'ConcreteMediator' class
/// </summary>
public class ConcreteMediator : Mediator
{
    ConcreteColleague1 colleague1;
    ConcreteColleague2 colleague2;
	
    public ConcreteColleague1 Colleague1
    {
        set { colleague1 = value; }
    }
    public ConcreteColleague2 Colleague2
    {
        set { colleague2 = value; }
    }
    public override void Send(string message, Colleague colleague)
    {
        if (colleague == colleague1)
        {
            colleague2.Notify(message);
        }
        else
        {
            colleague1.Notify(message);
        }
    }
}

/// <summary>
/// The 'Colleague' abstract class
/// </summary>
public abstract class Colleague
{
    protected Mediator mediator;
    // Constructor
    public Colleague(Mediator mediator)
    {
        this.mediator = mediator;
    }
}

/// <summary>
/// A 'ConcreteColleague' class
/// </summary>
public class ConcreteColleague1 : Colleague
{
    // Constructor
    public ConcreteColleague1(Mediator mediator)
        : base(mediator)
    {
    }
    public void Send(string message)
    {
        mediator.Send(message, this);
    }
    public void Notify(string message)
    {
        Console.WriteLine("Colleague1 gets message: "
            + message);
    }
}

/// <summary>
/// A 'ConcreteColleague' class
/// </summary>
public class ConcreteColleague2 : Colleague
{
    // Constructor
    public ConcreteColleague2(Mediator mediator)
        : base(mediator)
    {
    }
    public void Send(string message)
    {
        mediator.Send(message, this);
    }
    public void Notify(string message)
    {
        Console.WriteLine("Colleague2 gets message: "
            + message);
    }
}