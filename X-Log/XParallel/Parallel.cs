using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using X_Log.XLog;

namespace X_Log.XParallel
{
    public class Parallel
    {
        public delegate void TaskDelegate();

        public static LogBuffer Logger = null;

        public static void WaitAll(List<TaskDelegate> delegates, LogBuffer logger = null)
        {
            if (delegates == null) throw new ArgumentNullException(nameof(delegates));
            logger ??= Logger;

            var tasks = new List<ParallelTask>();

            foreach (var taskDelegate in delegates)
            {
                var task = new ParallelTask(taskDelegate);
                tasks.Add(task);
                logger.Add("Created " + nameof(ParallelTask) + " with id = " + task.Id);
                ThreadPool.QueueUserWorkItem(ThreadPoolRun, task);
            }

            while (tasks.Count > 0)
            {
                var completedTasks = tasks.Where(task => task.Completed).ToList();

                foreach (var task in completedTasks)
                {
                    tasks.Remove(task);
                    logger.Add("Completed " + nameof(ParallelTask) + " with id = " + task.Id);
                }

                Thread.Sleep(200);
            }

            logger.Add("Completed all " + nameof(ParallelTask) + "s");
        }

        private static void ThreadPoolRun(object state)
        {
            if (state is ParallelTask task)
                task.Run();
            else
                throw new Exception("Invalid arguments in " + nameof(ThreadPoolRun) + " method.");
        }
    }
}