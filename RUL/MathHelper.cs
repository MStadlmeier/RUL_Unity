using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUL
{
    internal static class MathHelper
    {
        private const float EPSILON = 0.00001F;

        /// <summary>
        /// Clamps the value between lower and upper bound
        /// </summary>
        internal static int Clamp(int value, int lowerBound, int upperBound)
        {
            if (lowerBound < upperBound)
            {
                if (value < lowerBound)
                    return lowerBound;
                else if (value > upperBound)
                    return upperBound;
                else
                    return value;
            }
            else if (lowerBound == upperBound)
                return lowerBound;
            else
                throw new ArgumentException("Lower bound must be less than upper bound");
        }

        /// <summary>
        /// Clamps the value between lower and upper bound
        /// </summary>
        internal static long Clamp(long value, long lowerBound, long upperBound)
        {
            if (lowerBound < upperBound)
            {
                if (value < lowerBound)
                    return lowerBound;
                else if (value > upperBound)
                    return upperBound;
                else
                    return value;
            }
            else if (lowerBound == upperBound)
                return lowerBound;
            else
                throw new ArgumentException("Lower bound must be less than upper bound");
        }

        /// <summary>
        /// Clamps the value between lower and upper bound
        /// </summary>
        internal static float Clamp(float value, float lowerBound, float upperBound)
        {
            if (lowerBound < upperBound)
            {
                if (value < lowerBound)
                    return lowerBound;
                else if (value > upperBound)
                    return upperBound;
                else
                    return value;
            }
            else if (lowerBound == upperBound)
                return lowerBound;
            else
                throw new ArgumentException("Lower bound must be less than upper bound");
        }

        /// <summary>
        /// Very simple floating point comparison
        /// </summary>
        internal static bool FloatsEqual(float a, float b)
        {
            return Math.Abs(a - b) < EPSILON;
        }

        internal static float NormalizeValue(float val, float currentMin, float currentMax, float newMin, float newMax)
        {
            if (currentMax - currentMin != 0)
                return (val - currentMin) / (currentMax - currentMin) * (newMax - newMin) + newMin;
            return 0;
        }
    }
}
