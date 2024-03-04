<Query Kind="Program" />

void Main()
{
	"The Iterator design pattern provides a way to access the elements of an aggregate object sequentially without exposing its underlying representation.".Dump();
	
	ConcreteAggregate a = new ConcreteAggregate();
    a[0] = "Item A";
    a[1] = "Item B";
    a[2] = "Item C";
    a[3] = "Item D";
    // Create Iterator and provide aggregate
    Iterator i = a.CreateIterator();
    Console.WriteLine("Iterating over collection:");
    object item = i.First();
    while (item != null)
    {
        Console.WriteLine(item);
        item = i.Next();
    }
}

// Define other methods and classes here
/// <summary>
/// The 'Aggregate' abstract class
/// </summary>
public abstract class Aggregate
{
    public abstract Iterator CreateIterator();
}

/// <summary>
/// The 'ConcreteAggregate' class
/// </summary>
public class ConcreteAggregate : Aggregate
{
    List<object> items = new List<object>();
    public override Iterator CreateIterator()
    {
        return new ConcreteIterator(this);
    }
    // Get item count
    public int Count
    {
        get { return items.Count; }
    }
    // Indexer
    public object this[int index]
    {
        get { return items[index]; }
        set { items.Insert(index, value); }
    }
}

/// <summary>
/// The 'Iterator' abstract class
/// </summary>
public abstract class Iterator
{
    public abstract object First();
    public abstract object Next();
    public abstract bool IsDone();
    public abstract object CurrentItem();
}

/// <summary>
/// The 'ConcreteIterator' class
/// </summary>
public class ConcreteIterator : Iterator
{
    ConcreteAggregate aggregate;
    int current = 0;
    // Constructor
    public ConcreteIterator(ConcreteAggregate aggregate)
    {
        this.aggregate = aggregate;
    }
    // Gets first iteration item
    public override object First()
    {
        return aggregate[0];
    }
    // Gets next iteration item
    public override object Next()
    {
        object ret = null;
        if (current < aggregate.Count - 1)
        {
            ret = aggregate[++current];
        }
        return ret;
    }
    // Gets current iteration item
    public override object CurrentItem()
    {
        return aggregate[current];
    }
    // Gets whether iterations are complete
    public override bool IsDone()
    {
        return current >= aggregate.Count;
    }
}