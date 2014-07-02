using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUL
{
    public struct Vec3
    {
        public static Vec3 Zero { get { return new Vec3(); } }

        #region Public Fields

        public float X, Y, Z;

        #endregion

        #region Contructors

        public Vec3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vec3(float c)
        {
            this.X = c;
            this.Y = c;
            this.Z = c;
        }

        #endregion

        #region Operators

        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vec3 operator -(Vec3 value)
        {
            return new Vec3(-value.X, -value.Y, -value.Z);
        }

        public static bool operator ==(Vec3 a, Vec3 b)
        {
            return (a.X == b.X && a.Y == b.Y && a.Z == b.Z);
        }

        public static bool operator !=(Vec3 a, Vec3 b)
        {
            return !(a == b);
        }

        public static Vec3 operator *(Vec3 a, float scalar)
        {
            return new Vec3(a.X * scalar, a.Y * scalar, a.Z * scalar);
        }

        public static Vec3 operator /(Vec3 a, float divider)
        {
            return new Vec3(a.X / divider, a.Y / divider, a.Z / divider);
        }


        #endregion

        #region Public Methods

        public float Dot(Vec3 other)
        {
            return (this.X * other.X + this.Y * other.Y + this.Z * other.Z);
        }

        public override string ToString()
        {
            return String.Format("X: {0} Y: {1} Z: {2}", X, Y, Z);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vec3)
            {
                Vec3 vec = (Vec3)obj;
                return (this.X == vec.X && this.Y == vec.Y && this.Z == vec.Z);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
        }

        #endregion
    }
}
