using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRPG
{
    public static class RandomUtil
    {
        static Random random = new Random();
        /// <summary>
        /// Min Inclusive, Max Exclusive
        /// </summary>
        /// <param name="min">Inclusive</param>
        /// <param name="max">Exclusive</param>
        /// <returns></returns>
        public static int Next(int min, int max)
        {
            return random.Next(min, max);
        }
        public static bool NextBool(int numerator, int denominator)
        {
            for (int i = 0; i < numerator; i++)
            {
                if (random.Next(0, denominator) == 0) return true;
            }
            return false;
        }
        public static float Next(float max)
        {
            return (float)random.NextDouble() * max;
        }
        public static double NextDouble()
        {
            return random.NextDouble();
        }
    }
}
