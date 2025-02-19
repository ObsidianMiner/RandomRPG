using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRPG
{
    public class MultiplierList
    {
        Dictionary<string, float> multipliers;

        public static MultiplierList empty => new MultiplierList(new Dictionary<string, float>());
        public MultiplierList(Dictionary<string, float> multipliers)
        {
            this.multipliers = multipliers;
        }
        public float Evaluate()
        {
            float value = 1;
            foreach (float mult in multipliers.Values)
            {
                value *= mult;
            }
            return value;
        }
        /// <summary>
        /// Updates the multiplier if it already exists.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddMult(string name, float value)
        {
            multipliers[name] = value;
        }
        public void RemoveMult(string name)
        {
            multipliers.Remove(name);
        }
        public bool HasMult(string name) => multipliers.ContainsKey(name);
        public float GetMult(string name) => multipliers[name];
        public static float operator *(MultiplierList multiplierList, float a) => multiplierList.Evaluate() * a;
        public static float operator *(float a, MultiplierList multiplierList) => multiplierList.Evaluate() * a;
        public static float operator /(MultiplierList multiplierList, float a) => multiplierList.Evaluate() / a;
        public static float operator /(float a, MultiplierList multiplierList) => a / multiplierList.Evaluate();
    }
}
