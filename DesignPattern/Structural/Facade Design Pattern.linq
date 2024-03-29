<Query Kind="Program" />

void Main()
{
	@"The Facade design pattern provides a unified interface to a set of interfaces in a subsystem.
	This pattern defines a higher-level interface that makes the subsystem easier to use."
	.Dump("Facade Design Pattern");
	
	Facade facade = new Facade();
    facade.MethodA();
    facade.MethodB();
}

// Define other methods and classes here

/// The 'Subsystem ClassA' class
public class SubSystemOne
{
    public void MethodOne()
    {
        Console.WriteLine(" SubSystemOne Method");
    }
}


/// The 'Subsystem ClassB' class
public class SubSystemTwo
{
    public void MethodTwo()
    {
        Console.WriteLine(" SubSystemTwo Method");
    }
}

/// The 'Subsystem ClassC' class
public class SubSystemThree
{
    public void MethodThree()
    {
        Console.WriteLine(" SubSystemThree Method");
    }
}


/// The 'Subsystem ClassD' class
public class SubSystemFour
{
    public void MethodFour()
    {
        Console.WriteLine(" SubSystemFour Method");
    }
}


/// The 'Facade' class
public class Facade
{
    SubSystemOne one;
    SubSystemTwo two;
    SubSystemThree three;
    SubSystemFour four;
	
    public Facade()
    {
        one = new SubSystemOne();
        two = new SubSystemTwo();
        three = new SubSystemThree();
        four = new SubSystemFour();
    }
	
    public void MethodA()
    {
        Console.WriteLine("\nMethodA() ---- ");
        one.MethodOne();
        two.MethodTwo();
        four.MethodFour();
    }
    public void MethodB()
    {
        Console.WriteLine("\nMethodB() ---- ");
        two.MethodTwo();
        three.MethodThree();
    }
}