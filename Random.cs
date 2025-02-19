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
        public static int Next(int min, int max)
        {
            return random.Next(min, max);
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
