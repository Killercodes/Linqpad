<Query Kind="Program" />

void Main()
{
	@"The Template Method design pattern defines the skeleton of an algorithm in an operation, deferring some steps to subclasses.
	This pattern lets subclasses redefine certain steps of an algorithm without changing the algorithm‘s structure.".Dump();
	
	AbstractClass aA = new ConcreteClassA();
    aA.TemplateMethod();
    AbstractClass aB = new ConcreteClassB();
    aB.TemplateMethod();

}

// Define other methods and classes here
/// <summary>
/// The 'AbstractClass' abstract class
/// </summary>
public abstract class AbstractClass
{
    public abstract void PrimitiveOperation1();
    public abstract void PrimitiveOperation2();
    // The "Template method"
    public void TemplateMethod()
    {
        PrimitiveOperation1();
        PrimitiveOperation2();
        Console.WriteLine("");
    }
}

/// <summary>
/// A 'ConcreteClass' class
/// </summary>
public class ConcreteClassA : AbstractClass
{
    public override void PrimitiveOperation1()
    {
        Console.WriteLine("ConcreteClassA.PrimitiveOperation1()");
    }
    public override void PrimitiveOperation2()
    {
        Console.WriteLine("ConcreteClassA.PrimitiveOperation2()");
    }
}

/// <summary>
/// A 'ConcreteClass' class
/// </summary>
public class ConcreteClassB : AbstractClass
{
    public override void PrimitiveOperation1()
    {
        Console.WriteLine("ConcreteClassB.PrimitiveOperation1()");
    }
    public override void PrimitiveOperation2()
    {
        Console.WriteLine("ConcreteClassB.PrimitiveOperation2()");
    }
}