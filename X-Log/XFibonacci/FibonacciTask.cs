namespace X_Log.XFibonacci
{
    public class FibonacciTask
    {
        public readonly uint N;
        public uint? Result;

        public FibonacciTask(uint n)
        {
            N = n;
        }

        public void Execute()
        {
            Result = Fibonacci.Calculate(N);
        }
    }
}