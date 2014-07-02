using System;

namespace RUL
{
    public class RandObject<T>
    {
        public T Element;
        public float Probability
        {
            get { return _probability; }
            set
            {
                _probability = MathHelper.Clamp(value, 0, 1);
            }
        }

        private float _probability;

        public RandObject(T element, float prob)
        {
            this.Element = element;
            this.Probability = prob;
        }
    }
}
