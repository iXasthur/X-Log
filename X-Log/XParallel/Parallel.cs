using System.Collections.Generic;

namespace X_Log.XParallel
{
    public class Parallel
    {
        public delegate void TaskDelegate();

        public static void WaitAll(List<TaskDelegate> delegates)
        {
        }
    }
}