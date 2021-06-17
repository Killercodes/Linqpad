<Query Kind="Program" />

void Main()
{
	BruteForceTest testCallback = delegate(ref char[] testChars)
	{
		var str = new string(testChars);
		Console.WriteLine(str);
		return (str == "bbcee");
	};
 
 
            bool result = BruteForce("abcde", 1, 5, testCallback);
            Console.WriteLine(result);
}

// Define other methods and classes here
public delegate bool BruteForceTest(ref char[] testChars);
 
public static bool BruteForce(string testChars, int startLength, int endLength, BruteForceTest testCallback)
{
  for (int len = startLength; len <= endLength; ++len)
  {
      char[] chars = new char[len];

      for (int i = 0; i < len; ++i)
          chars[i] = testChars[0];

      if (testCallback(ref chars))
          return true;

      for (int i1 = len - 1; i1 > -1; --i1)
      {
          int i2 = 0;

          for (i2 = testChars.IndexOf(chars[i1]) + 1; i2 < testChars.Length; ++i2)
          {
              chars[i1] = testChars[i2];

              if (testCallback(ref chars))
                  return true;

              for (int i3 = i1 + 1; i3 < len; ++i3)
              {
                  if (chars[i3] != testChars[testChars.Length - 1])
                  {
                      i1 = len;
                      goto outerBreak;
                  }
              }
          }

      outerBreak:
          if (i2 == testChars.Length)
              chars[i1] = testChars[0];
      }
  }

  return false;
}