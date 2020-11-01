using System.Numerics;

namespace X_Log.XFibonacci
{
    public class Fibonacci
    {
        public static BigInteger Calculate(uint n)
        {
            if (n <= 1) return n;

            BigInteger a = 0;
            BigInteger b = 1;
            while (n-- > 1) {
                BigInteger t = a;
                a = b;
                b += t;
            }
            return b;
        }
    }
}