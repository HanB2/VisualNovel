using System;

namespace Minalear
{
    public static class RNG
    {
        public static Random Random = new Random();

        //Floats
        public static float NextFloat()
        {
            return Random.NextFloat();
        }
        public static float NextFloat(float max)
        {
            return Random.NextFloat(max);
        }
        public static float NextFloat(float min, float max)
        {
            return Random.NextFloat(min, max);
        }

        //Ints
        public static int Next()
        {
            return Random.Next();
        }
        public static int Next(int max)
        {
            return Random.Next(max);
        }
        public static int Next(int min, int max)
        {
            return Random.Next(min, max);
        }

        //Doubles
        public static double NextDouble()
        {
            return Random.NextDouble();
        }
        public static double NextDouble(double max)
        {
            return Random.NextDouble() * max;
        }
        public static double NextDouble(double min, double max)
        {
            return Random.NextDouble() * min + (max - min);
        }

        //Bytes
        public static void NextBytes(byte[] buffer)
        {
            Random.NextBytes(buffer);
        }

        public static float NextFloat(this Random r)
        {
            return (float)r.NextDouble();
        }
        public static float NextFloat(this Random r, float max)
        {
            return max * (float)r.NextDouble();
        }
        public static float NextFloat(this Random r, float min, float max)
        {
            return min + (max - min) * (float)r.NextDouble();
        }
    }
}
