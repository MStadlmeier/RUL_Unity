using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUL
{
    public static class RulNoise
    {
        #region Private

        private const int MAX_PERLIN_SIZE = 4000;
        private const int MAX_OCTAVE_COUNT = 32;
        private const float DEFAULT_FREQUENCY = 1;
        private const float DEFAULT_PERSISTENCE = 0.5F;
        private const float DEFAULT_AMPLITUDE = 1;
        private const int DEFAULT_OCTAVE_COUNT = 6;        

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a random, one-dimensional array of floats between 0 and 1
        /// </summary>
        public static float[] RandNoise1(int length)
        {
            ValidateSizeParameters(new object[] { length });

            float[] noise = new float[length];
            for (int i = 0; i < length; i++)
                noise[i] = Rul.RandFloat();
            return noise;
        }

        /// <summary>
        /// Returns a random, two-dimensional array of floats between 0 and 1
        /// </summary>
        public static float[,] RandNoise2(int width, int height)
        {
            ValidateSizeParameters(new object[] { width, height });

            float[,] noise = new float[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    noise[x, y] = Rul.RandFloat();
            return noise;
        }

        /// <summary>
        /// Returns a random, three-dimensional array of floats between 0 and 1
        /// </summary>
        public static float[, ,] RandNoise3(int width, int height, int depth)
        {
            ValidateSizeParameters(new object[] { width, height, depth });

            float[, ,] noise = new float[width, height, depth];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    for (int z = 0; z < depth; z++)
                        noise[x, y, z] = Rul.RandFloat();
            return noise;
        }

        /// <summary>
        /// Returns 2 dimensional perlin noise of the specified size and with default parameters
        /// </summary>
        public static float[,] RandPerlinNoise2(int width, int height)
        {
            return RandPerlinNoise2(width, height, DEFAULT_FREQUENCY, DEFAULT_PERSISTENCE, DEFAULT_OCTAVE_COUNT, DEFAULT_AMPLITUDE);
        }

        /// <summary>
        /// Returns 2 dimensional perlin noise of the specified size and with the given parameters. See documentation for more information
        /// on parameters and recommended values.
        /// </summary>
        /// <param name="width">The width of the noise</param>
        /// <param name="height">The height of the noise</param>
        /// <param name="frequency">The frequency of the noise function.A high frequency will make the noise look more random
        /// while a low frequency will make the noise look more flat and bland. 
        /// Default : 1</param>
        /// <param name="persistence">The rate at which the frequency decreases for each octave. Increasing this value will make the noise
        /// rougher, while decreasing it will make it smoother.
        /// Default : 0.5 , Min : 0</param>
        /// <param name="octaveCount">The octave count determines how many noise functions are combined. Increasing this value will make
        /// the noise look more fractal, decreasing it will make the noise look more simple and stylized.
        /// Increasing this value will significantly increase computation time.
        /// Default : 6 , Min : 1 , Max : 32</param>
        /// <param name="amplitude">The highest value any point in the noise can have. Default : 1</param>
        public static float[,] RandPerlinNoise2(int width, int height, float frequency, float persistence, int octaveCount, float amplitude)
        {
            if (width <= 0 || width > MAX_PERLIN_SIZE || height <= 0 || height > MAX_PERLIN_SIZE)
                throw new ArgumentException(string.Format("Noise width and height must be between 1 and {0}", MAX_PERLIN_SIZE));
            if (frequency < 0)
                throw new ArgumentException("Frequency must be greater than zero");
            if (persistence < 0)
                throw new ArgumentException("Persistence must be greater than zero");
            if(octaveCount <= 0 || octaveCount > MAX_OCTAVE_COUNT)
                throw new ArgumentException(string.Format("Octave count must be between 1 and {0}", MAX_OCTAVE_COUNT));
            if(amplitude < 0)
                throw new ArgumentException("Amplitude can't be less than zero");

            PerlinGenerator generator = new PerlinGenerator(width, height, persistence, frequency, octaveCount, amplitude);
            return generator.GetPerlinNoise();
                
        }

        #endregion

        #region Private

        private static void ValidateSizeParameters(object[] parameters)
        {
            foreach (object p in parameters)
            {
                if ((int)p < 0)
                    throw new ArgumentException("Size cannot be less than zero");
            }
        }

        #endregion
    }
}
