<Query Kind="Program" />

void Main()
{
	"The Singleton design pattern ensures a class has only one instance and provide a global point of access to it.".Dump();
	// Constructor is protected -- cannot use new
    Singleton s1 = Singleton.Instance();
    Singleton s2 = Singleton.Instance();
    // Test for same instance
    if (s1 == s2)
    {
        Console.WriteLine("Objects are the same instance");
    }
}

// Define other methods and classes here
/// <summary>
/// The 'Singleton' class
/// </summary>
public class Singleton
{
    static Singleton instance;
    // Constructor is 'protected'
    protected Singleton()
    {
    }
    public static Singleton Instance()
    {
        // Uses lazy initialization.
        // Note: this is not thread safe.
        if (instance == null)
        {
            instance = new Singleton();
        }
        return instance;
    }
}