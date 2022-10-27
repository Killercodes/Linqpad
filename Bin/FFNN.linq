<Query Kind="Program" />

void Main()
{
	FeedForwardProgram.Run();
}

// Define other methods and classes here
public class FeedForwardProgram
  {
    public static void Run()
    {
      try
      {
        Console.WriteLine("\nBegin neural network feed-forward demo\n");

        Console.WriteLine("Creating a 3-input, 4-hidden, 2-output neural network");
        Console.WriteLine("Using log-sigmoid function for input-to-hidden and hidden-to-output activation");

        const int numInput = 3;
        const int numHidden = 4;
        const int numOutput = 2;

        NeuralNetwork nn = new NeuralNetwork(numInput, numHidden, numOutput);
		nn.Print += s => Console.WriteLine("  "+s);

        const int numWeights = (numInput * numHidden) + (numHidden * numOutput) + numHidden + numOutput; // (3*4)+(4*2)+4+2 = 26
		NeuralNetwork.CalculateWeight(3,4,2).Dump();

        double[] weights = new double[numWeights] {
          0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0, 1.1, 1.2,
          -2.0, -6.0, -1.0, -7.0,
          1.3, 1.4, 1.5, 1.6, 1.7, 1.8, 1.9, 2.0,
          -2.5, -5.0 };

        Console.WriteLine("\nWeights and biases are:");
        ShowVector(weights, 2);

        Console.WriteLine("Loading neural network weights and biases");
        nn.SetWeights(weights);

        Console.WriteLine("\nSetting neural network inputs:");
        double[] xValues = new double[] { 2.0, 3.0, 4.0 };
        ShowVector(xValues, 2);

        Console.WriteLine("Loading inputs and computing outputs\n");
        double[] yValues = nn.ComputeOutputs(xValues);

        Console.WriteLine("\nNeural network outputs are:");
        ShowVector(yValues, 4);

        Console.WriteLine("\nEnd neural network demo\n");
        //Console.ReadLine();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        Console.ReadLine();
      }
    } // Main

    public static void ShowVector(double[] vector, int decimals)
    {
      for (int i = 0; i < vector.Length; ++i)
      {
        if (i > 0 && i % 12 == 0) // max of 12 values per row 
          Console.WriteLine("");
        if (vector[i] >= 0.0) Console.Write(" ");
        Console.Write(vector[i].ToString("F" + decimals) + " "); // 2 decimals
      }
      Console.WriteLine("\n");
    }

    public static void ShowMatrix(double[][] matrix, int numRows)
    {
      int ct = 0;
      if (numRows == -1) numRows = int.MaxValue; // if numRows == -1, show all rows
      for (int i = 0; i < matrix.Length && ct < numRows; ++i)
      {
        for (int j = 0; j < matrix[0].Length; ++j)
        {
          if (matrix[i][j] >= 0.0) Console.Write(" "); // blank space instead of '+' sign
          Console.Write(matrix[i][j].ToString("F2") + " ");
        }
        Console.WriteLine("");
        ++ct;
      }
      Console.WriteLine("");
    }

  } // class Program

  public class NeuralNetwork
  {
    public delegate void PrintEventHandler(object message);
    public event PrintEventHandler Print;
    private int numInput;
    private int numHidden;
    private int numOutput;

    private double[] inputs;
    private double[][] ihWeights; // input-to-hidden
    private double[] ihBiases;
    private double[][] hoWeights; // hidden-to-output
    private double[] hoBiases;
    private double[] outputs;
	
	public void WriteLine(string format, object[] args){
      string message = string.Format(format,args);
      if(this.Print != null){
        this.Print(message);
      }
    }
	
	public void WriteLine(string message){      
      if(this.Print != null){
        this.Print(message);
      }
    }

    public NeuralNetwork(int numInput, int numHidden, int numOutput)
    {
      this.numInput = numInput;
      this.numHidden = numHidden;
      this.numOutput = numOutput;

      inputs = new double[numInput];
      ihWeights = MakeMatrix(numInput, numHidden);
      ihBiases = new double[numHidden];
      hoWeights = MakeMatrix(numHidden, numOutput);
      hoBiases = new double[numOutput];
      outputs = new double[numOutput]; // aka hoOutputs
    }

    private static double[][] MakeMatrix(int rows, int cols)
    {
      double[][] result = new double[rows][];
      for (int i = 0; i < rows; ++i)
        result[i] = new double[cols];
      return result;
    }

    public void SetWeights(double[] weights)
    {
      // order: ihWeights (row-col order), ihBiases, hoWeights, hoBiases 
      int numWeights = (numInput * numHidden) + (numHidden * numOutput) + numHidden + numOutput;
      if (weights.Length != numWeights)
        throw new Exception("The weights array length: " + weights.Length + " does not match the total number of weights and biases: " + numWeights);

      int k = 0; // points into weights param

      for (int i = 0; i < numInput; ++i)
        for (int j = 0; j < numHidden; ++j)
          ihWeights[i][j] = weights[k++];

      for (int i = 0; i < numHidden; ++i)
        ihBiases[i] = weights[k++];

      for (int i = 0; i < numHidden; ++i)
        for (int j = 0; j < numOutput; ++j)
          hoWeights[i][j] = weights[k++];

      for (int i = 0; i < numOutput; ++i)
        hoBiases[i] = weights[k++];
    }

    public double[] ComputeOutputs(double[] xValues)
    {
      if (xValues.Length != numInput)
        throw new Exception("Inputs array length " + inputs.Length + " does not match NN numInput value " + numInput);

      double[] ihSums = new double[this.numHidden]; // these scratch arrays could be class members
      double[] ihOutputs = new double[this.numHidden];
      double[] hoSums = new double[this.numOutput];

      for (int i = 0; i < xValues.Length; ++i) // copy x-values to inputs
        this.inputs[i] = xValues[i];

      WriteLine("Inputs:");
      FeedForwardProgram.ShowVector(this.inputs, 2);

      WriteLine("input-to-hidden weights:");
      FeedForwardProgram.ShowMatrix(this.ihWeights, -1);

      for (int j = 0; j < numHidden; ++j)  // compute input-to-hidden weighted sums
        for (int i = 0; i < numInput; ++i)
          ihSums[j] += this.inputs[i] * ihWeights[i][j];

      WriteLine("input-to-hidden sums before adding i-h biases:");
      FeedForwardProgram.ShowVector(ihSums, 2);

      WriteLine("input-to-hidden biases:");
      FeedForwardProgram.ShowVector(this.ihBiases, 2);

      for (int i = 0; i < numHidden; ++i)  // add biases to input-to-hidden sums
        ihSums[i] += ihBiases[i];

      WriteLine("input-to-hidden sums after adding i-h biases:");
      FeedForwardProgram.ShowVector(ihSums, 2);

      for (int i = 0; i < numHidden; ++i)   // determine input-to-hidden output
        ihOutputs[i] = LogSigmoid(ihSums[i]);

      WriteLine("input-to-hidden outputs after log-sigmoid activation:");
      FeedForwardProgram.ShowVector(ihOutputs, 2);

      WriteLine("hidden-to-output weights:");
      FeedForwardProgram.ShowMatrix(hoWeights, -1);

      for (int j = 0; j < numOutput; ++j)   // compute hidden-to-output weighted sums
        for (int i = 0; i < numHidden; ++i)
          hoSums[j] += ihOutputs[i] * hoWeights[i][j];

      WriteLine("hidden-to-output sums before adding h-o biases:");
      FeedForwardProgram.ShowVector(hoSums, 2);

      WriteLine("hidden-to-output biases:");
      FeedForwardProgram.ShowVector(this.hoBiases, 2);

      for (int i = 0; i < numOutput; ++i)  // add biases to input-to-hidden sums
        hoSums[i] += hoBiases[i];

      WriteLine("hidden-to-output sums after adding h-o biases:");
      FeedForwardProgram.ShowVector(hoSums, 2);

      for (int i = 0; i < numOutput; ++i)   // determine hidden-to-output result
        this.outputs[i] = LogSigmoid(hoSums[i]);

      double[] result = new double[numOutput]; // copy hidden-to-output to this.outputs
      this.outputs.CopyTo(result, 0);

      return result;
    } // ComputeOutputs

    private static double LogSigmoid(double z)
    {
      if (z < -20.0) return 0.0;
      else if (z > 20.0) return 1.0;
      else return 1.0 / (1.0 + Math.Exp(-z));
    }

    //extend
    public static int CalculateWeight(int inputNodeCount, int hiddenNodeCount,int outputNodeCount)
    {
      int numWeights = (inputNodeCount * hiddenNodeCount) + (hiddenNodeCount * outputNodeCount) + hiddenNodeCount + outputNodeCount; 		   //count the total weighs
      return numWeights;
    }



  } // class