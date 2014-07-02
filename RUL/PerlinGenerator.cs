using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RUL.Noise;

namespace RUL
{
    internal class PerlinGenerator
    {
        #region Private

        float _persistence, _frequency, _amplitude, _highestValue;
        float[,] _baseNoise;
        int _octaveCount, _width, _height;

        #endregion

        #region Constructors

        public PerlinGenerator(int width, int height, float persistence, float frequency, int octaveCount, float amplitude)
        {
            this._persistence = persistence;
            this._octaveCount = octaveCount;
            //Less awkward frequencies for user
            this._frequency = frequency / 100F;
            this._amplitude = amplitude;
            this._width = width;
            this._height = height;

            _baseNoise = RulNoise.RandNoise2(width,height);
        }

        public float[,] GetPerlinNoise()
        {
            _highestValue = PerlinNoiseAt(0, 0,true);
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

        private float NormalizeValue(float val, float currentMin, float currentMax, float newMin, float newMax)
        {
            if (currentMax - currentMin != 0)
                return (val - currentMin) / (currentMax - currentMin) * (newMax - newMin) + newMin;
            return 0;
        }

        private float PerlinNoiseAt(int x, int y, bool testHighest = false)
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
            return NormalizeValue(value, 0, _highestValue, 0, _amplitude);
        }

        #endregion
    }
}
