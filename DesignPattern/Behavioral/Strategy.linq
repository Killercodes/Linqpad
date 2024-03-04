<Query Kind="Program" />

void Main()
{
	@"The Strategy design pattern defines a family of algorithms, encapsulate each one, and make them interchangeable.
	This pattern lets the algorithm vary independently from clients that use it.".Dump();
	
	Context context;
    // Three contexts following different strategies
    context = new Context(new ConcreteStrategyA());
    context.ContextInterface();
    context = new Context(new ConcreteStrategyB());
    context.ContextInterface();
    context = new Context(new ConcreteStrategyC());
    context.ContextInterface();

}

// Define other methods and classes here
/// <summary>
/// The 'Strategy' abstract class
/// </summary>
public abstract class Strategy
{
    public abstract void AlgorithmInterface();
}

/// <summary>
/// A 'ConcreteStrategy' class
/// </summary>
public class ConcreteStrategyA : Strategy
{
    public override void AlgorithmInterface()
    {
        Console.WriteLine(
            "Called ConcreteStrategyA.AlgorithmInterface()");
    }
}

/// <summary>
/// A 'ConcreteStrategy' class
/// </summary>
public class ConcreteStrategyB : Strategy
{
    public override void AlgorithmInterface()
    {
        Console.WriteLine(
            "Called ConcreteStrategyB.AlgorithmInterface()");
    }
}

/// <summary>
/// A 'ConcreteStrategy' class
/// </summary>
public class ConcreteStrategyC : Strategy
{
    public override void AlgorithmInterface()
    {
        Console.WriteLine(
            "Called ConcreteStrategyC.AlgorithmInterface()");
    }
}

/// <summary>
/// The 'Context' class
/// </summary>
public class Context
{
    Strategy strategy;
    // Constructor
    public Context(Strategy strategy)
    {
        this.strategy = strategy;
    }
    public void ContextInterface()
    {
        strategy.AlgorithmInterface();
    }
}