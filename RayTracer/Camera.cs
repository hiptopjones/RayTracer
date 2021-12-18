using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Camera
    {
        public Point3 Origin { get; set; }
        public Point3 LowerLeftCorner { get; set; }
        public Vector3 HorizontalOffset { get; set; }
        public Vector3 VerticalOffset { get; set; }

        public Ray GetRay(double u, double v)
        {
            return new Ray
            {
                Origin = Origin,
                Direction = Vector3.Normalize(LowerLeftCorner + u * HorizontalOffset + v * VerticalOffset - Origin)
            };
        }
    }
}
