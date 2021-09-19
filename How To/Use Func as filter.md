# Use Func as filter

The following code shows how Func can be used as filter


```cs
List<Item> items;

void Main()
{
	items = new List<Item>
            {
                new Item(){ Id = 1, Name = "Adam" },
                new Item(){ Id = 2, Name = "Billy" },
                new Item(){ Id = 3, Name = "Cecil" }
            };
 
            Console.WriteLine("Length == 5");
            PrintItems(x => x.Name.Length == 5);
 
            Console.WriteLine("Id is odd number");
            PrintItems(r => r.Id % 2 == 1 && r.Name == "Adam");
}

// Define other methods and classes here
class Item
{
  public int Id { get; set; }
  public string Name { get; set; }
}

 void PrintItems(Func<Item, bool> condition)
{
	var selectedItems = this.items.Where(condition);

	foreach (var item in selectedItems)
	{
		Console.WriteLine("{0}, {1}", item.Id, item.Name);
	}
}
```
