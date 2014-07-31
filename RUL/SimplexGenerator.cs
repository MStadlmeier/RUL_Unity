using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUL.Generators
{
    public class SimplexGenerator
    {
        #region Private Fields

        private float _persistence, _frequency, _amplitude, _highestValue;
        private int _octaveCount;
        private SimplexOctave[] _octaves;

        #endregion

        #region Constructor

        public SimplexGenerator(float frequency, float persistence, int octaveCount, float amplitude)
        {
            this._persistence = persistence;
            //Less awkward frequencies for user
            this._frequency = frequency / 100F;
            this._octaveCount = octaveCount;
            this._amplitude = amplitude;
            _octaves = new SimplexOctave[octaveCount];
            for (int i = 0; i < octaveCount; i++)
                _octaves[i] = new SimplexOctave(Rul.RandInt());
            _highestValue = SimplexNoiseAt(0, 0, true);
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Returns 2D simplex noise for a specified area
        /// </summary>
        public float[,] GetSimplexNoise(int width, int height, int startX, int startY)
        {
            float[,] noise = new float[width, height];
            for (int x = startX; x < width; x++)
                for (int y = startY; y < height; y++)
                    noise[x, y] = SimplexNoiseAt(x, y);
            return noise;
        }

        /// <summary>
        /// Returns 3D simplex noise for a specified area
        /// </summary>
        public float[, ,] GetSimplexNoise(int width, int height, int depth, int startX, int startY, int startZ)
        {
            float[, ,] noise = new float[width, height, depth];
            for (int x = startX; x < width; x++)
                for (int y = startY; y < height; y++)
                    for (int z = startZ; z < depth; z++)
                        noise[x, y, z] = SimplexNoiseAt(x, y, z);
            return noise;
        }

        /// <summary>
        /// Returns a noise sample at the specified 2D coordinates
        /// </summary>
        public float SimplexNoiseAt(float x, float y, bool testHighest = false)
        {
            float value = 0;
            float amplitude = _amplitude;
            float frequency = _frequency;
            for (int i = 0; i < _octaveCount; i++)
            {
                float signal = testHighest ? 1F : (float)_octaves[i].SimplexNoiseAt(x * frequency, y * frequency);
                value += signal * amplitude;
                frequency *= 2;
                amplitude *= _persistence;
            }
            if (testHighest)
                return value;
            return MathHelper.NormalizeValue(value, 0, _highestValue, 0, _amplitude);
        }

        /// <summary>
        /// Returns a noise sample at the specified 3D coordinates
        /// </summary>
        public float SimplexNoiseAt(float x, float y, float z, bool testHighest = false)
        {
            float value = 0;
            float amplitude = _amplitude;
            float frequency = _frequency;
            for (int i = 0; i < _octaveCount; i++)
            {
                float signal = testHighest ? 1F : (float)_octaves[i].SimplexNoiseAt(x * frequency, y * frequency, z * frequency);
                value += signal * amplitude;
                frequency *= 2;
                amplitude *= _persistence;
            }
            if (testHighest)
                return value;
            return MathHelper.NormalizeValue(value, 0, _highestValue, 0, _amplitude);
        }

        #endregion
    }
}
