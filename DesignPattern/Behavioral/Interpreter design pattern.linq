<Query Kind="Program" />

void Main()
{
	@"Given a language, the Interpreter design pattern defines a representation for its
	grammar along with an interpreter that uses the representation to interpret sentences in the language.".Dump("Interpreter design pattern");
	
	Context context = new Context();
	context.Message = "CREATE";
	context.EntityName = "Account";
	
    // Usually a tree 
    List<AbstractExpression> list = new List<AbstractExpression>();
	
    // Populate 'abstract syntax tree' 
    list.Add(new TerminalExpression());
    list.Add(new NonterminalExpression());
    list.Add(new TerminalExpression());
    list.Add(new TerminalExpression());
	
    // Interpret
    foreach (AbstractExpression exp in list)
    {
        exp.Interpret(context);
    }
}

// Define other methods and classes here


/// <summary>
/// The 'Context' class
/// </summary>
public class Context
{
	public DateTime ExecutionTime {get;private set;} = DateTime.Now;
	public string Message {get;set;}
	public string EntityName {get;set;}
	public Guid Id {get;set;} = Guid.NewGuid();
}

/// <summary>
/// The 'AbstractExpression' abstract class
/// </summary>
public abstract class AbstractExpression
{
    public abstract void Interpret(Context context);
}

/// <summary>
/// The 'TerminalExpression' class
/// </summary>
public class TerminalExpression : AbstractExpression
{
    public override void Interpret(Context context)
    {
		
        Console.WriteLine("Called Terminal.Interpret()");
		$"{context.Id} - {context.Message} - {context.EntityName}".Dump("Called Terminal.Interpret()");
    }
}

/// <summary>
/// The 'NonterminalExpression' class
/// </summary>
public class NonterminalExpression : AbstractExpression
{
    public override void Interpret(Context context)
    {
        Console.WriteLine("Called Nonterminal.Interpret()");
		$"{context.Id} - {context.Message} - {context.EntityName}".Dump("Called Nonterminal.Interpret()");
    }
}