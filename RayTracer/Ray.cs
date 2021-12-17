using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Ray
    {
        public Point3 Origin { get; set; }
        public Vector3 Direction { get; set; }

        public Ray(Point3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Point3 GetPosition(double t)
        {
            return Origin + Direction * t;
        }

        public override string ToString()
        {
            return $"R[ {Origin}, {Direction} ]";
        }
    }
}
