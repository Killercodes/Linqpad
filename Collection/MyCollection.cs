void Main()
{
	MyCollection et = MyCollection.Initialize();
	et.Add("one");et.Add("two");

            foreach (var VARIABLE in et)
            {
                VARIABLE.Dump();
            }
}

// Define other methods and classes here
public class MyCollection:IEnumerable
    {
        ArrayList _array = new ArrayList();
		
		private MyCollection(){}
			
		public static MyCollection Initialize(){
			return (new MyCollection());
		}

        public void Add(object obj)
        {
            _array.Add(obj);
        }

        public void Add<T>(T t)
        {
            _array.Add(t);
        }

        public object Get(int index)
        {
            return _array[index];
        }
        public T Get<T>(int index)
        {
            return (T) _array[index];
        }

        public IEnumerator GetEnumerator()
        {
             return _array.GetEnumerator();
        }
		
    }
