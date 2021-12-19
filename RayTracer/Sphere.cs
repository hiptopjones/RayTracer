using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Sphere : IHittable
    {
        public Point3 Center { get; set; }
        public double Radius { get; set; }
        public Material Material { get; set; }

        public Sphere()
        {

        }

        public Sphere(Point3 center, double radius)
            : this(center, radius, null)
        {
        }

        public Sphere(Point3 center, double radius, Material material)
        {
            Center = center;
            Radius = radius;
            Material = material;
        }

        public bool Hit(Ray ray, double minDistance, double maxDistance, out Hit hit)
        {
            // Initialize out parameter (makes early returns simpler)
            hit = null;

            Vector3 rayOriginToSphereCenter = ray.Origin - Center;

            // Coefficients for quadratic form: at^2 + bt + c
            // NOTE: The magnitude squared is equivalent to dotting a vector with itself
            // NOTE: Text does some additional term reduction with I omit for clarity
            double a = ray.Direction.MagnitudeSquared;
            double b = 2 * Vector3.DotProduct(rayOriginToSphereCenter, ray.Direction);
            double c = rayOriginToSphereCenter.MagnitudeSquared - Radius * Radius;

            // The discriminant is the part of the quadratic formula underneath the square root symbol
            // It tells us how many solutions there are
            //  - Positive means 2 solutions (ray intersects the sphere)
            //  - Negative means no solutions (ray misses the sphere)
            //  - Zero means one solution (ray tangent to the sphere)
            double discriminant = (b * b) - (4 * a * c);

            // Not interested in misses
            if (discriminant < 0)
            {
                return false;
            }

            // Find the nearest root in the range
            double distance = (-b - Math.Sqrt(discriminant)) / 2 * a;
            if (distance < minDistance || distance > maxDistance)
            {
                distance = (-b + Math.Sqrt(discriminant)) / 2 * a;
                if (distance < minDistance || distance > maxDistance)
                {
                    return false;
                }
            }

            hit = new Hit();
            hit.Distance = distance;
            hit.Position = ray.GetPosition(distance);
            hit.Material = Material;
            hit.SetFaceNormal(ray, Vector3.Normalize(hit.Position - Center));

            return true;

        }
    }
}
