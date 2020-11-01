namespace X_Log.XFibonacci
{
    public class Fibonacci
    {
        public static uint Calculate(uint n)
        {
            if (n <= 1) return n;

            return Calculate(n - 1) + Calculate(n - 2);
        }
    }
}