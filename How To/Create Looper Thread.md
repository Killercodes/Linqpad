# Looper Thread

```cs
void Main()
{
	LooperThread tm = LooperThread.Start(()=>Console.WriteLine("-"+DateTime.Now.ToString("HH.mm.ss.fffff")));
    
	//tm.Stop();
}

// Define other methods and classes here
class LooperThread
    {
        private ManualResetEvent shutdown = new ManualResetEvent(false);
        private Thread thread;
        public Action Action;

        private LooperThread(Action action)
        {
            Action = action;
        }
		
        public static LooperThread Start(Action action)
        {
            var tm = new LooperThread(action);
            tm.Init();
            return tm;
        }

        private void Init()
        {
            thread = new Thread(MyThreadFunc);
            thread.Name = "MyThreadFunc";
            thread.IsBackground = true;
            thread.Start();
        }

        public void Stop()
        {
            shutdown.Set();
            if (!thread.Join(100)) //2 sec to stop 
            {
                thread.Abort();
            }
        }

        void MyThreadFunc()
        {
            while (!shutdown.WaitOne(0))
            {
                // call with the work you need to do
                try
                {
                    Action.Invoke();
                    /*
                    RuntimeHelpers.PrepareConstrainedRegions();
                    try { }
                    finally
                    {
                        // do something not to be aborted
                    }*/
                }
                catch (ThreadAbortException e)
                {
					Console.WriteLine(e.Message);
                    // handle the exception 
                }
            }
        }
    }
 ```
