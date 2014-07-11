using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace RUL
{
    public static class RulVec
    {
        #region Public Methods

        #region 2D

        /// <summary>
        /// Returns a random 2-dimensional vector within the specified range
        /// </summary>
        /// <param name="lowerBoundX">Lower bound for the x-component</param>
        /// <param name="upperBoundX">Upper bound for the x-component</param>
        /// <param name="lowerBoundY">Lower bound for the y-component</param>
        /// <param name="upperBoundY">Upper bound for the y-component</param>
        public static Vector2 RandVector2(float lowerBoundX, float lowerBoundY, float upperBoundX, float upperBoundY)
        {
            return new Vector2(Rul.RandFloat(lowerBoundX, upperBoundX), Rul.RandFloat(lowerBoundY, upperBoundY));
        }

        /// <summary>
        /// Returns a random 2-dimensional vector that lies between the given end points
        /// </summary>
        public static Vector2 RandVector2(Vector2 pointA, Vector2 pointB)
        {
            if (pointA == pointB)
                return pointA;
            Vector2 difference = pointB - pointA;
            return pointA + difference * Rul.RandFloat();

        }

        /// <summary>
        /// Returns a randomly rotated version of the given base vector
        /// </summary>
        /// <param name="baseVector">The vector that is used as a base for the new one</param>
        /// <param name="maxAngle">The greatest possible angle(in radians) between the base vector and the rotated random vector</param>
        public static Vector2 RandVector2(Vector2 baseVector, float maxAngle)
        {
            float angle = Rul.RandFloat(maxAngle % (float)(2F * Math.PI)) * Rul.RandSign();
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            float newX = (float)(baseVector.x * cos - baseVector.y * sin);
            float newY = (float)(baseVector.x * sin + baseVector.y * cos);
            return new Vector2(newX, newY);
        }

        /// <summary>
        /// Returns a random 2-dimensional vector with the length 1
        /// </summary>
        public static Vector2 RandUnitVector2()
        {
            float rad = Rul.RandFloat((float)Math.PI * 2);
            return new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));
        }

        /// <summary>
        /// Returns a random 2-dimensional vector that lies in the circle with the specified radius
        /// </summary>
        /// <param name="circleRadius">The radius of the circle that contains the point represented by the random vector</param>
        public static Vector2 RandVecInCircle(float circleRadius)
        {
            return RandUnitVector2() * Rul.RandFloat(circleRadius);
        }

        /// <summary>
        /// Returns a random 2-dimensional vector that points up, down, left or right
        /// </summary>
        public static Vector2 RandDirection2()
        {
            return Rul.RandElement(new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1));
        }

        #endregion

        #region 3D

        /// <summary>
        /// Returns a random 3-dimensional vector within the specified range
        /// </summary>
        /// <param name="lowerBoundX">Lower bound for the x-component</param>
        /// <param name="upperBoundX">Upper bound for the x-component</param>
        /// <param name="lowerBoundY">Lower bound for the y-component</param>
        /// <param name="upperBoundY">Upper bound for the y-component</param>
        /// <param name="lowerBoundZ">Lower bound for the z-component</param>
        /// <param name="upperBoundZ">Upper bound for the z-component</param>
        public static Vector3 RandVector3(float lowerBoundX, float lowerBoundY, float lowerBoundZ, float upperBoundX, float upperBoundY, float upperBoundZ)
        {
            return new Vector3(Rul.RandFloat(lowerBoundX, upperBoundX), Rul.RandFloat(lowerBoundY, upperBoundY), Rul.RandFloat(lowerBoundZ, upperBoundZ));
        }

        /// <summary>
        /// Returns a random 3-dimensional vector that lies between the given end points
        /// </summary>
        public static Vector3 RandVector3(Vector3 pointA, Vector3 pointB)
        {
            if (pointA == pointB)
                return pointA;
            Vector3 difference = pointB - pointA;
            return pointA + difference * Rul.RandFloat();

        }

        /// <summary>
        /// Returns a random 3-dimensional vector with the length 1
        /// </summary>
        public static Vector3 RandUnitVector3()
        {
            float theta = Rul.RandFloat((float)Math.PI * 2);
            float z = Rul.RandFloat(-1, 1);
            float c = (float)(Math.Sqrt(1-z * z));
            float x = (float)(c * Math.Cos(theta));
            float y = (float)(c * Math.Sin(theta));
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Returns a random 3-dimensional vector that lies in the sphere with the specified radius
        /// </summary>
        /// <param name="sphereRadius">The radius of the sphere that contains the point represented by the random vector</param>
        public static Vector3 RandVecInSphere(float sphereRadius)
        {
            return RandUnitVector3() * Rul.RandFloat() * sphereRadius;
        }

        /// <summary>
        /// Returns a random 3-dimensional vector that points left, right, up, down, forwards or backwards
        /// </summary>
        public static Vector3 RandDirection3()
        {
            return Rul.RandElement(new Vector3(-1, 0, 0), new Vector3(1, 0, 0), new Vector3(0, -1, 0), new Vector3(0, 1, 0), new Vector3(0, 0, -1), new Vector3(0, 0, 1));
        }

        #endregion

        #endregion
    }
}