<Query Kind="Program" />

void Main()
{
	@"The Memento Design Pattern without violating encapsulation, captures and externalizes an object‘s
	internal state so that the object can be restored to this state later.".Dump("Memento design pattern");
	
	Originator o = new Originator();
    o.State = "On";
	
    // Store internal state
    Caretaker c = new Caretaker();
    c.Memento = o.CreateMemento();
	
    // Continue changing originator
    o.State = "Off";
	
    // Restore saved state
    o.SetMemento(c.Memento);
}

// Define other methods and classes here
/// <summary>
/// The 'Originator' class
/// </summary>
public class Originator
{
    string state;
    public string State
    {
        get { return state; }
        set
        {
            state = value;
            Console.WriteLine("State = " + state);
        }
    }
	
    // Creates memento 
    public Memento CreateMemento()
    {
        return (new Memento(state));
    }
	
    // Restores original state
    public void SetMemento(Memento memento)
    {
        Console.WriteLine("Restoring state...");
        State = memento.State;
    }
}

/// <summary>
/// The 'Memento' class
/// </summary>
public class Memento
{
    string state;
    // Constructor
    public Memento(string state)
    {
        this.state = state;
    }
    public string State
    {
        get { return state; }
    }
}

/// <summary>
/// The 'Caretaker' class
/// </summary>
public class Caretaker
{
    Memento memento;
    public Memento Memento
    {
        set { memento = value; }
        get { return memento; }
    }
}