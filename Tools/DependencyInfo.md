# Dependency Info

Runs agains all the `*.dll` and `*.exe` file in the folder and listes them out in the console

```cs
void Main()
{
	var path = @"C:\services";
	//DependencyInfo();
	
	//string[] filePaths = Directory.GetFiles(path, "*.dll",SearchOption.AllDirectories);
	
	var filePaths = Directory.EnumerateFiles(path)
    .Where(file => file.ToLower().EndsWith("dll") || file.ToLower().EndsWith("exe")).ToList();
	
	//filePaths.Dump();
	foreach(var f in filePaths)
		DependencyInfo(f);

}

// Define other methods and classes here
public static int DependencyInfo(string args) 
{
	"".Dump();
	Path.GetFileName (args).Dump();
	Assembly.LoadFile(args).FullName.Dump();
	//Assembly.LoadFile(args).GetCustomAttributes(typeof(System.Runtime.Versioning.TargetFrameworkAttribute), false).SingleOrDefault().Dump();
	try {
		var assemblies = Assembly.LoadFile(args).GetReferencedAssemblies();	
		
		if (assemblies.GetLength(0) > 0)
		{
			foreach (var assembly in assemblies)
			{
				//Console.WriteLine(assembly);
				
				Console.WriteLine(" - " + assembly.FullName + ", ProcessorArchitecture=" + assembly.ProcessorArchitecture);
				//Console.WriteLine("   -" + Assembly.Load(assembly.FullName).GetCustomAttributes(typeof(System.Runtime.Versioning.TargetFrameworkAttribute), false).SingleOrDefault());
			}
			return 0;
		}
	}
	catch(Exception e) {
		Console.WriteLine("An exception occurred: {0}", e.Message);
		return 1;
	} 
	finally{}

  	return 1;
}
```
