using System.Collections.Generic;
using X_Log.XLog;
using X_Log.XParallel;

namespace X_Log
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var logger = new LogBuffer(20, 10000);
            var delegates = new List<Parallel.TaskDelegate>();
            Parallel.WaitAll(delegates);
        }
    }
}