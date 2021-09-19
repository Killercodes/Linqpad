# Load & Execute DLL Dynamically
The following code shows how can we load a dll to app doamin and execute it dynamically. 
It loads a dll `"C:\\Program Files (x86)\\LINQPad4\\test2.dll"` and call the `Hello Run,TestNoparameters & Execute` function from inside.

```cs
void Main()
{
	ExecuteWithReflection("Hello");
	ExecuteWithReflection("Run","jo");
	ExecuteWithReflection("TestNoParameters");
	ExecuteWithReflection("Execute",new object[]{"jo","wo"});
}

// Define other methods and classes here

private void ExecuteWithReflection(string methodName,object parameterObject =null)
{
    Assembly assembly = Assembly.LoadFile("C:\\Program Files (x86)\\LINQPad4\\test2.dll");
    Type typeInstance = assembly.GetType("TestAssembly.Main");

    if (typeInstance != null)
    {
        MethodInfo methodInfo = typeInstance.GetMethod(methodName);
        ParameterInfo[] parameterInfo = methodInfo.GetParameters();
        object classInstance = Activator.CreateInstance(typeInstance, null);
		
        if (parameterInfo.Length == 0)
        {
            // there is no parameter we can call with 'null'
            var result = methodInfo.Invoke(classInstance, null);
        }
        else
        {
            var result = methodInfo.Invoke(classInstance,new object[] { parameterObject } );
        }
    }
}
```
