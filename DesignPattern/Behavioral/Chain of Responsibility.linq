<Query Kind="Program">
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
{
	@"The Chain of Responsibility design pattern avoids coupling the sender of a request to its receiver by giving more than one object a chance to handle the request. 
	This pattern chains the receiving objects and passes the request along the chain until an object handles it".Dump("Chain of Responsibility");
	
	// Setup Chain of Responsibility
    Handler h1 = new ConcreteHandler1();
    Handler h2 = new ConcreteHandler2();
    Handler h3 = new ConcreteHandler3();
    h1.SetSuccessor(h2);
    h2.SetSuccessor(h3);
    // Generate and process request
    int[] requests = { 2, 5, 14, 22, 18, 3, 27, 20 };
    foreach (int request in requests)
    {
        h1.HandleRequest(request);
    }
}

// Define other methods and classes here
/// <summary>
/// The 'Handler' abstract class
/// </summary>
public abstract class Handler
{
    protected Handler successor;
    public void SetSuccessor(Handler successor)
    {
        this.successor = successor;
    }
    public abstract void HandleRequest(int request);
}

/// <summary>
/// The 'ConcreteHandler1' class
/// </summary>
public class ConcreteHandler1 : Handler
{
    public override void HandleRequest(int request)
    {
        if (request >= 0 && request < 10)
        {
            Console.WriteLine("{0} handled request {1}",
                this.GetType().Name, request);
        }
        else if (successor != null)
        {
            successor.HandleRequest(request);
        }
    }
}

/// <summary>
/// The 'ConcreteHandler2' class
/// </summary>
public class ConcreteHandler2 : Handler
{
    public override void HandleRequest(int request)
    {
        if (request >= 10 && request < 20)
        {
            Console.WriteLine("{0} handled request {1}",
                this.GetType().Name, request);
        }
        else if (successor != null)
        {
            successor.HandleRequest(request);
        }
    }
}

/// <summary>
/// The 'ConcreteHandler3' class
/// </summary>
public class ConcreteHandler3 : Handler
{
    public override void HandleRequest(int request)
    {
        if (request >= 20 && request < 30)
        {
            Console.WriteLine("{0} handled request {1}",
                this.GetType().Name, request);
        }
        else if (successor != null)
        {
            successor.HandleRequest(request);
        }
    }
}