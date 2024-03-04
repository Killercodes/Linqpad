<Query Kind="Program" />

void Main()
{
	@"The Visitor design pattern represents an operation to be performed on the elements of an object structure.
	This pattern lets you define a new operation without changing the classes of the elements on which it operates.".Dump("Visitor Design Pattern");
	
	// Setup structure
    ObjectStructure o = new ObjectStructure();
    o.Attach(new ConcreteElementA());
    o.Attach(new ConcreteElementB());
	
    // Create visitor objects
    ConcreteVisitor1 v1 = new ConcreteVisitor1();
    ConcreteVisitor2 v2 = new ConcreteVisitor2();
	
    // Structure accepting visitors
    o.Accept(v1);
    o.Accept(v2);
}

// Define other methods and classes here
// The 'Visitor' abstract class
public abstract class Visitor
{
    public abstract void VisitConcreteElementA(
        ConcreteElementA concreteElementA);
		
    public abstract void VisitConcreteElementB(
        ConcreteElementB concreteElementB);
}

/// A 'ConcreteVisitor' class
public class ConcreteVisitor1 : Visitor
{
    public override void VisitConcreteElementA(
        ConcreteElementA concreteElementA)
    {
        Console.WriteLine("{0} visited by {1}",
            concreteElementA.GetType().Name, this.GetType().Name);
    }
    public override void VisitConcreteElementB(
        ConcreteElementB concreteElementB)
    {
        Console.WriteLine("{0} visited by {1}",
            concreteElementB.GetType().Name, this.GetType().Name);
    }
}

/// A 'ConcreteVisitor' class
public class ConcreteVisitor2 : Visitor
{
    public override void VisitConcreteElementA(
        ConcreteElementA concreteElementA)
    {
        Console.WriteLine("{0} visited by {1}",
            concreteElementA.GetType().Name, this.GetType().Name);
    }
    public override void VisitConcreteElementB(
        ConcreteElementB concreteElementB)
    {
        Console.WriteLine("{0} visited by {1}",
            concreteElementB.GetType().Name, this.GetType().Name);
    }
}


/// The 'Element' abstract class
public abstract class Element
{
    public abstract void Accept(Visitor visitor);
}


/// A 'ConcreteElement' class
public class ConcreteElementA : Element
{
    public override void Accept(Visitor visitor)
    {
        visitor.VisitConcreteElementA(this);
    }
    public void OperationA()
    {
    }
}


/// A 'ConcreteElement' class
public class ConcreteElementB : Element
{
    public override void Accept(Visitor visitor)
    {
        visitor.VisitConcreteElementB(this);
    }
    public void OperationB()
    {
    }
}

/// The 'ObjectStructure' class
public class ObjectStructure
{
    List<Element> elements = new List<Element>();
    public void Attach(Element element)
    {
        elements.Add(element);
    }
    public void Detach(Element element)
    {
        elements.Remove(element);
    }
    public void Accept(Visitor visitor)
    {
        foreach (Element element in elements)
        {
            element.Accept(visitor);
        }
    }
}