void Main()
{
	var colors = new Table<Color>(); 
	
	for(int i =0;i<=9;i++)
		colors.Add(i,new Color { Name = "red"+i, Age = i });
	
	//System.Linq.Enumerable.ToList<Color>(colors).Dump();
	

	
	//colors.ToList().Where(k=> k.Age==5).Dump();
	
	foreach (Color theColor in colors)  
    {  
        (theColor.Name + " ").Dump();  
    } 
	
}

// Define other methods and classes here
// Collection class.  
public class Table<T> : System.Collections.IEnumerable  
{  
    T[] _colors = new T[10];  
    /*{  
        new T() { Name = "red" },  
        new T() { Name = "blue" },  
        new T() { Name = "green" }  
    };*/  
	
	public void Add(int index,T t)
	{
		_colors[index] = (t);
	}

    public System.Collections.IEnumerator GetEnumerator()  
    {  
        return new TEnumerator(_colors);  

        // Instead of creating a custom enumerator, you could  
        // use the GetEnumerator of the array.  
        //return _colors.GetEnumerator();  
    }  

    // Custom enumerator.  
    private class TEnumerator : System.Collections.IEnumerator  
    {  
        private T[] _colors;  
        private int _position = -1;  

        public TEnumerator(T[] colors)  
        {  
            _colors = colors;  
        }  

        object System.Collections.IEnumerator.Current  
        {  
            get  
            {  
                return _colors[_position];  
            }  
        }  

        bool System.Collections.IEnumerator.MoveNext()  
        {  
            _position++;  
            return (_position < _colors.Length);  
        }  

        void System.Collections.IEnumerator.Reset()  
        {  
            _position = -1;  
        }  
    }  
}  

// Element class.  
public class Color  
{  
    public string Name { get; set; } 
	public int Age {get;set;}
}
