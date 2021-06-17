void Main()
{
	var colors = new AllColors();  

    foreach (Color theColor in colors)  
    {  
        Console.WriteLine(theColor.Name + " ");  
    }  
    Console.WriteLine();  
}

// Define other methods and classes here
// Collection class.  
public class AllColors : System.Collections.IEnumerable  
{  
    Color[] _colors =  
    {  
        new Color() { Name = "red" },  
        new Color() { Name = "blue" },  
        new Color() { Name = "green" }  
    };  

    public System.Collections.IEnumerator GetEnumerator()  
    {  
        return new ColorEnumerator(_colors);  

        // Instead of creating a custom enumerator, you could  
        // use the GetEnumerator of the array.  
        //return _colors.GetEnumerator();  
    }  

    // Custom enumerator.  
    private class ColorEnumerator : System.Collections.IEnumerator  
    {  
        private Color[] _colors;  
        private int _position = -1;  

        public ColorEnumerator(Color[] colors)  
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
}
