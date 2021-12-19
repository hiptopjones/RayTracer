using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class RandomUtilities
    {
        private static Random Random { get; } = new Random();

        public static Vector3 CreateRandomVector3(double min, double max)
        {
            return new Vector3
            {
                X = Utilities.Lerp(min, max, Random.NextDouble()),
                Y = Utilities.Lerp(min, max, Random.NextDouble()),
                Z = Utilities.Lerp(min, max, Random.NextDouble())
            };
        }

        public static Vector3 GetVectorInUnitSphere()
        {
            while (true)
            {
                Vector3 v = CreateRandomVector3(-1, 1);
                if (v.MagnitudeSquared >= 1)
                {
                    continue;
                }

                return v;
            }
        }

        public static Point3 GetPointInUnitSphere()
        {
            return Point3.Origin + GetVectorInUnitSphere();
        }

        public static Point3 GetPointOnUnitSphere()
        {
            return Point3.Origin + GetVectorInUnitSphere().Normalized;
        }

        public static Point3 GetPointInHemisphere(Vector3 normal)
        {
            Vector3 v = GetVectorInUnitSphere();
            if (Vector3.DotProduct(v, normal) < 0)
            {
                v = -v;
            }

            return Point3.Origin + v;
        }
    }
}
