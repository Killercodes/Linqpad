<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.DataVisualization.dll</Reference>
  <Namespace>System</Namespace>
  <Namespace>System.Collections</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Collections.Specialized</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.IO.Compression</Namespace>
  <Namespace>System.IO.Pipes</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Binary</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.ServiceModel.Description</Namespace>
  <Namespace>System.Text</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Windows.Forms.DataVisualization.Charting</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

void Main()
{
	/*
	Prototype is a creational design pattern that allows cloning objects, even complex ones, without coupling to their
	specific classes.

	All prototype classes should have a common interface that makes it possible to copy objects even if their concrete 
	classes are unknown. Prototype objects can produce full copies since objects of the same class can access each other’s 
	private fields.
	*/
	Person p1 = new Person();
    p1.Age = 42;
    p1.BirthDate = Convert.ToDateTime("1977-01-01");
    p1.Name = "Jack Daniels";
    p1.IdInfo = new IdInfo(666);

    // Perform a shallow copy of p1 and assign it to p2.
    Person p2 = p1.ShallowCopy();
    // Make a deep copy of p1 and assign it to p3.
    Person p3 = p1.DeepCopy();

    // Display values of p1, p2 and p3.
    Console.WriteLine("Original values of p1, p2, p3:");
    Console.WriteLine("   p1 instance values: ");
    DisplayValues(p1);
    Console.WriteLine("   p2 instance values:");
    DisplayValues(p2);
    Console.WriteLine("   p3 instance values:");
    DisplayValues(p3);

    // Change the value of p1 properties and display the values of p1,
    // p2 and p3.
    p1.Age = 32;
    p1.BirthDate = Convert.ToDateTime("1900-01-01");
    p1.Name = "Frank";
    p1.IdInfo.IdNumber = 7878;
    Console.WriteLine("\nValues of p1, p2 and p3 after changes to p1:");
    Console.WriteLine("   p1 instance values: ");
    DisplayValues(p1);
    Console.WriteLine("   p2 instance values (reference values have changed):");
    DisplayValues(p2);
    Console.WriteLine("   p3 instance values (everything was kept the same):");
    DisplayValues(p3);
}

public static void DisplayValues(Person p)
{
    Console.WriteLine("      Name: {0:s}, Age: {1:d}, BirthDate: {2:MM/dd/yy}",
        p.Name, p.Age, p.BirthDate);
    Console.WriteLine("      ID#: {0:d}", p.IdInfo.IdNumber);
}

// Define other methods and classes here
public class Person
{
    public int Age;
    public DateTime BirthDate;
    public string Name;
    public IdInfo IdInfo;

    public Person ShallowCopy()
    {
        return (Person) this.MemberwiseClone();
    }

    public Person DeepCopy()
    {
        Person clone = (Person) this.MemberwiseClone();
        clone.IdInfo = new IdInfo(IdInfo.IdNumber);
        clone.Name = String.Copy(Name);
        return clone;
    }
}

public class IdInfo
{
    public int IdNumber;

    public IdInfo(int idNumber)
    {
        this.IdNumber = idNumber;
    }
}