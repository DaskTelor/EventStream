using System;

namespace EventStream.Sourse
{
    public class EvenlyRandom
    {
        private static void Swap<T>(ref T first, ref T second)
        {
            T temp = first;
            first = second;
            second = temp;
        }
        private Random random;
        public EvenlyRandom() => random = new Random();
        public double GetRandomDouble(double a, double b)//равномерный закон распределения
        {
            if (a > b) Swap(ref a, ref b);
            return (b - a) * random.NextDouble() + a;
        }
    }
}
