using System.Threading;
using X_Log.XLog;

namespace X_Log
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var logBuffer = new LogBuffer(20, 2000);
            logBuffer.Add("1");
            logBuffer.Add("2");
            Thread.Sleep(10000);
            logBuffer.Add("3");
            logBuffer.Flush();
        }
    }
}