using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RUL
{
    public static class Rul
    {
        #region Private

        private static Random _rng;
        private static int _seed;
        private static bool _initialized;

        #endregion

        #region Public

        #region  RNG settings / initialization

        public static void Initialize()
        {
            _seed = Environment.TickCount;
            _rng = new Random(_seed);
            _initialized = true;
        }

        public static void Initialize(int seed)
        {
            _rng = new Random(seed);
            _seed = seed;
            _initialized = true;
        }

        public static int Seed { get { return _seed; } }

        #endregion

        #region Random Numbers
        /// <summary>
        /// Returns a random float between 0 and 1
        /// </summary>
        public static float RandFloat()
        {
            return (float)RandDouble();
        }

        /// <summary>
        /// Returns a random float between 0 and max
        /// </summary>
        /// <param name="max">The upper bound for the random value</param>
        public static float RandFloat(float max)
        {
            return (float)RandDouble(0, max);
        }

        /// <summary>
        /// Returns a random float between min and max, both included
        /// </summary>
        /// <param name="min">The lower bound for the random value</param>
        /// <param name="max">The upper bound for the random value</param>
        public static float RandFloat(float min, float max)
        {
            return (float)RandDouble(min, max);
        }

        /// <summary>
        /// Returns a random double between 0 and 1
        /// </summary>
        public static double RandDouble()
        {
            if (!_initialized)
                Initialize();
            return _rng.NextDouble();
        }

        /// <summary>
        /// Returns a random double between 0 and max
        /// </summary>
        /// <param name="max">The upper bound for the random value</param>
        public static double RandDouble(double max)
        {
            return RandDouble() * max;
        }

        /// <summary>
        /// Returns a random double between min and max, both included
        /// </summary>
        /// <param name="min">The lower bound for the random value</param>
        /// <param name="max">The upper bound for the random value</param>
        public static double RandDouble(double min, double max)
        {
            if (min < max)
                return (min + (max - min) * RandDouble());
            else if (min == max)
                return min;
            else
                return RandDouble(max, min);
        }


        /// <summary>
        /// Returns a random integer between 0 and max. Max is excluded by default
        /// </summary>
        /// <param name="max">The upper bound for the random value</param>
        /// <param name="option">Determines which bounds are included</param>
        /// <returns></returns>
        public static int RandInt(int max, InclusionOptions option = InclusionOptions.Lower)
        {

            return RandInt(0, max, option);
        }

        /// <summary>
        /// Returns a random int between min and max. Both bounds are included by default
        /// </summary>
        public static int RandInt(int min, int max, InclusionOptions option = InclusionOptions.Both)
        {
            if (min < max)
            {
                //Handle int overflow
                if (max == int.MaxValue)
                    if (option == InclusionOptions.Upper)
                        option = InclusionOptions.None;
                    else if (option == InclusionOptions.Both)
                        option = InclusionOptions.Lower;
                if (min == int.MinValue)
                    if (option == InclusionOptions.Lower)
                        option = InclusionOptions.None;
                    else if (option == InclusionOptions.Both)
                        option = InclusionOptions.Upper;

                switch (option)
                {
                    case InclusionOptions.Both:
                        return RandIntInRange(min, max + 1);
                    case InclusionOptions.Lower:
                        return RandIntInRange(min, max);
                    case InclusionOptions.Upper:
                        return RandIntInRange(min + 1, max + 1);
                    case InclusionOptions.None:
                        return RandIntInRange(min + 1, max);
                    default:
                        throw new ArgumentException("Invalid InclusionOption");
                }
            }
            else if (min == max)
                return min;
            else
                return RandInt(max, min, option);
        }

        /// <summary>
        /// Returns a random long between 0 and max. Max is excluded by default
        /// </summary>
        /// <param name="max">The upper bound for the random value</param>
        /// <param name="option">Determines which bounds are included</param>
        /// <returns></returns>
        public static long RandLong(long max, InclusionOptions option = InclusionOptions.Lower)
        {
            return RandLong(0, max, option);
        }

        /// <summary>
        /// Returns a random long between min and max. Both bounds are included by default
        /// </summary>
        public static long RandLong(long min, long max, InclusionOptions option = InclusionOptions.Both)
        {
            if (min < max)
            {
                //Handle long overflow
                if (max == long.MaxValue)
                    if (option == InclusionOptions.Upper)
                        option = InclusionOptions.None;
                    else if (option == InclusionOptions.Both)
                        option = InclusionOptions.Lower;
                if (min == long.MinValue)
                    if (option == InclusionOptions.Lower)
                        option = InclusionOptions.None;
                    else if (option == InclusionOptions.Both)
                        option = InclusionOptions.Upper;

                switch (option)
                {
                    case InclusionOptions.Both:
                        return RandLongInRange(min, max + 1);
                    case InclusionOptions.Lower:
                        return RandLongInRange(min, max);
                    case InclusionOptions.Upper:
                        return RandLongInRange(min + 1, max + 1);
                    case InclusionOptions.None:
                        return RandLongInRange(min + 1, max);
                    default:
                        throw new ArgumentException("Invalid InclusionOption");
                }
            }
            else if (min == max)
                return min;
            else
                return RandLong(max, min, option);
        }

        /// <summary>
        /// Returns true or false
        /// </summary>
        public static bool RandBool()
        {
            return RandDouble() < 0.5F;
        }

        /// <summary>
        /// Returns 1 or -1
        /// </summary>
        public static int RandSign()
        {
            return RandBool() ? 1 : -1;
        }

        #endregion

        #region RandomSelections

        /// <summary>
        /// Returns a random element from the given array.
        /// </summary>
        public static T RandElement<T>(params T[] elements)
        {
            if(elements.Length > 0)
                return elements[RandInt(elements.Length)];
            throw new ArgumentException("Element array cannot be empty");
        }

        /// <summary>
        /// Returns a random element from the given array with the specified probabilities for each element
        /// </summary>
        /// <param name="elements">The selection of elements</param>
        /// <param name="probabilities">The probability for each element</param>
        public static T RandElement<T>(T[] elements, params float[] probabilities)
        {
            if (elements.Length == 0)
                throw new ArgumentException("Element array cannot be empty");
            if (probabilities.Length == 0)
                return RandElement(elements);

            float pSum = probabilities.Sum();

            //Add equal probabilities if the probabilities array is not long enough
            if (probabilities.Length < elements.Length && pSum < 1)
            {
                int missing = elements.Length - probabilities.Length;
                float[] additional = new float[missing];
                for (int i = 0; i < additional.Length; i++)
                    additional[i] = (1 - pSum) / (float)missing;
                float[] allProbs = new float[elements.Length];
                probabilities.CopyTo(allProbs, 0);
                additional.CopyTo(allProbs, probabilities.Length);

                probabilities = allProbs;
            }

            //Correct invalid probabilities
            for (int i = 0; i < probabilities.Length; i++)
                probabilities[i] = MathHelper.Clamp(probabilities[i], 0, 1);

            //Make sure the probabilities add up to 1
            float difference = 1 - pSum;
            //Sum too low ? Add missing probability to last element if possible
            if (!MathHelper.FloatsEqual(difference, 0) && difference > 0)
            {
                for (int i = probabilities.Length - 1; i <= 0 && difference > 0 && !MathHelper.FloatsEqual(difference, 0); i++)
                {
                    float buffer = 1 - probabilities[i];
                    probabilities[i] += Math.Min(buffer, difference);
                    difference -= buffer;
                }
            }
            //Sum too high ? Subtract excess probability from last element if possible
            else if (!MathHelper.FloatsEqual(difference, 0) && difference < 0)
            {
                for (int i = probabilities.Length - 1; i <= 0 && difference < 0 && !MathHelper.FloatsEqual(difference, 0); i++)
                {
                    float buffer = probabilities[i];
                    probabilities[i] += Math.Max(buffer, difference);
                    difference += buffer;
                }
            }

            float r = Rul.RandFloat();
            float f = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                if (probabilities.Length > i)
                {
                    f += probabilities[i];
                    if (r <= f)
                        return elements[i];
                }
            }
            return elements[elements.Length - 1];
        }

        /// <summary>
        /// Returns a random object from the given array of RandObjects
        /// </summary>
        public static T RandElement<T>(RandObject<T>[] objects)
        {
            T[] elements = new T[objects.Length];
            float[] probs = new float[objects.Length];
            for (int i = 0; i < objects.Length; i++)
            {
                elements[i] = objects[i].Element;
                probs[i] = objects[i].Probability;
            }
            return RandElement(elements, probs);
        }


        #endregion

        #endregion

        #region Private

        private static int RandIntInRange(int lower, int upper)
        {
            if (!_initialized)
                Initialize();

            return _rng.Next(lower, upper);            
        }

        private static long RandLongInRange(long lower, long upper)
        {
            if (!_initialized)
                Initialize();

            byte[] randBytes = new byte[8];
            _rng.NextBytes(randBytes);
            long r = BitConverter.ToInt64(randBytes, 0);
            return Math.Abs(r % (upper - lower)) + lower;
        }

        #endregion
    }
}
