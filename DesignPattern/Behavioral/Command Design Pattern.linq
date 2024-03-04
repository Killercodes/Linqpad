<Query Kind="Program" />

void Main()
{
	@"The Command design pattern encapsulates a request as an object, thereby letting you parameterize 
	clients with different requests, queue or log requests, and support undoable operations.".Dump("Command Design Pattern");
	
	// Create receiver, command, and invoker
    Receiver receiver = new Receiver();
    Command command = new ConcreteCommand(receiver);
    Invoker invoker = new Invoker();
    // Set and execute command
    invoker.SetCommand(command);
    invoker.ExecuteCommand();
}

// Define other methods and classes here
/// <summary>
/// The 'Command' abstract class
/// </summary>
public abstract class Command
{
    protected Receiver receiver;
    // Constructor
    public Command(Receiver receiver)
    {
        this.receiver = receiver;
    }
    public abstract void Execute();
}

/// <summary>
/// The 'ConcreteCommand' class
/// </summary>
public class ConcreteCommand : Command
{
    // Constructor
    public ConcreteCommand(Receiver receiver) :
        base(receiver)
    {
    }
    public override void Execute()
    {
        receiver.Action();
    }
}

/// <summary>
/// The 'Receiver' class
/// </summary>
public class Receiver
{
    public void Action()
    {
        Console.WriteLine("Called Receiver.Action()");
    }
}

/// <summary>
/// The 'Invoker' class
/// </summary>
public class Invoker
{
    Command command;
    public void SetCommand(Command command)
    {
        this.command = command;
    }
    public void ExecuteCommand()
    {
        command.Execute();
    }
}