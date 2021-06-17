void Main()
{
	var customers = new Collection<Customer>();
	
	for(int i=0;i<9;i++)
	{
		var customer = new Customer();
	    customer.CustomerID = Guid.NewGuid().ToString("N");
	    customer.FirstName = "First" + i;
	    customer.LastName = "Last" + i;
	    customer.Address = "Add" + 1;
	    customer.City = "City"+i;
	    customer.State = "lDataReader[5].ToString()";
	    customer.Country = "sqlDataReader[6].ToString()";
	    customer.Mobile = "sqlDataReader[7].ToString()";
	    customer.Mail = "sqlDataReader[8].ToString()";
	    customers.Add(customer);
	}
	
	
	customers.Get().Where(c=>c.LastName == "Last5").Select(c=> c).Dump();
	
	foreach(Customer c in customers)
	{
		if(c.LastName == "ln")
			c.Dump();
	}
	
	var r = from c in customers.Cast<Customer>() where c.LastName == "ln" select c;
	
	r.Dump();
	customers.Dump();
}

// Define other methods and classes here

[Serializable]
public class Customer
{
    public Customer(){}
    public string CustomerID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Mobile { get; set; }
    public string Mail { get; set; }
}

[Serializable]
public class Collection<T> : CollectionBase
{
	public Collection()
	{
	}

    // Gets/Sets value for the item by that index
    public T this[int index]
    {
        get
        {
            return (T)this.List[index];
        }
        set
        {
            this.List[index] = value;
        }
    }

    public int IndexOf(T customerItem)
    {
        if (customerItem != null)
        {
            return base.List.IndexOf(customerItem);
        }
        return -1;
    }

    public int Add(T customerItem)
    {
        if (customerItem != null)
        {
            return this.List.Add(customerItem);
        }
        return -1;
    }

    public void Remove(T customerItem)
    {
        this.InnerList.Remove(customerItem);
    } 

    /*public void AddRange(T collection)
    {
        if (collection != null)
        {
            this.InnerList.AddRange(collection);
        }
    }*/
	
	public IEnumerable<T> Get()
	{
		return this.List.Cast<T>();
	}

    public void Insert(int index, T customerItem)
    {
        if (index <= List.Count && customerItem != null)
        {
            this.List.Insert(index, customerItem);
        }
    }
 
    public bool Contains(T customerItem)
    {
        return this.List.Contains(customerItem);
    }
}
