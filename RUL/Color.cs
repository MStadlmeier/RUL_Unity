using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUL
{
    public struct Col
    {
        #region Private Fields

        private int _r, _g, _b, _a;

        #endregion

        #region Public Fields

        public int R
        {
            get { return _r; }
            set
            {
                ValidateParameter(value);
                _r = value;
            }
        }

        public int G
        {
            get { return _g; }
            set
            {
                ValidateParameter(value);
                _g = value;
            }
        }

        public int B
        {
            get { return _b; }
            set
            {
                ValidateParameter(value);
                _b = value;
            }
        }

        public int A
        {
            get { return _a; }
            set
            {
                ValidateParameter(value);
                _a = value;
            }
        }

        #endregion

        #region Constructors

        public Col(int r, int g, int b)
        {
            _r = (byte)r;
            _g = (byte)g;
            _b = (byte)b;
            _a = 255;
        }

        public Col(int r, int g, int b, int a)
        {
            _r = (byte)r;
            _g = (byte)g;
            _b = (byte)b;
            _a = (byte)a;
        }

        public Col(int greyScale)
        {
            _r = (byte)greyScale;
            _g = (byte)greyScale;
            _b = (byte)greyScale;
            _a = 255;
        }

        public Col(int greyScale, int a)
        {
            _r = (byte)greyScale;
            _g = (byte)greyScale;
            _b = (byte)greyScale;
            _a = (int)a;
        }

        #endregion

        #region Private Methods

        private void ValidateParameter(int value)
        {
            if (value <= 0 || value >= 255)
                throw new ArgumentException("Value must be between 0 and 255");
        }

        #endregion
    }
}
