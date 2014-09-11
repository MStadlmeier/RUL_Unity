using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RUL.Generators
{
    public class PerlinGenerator
    {
        #region Private

        float _persistence, _frequency, _amplitude, _highestValue;
        float[,] _baseNoise;
        int _octaveCount, _width, _height;

        #endregion

        #region Constructors

        public PerlinGenerator(int width, int height, float frequency, float persistence, int octaveCount, float amplitude)
        {
            this._persistence = persistence;
            this._octaveCount = octaveCount;
            //Less awkward frequencies for user
            this._frequency = frequency / 100F;
            this._amplitude = amplitude;
            this._width = width;
            this._height = height;

            _baseNoise = RulNoise.RandNoise2(width, height);
        }

        /// <summary>
        /// Returns a 2D perlin noise array
        /// </summary>
        /// <returns></returns>
        public float[,] GetPerlinNoise()
        {
            _highestValue = PerlinNoiseAt(0, 0, true);
            float[,] noise = new float[_width, _height];
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    float value = PerlinNoiseAt(x, y);
                    noise[x, y] = value;
                }
            }
            return noise;
        }

        /// <summary>
        /// Returns a single Perlin noise sample at the specified coordinates
        /// At the moment you will still need to specifiy the maximum size for the noise in the constructor,
        /// even if you only need a single sample
        /// </summary>
        public float PerlinNoiseAt(int x, int y)
        {
            return PerlinNoiseAt(x, y, false);
        }

        #endregion

        #region Private Methods

        private float SmoothNoiseAt(float x, float y)
        {
            long intX = (long)x;
            long intY = (long)y;
            float fracX = x - intX;
            float fracY = y - intY;
            //Get values from base noise
            int x1 = (int)((intX + _width) % _width);
            int y1 = (int)((intY + _height) % _height);
            int x2 = (int)((x1 + _width - 1) % _width);
            int y2 = (int)((y1 + _height - 1) % _height);

            float value = 0;
            //Bilinear interpolation
            value += fracX * fracY * _baseNoise[x1, y1];
            value += (1 - fracX) * fracY * _baseNoise[x2, y1];
            value += fracX * (1 - fracY) * _baseNoise[x1, y2];
            value += (1 - fracX) * (1 - fracY) * _baseNoise[x2, y2];

            return value;
        }

        private float PerlinNoiseAt(int x, int y, bool testHighest)
        {
            float value = 0;
            float amplitude = _amplitude;
            float frequency = _frequency;
            for (int i = 0; i < _octaveCount; i++)
            {
                float signal = testHighest ? 1F : SmoothNoiseAt(x * frequency, y * frequency);
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
