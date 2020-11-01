namespace X_Log.XParallel
{
    public class ParallelTask
    {
        private static uint _id;

        public readonly Parallel.TaskDelegate Delegate;

        public uint Id = _id++;

        public ParallelTask(Parallel.TaskDelegate taskDelegate)
        {
            Completed = false;
            Delegate = taskDelegate;
        }

        public bool Completed { get; private set; }

        public void Run()
        {
            Delegate.Invoke();
            Completed = true;
        }
    }
}