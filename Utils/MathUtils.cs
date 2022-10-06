using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketAI.Utils
{
    /// <summary>
    /// Maths related utility functions.
    /// </summary>
    internal static class MathUtils
    {
        /// <summary>
        /// Determine a point rotated by an angle around an origin.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="origin"></param>
        /// <param name="angleInDegrees"></param>
        /// <returns></returns>
        internal static PointF RotatePointAboutOrigin(PointF point, PointF origin, double angleInDegrees)
        {
            return RotatePointAboutOriginInRadians(point, origin, DegreesInRadians(angleInDegrees));
        }

        /// <summary>
        /// Determine a point rotated by an angle around an origin.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="origin"></param>
        /// <param name="angleInRadians"></param>
        /// <returns></returns>
        internal static PointF RotatePointAboutOriginInRadians(PointF point, PointF origin, double angleInRadians)
        {
            double cos = Math.Cos(angleInRadians);
            double sin = Math.Sin(angleInRadians);
            float dx = point.X - origin.X;
            float dy = point.Y - origin.Y;

            // standard maths for rotation.
            return new PointF((float)(cos * dx - sin * dy + origin.X),
                              (float)(sin * dx + cos * dy + origin.Y)
            );
        }

        /// <summary>
        /// Logic requires radians but we track angles in degrees, this converts.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        internal static double DegreesInRadians(double angle)
        {
            return (double)Math.PI * angle / 180;
        }

        /// <summary>
        /// Returns a value between min and max (never outside of).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0)
            {
                return min;
            }

            if (val.CompareTo(max) > 0)
            {
                return max;
            }

            return val;
        }

    }
}
