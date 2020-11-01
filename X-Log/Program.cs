using System.Collections.Generic;
using X_Log.XFibonacci;
using X_Log.XLog;
using X_Log.XParallel;

namespace X_Log
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var logger = new LogBuffer(20, 10000);
            Parallel.Logger = logger;

            var tasks = new List<FibonacciTask>();
            for (uint i = 5; i <= 40; i += 5)
            {
                var task = new FibonacciTask(i);
                tasks.Add(task);
                logger.Add("Created " + nameof(FibonacciTask) + " with " + nameof(FibonacciTask.N) + " = " + task.N);
            }

            var delegates = new List<Parallel.TaskDelegate>();
            foreach (var task in tasks) delegates.Add(task.Execute);

            logger.Add("Performing " + tasks.Count + " tasks using " + nameof(Parallel.WaitAll));
            Parallel.WaitAll(delegates);

            foreach (var task in tasks)
                logger.Add(nameof(FibonacciTask) + " with " + nameof(FibonacciTask.N) + " = " + task.N + " is " +
                           task.Result);

            logger.Add("Terminating application");
            logger.Flush();
        }
    }
}