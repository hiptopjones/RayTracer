using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Hit
    {
        public Point3 Position { get; set; }
        public Vector3 Normal { get; set; }
        public double Distance { get; set; }
        public bool IsFrontFace { get; set; }

        // Normal is adjusted to always point against the ray, which means
        // we need to track separately whether it was front hit or a back hit
        public void SetFaceNormal(Ray ray, Vector3 outwardNormal)
        {
            IsFrontFace = (Vector3.DotProduct(ray.Direction, outwardNormal) < 0);
            Normal = IsFrontFace ? outwardNormal : -outwardNormal;
        }
    }

    interface IHittable
    {
        bool Hit(Ray ray, double minDistance, double maxDistance, out Hit hit);
    }

    class HittableList : List<IHittable>
    {
        public bool Hit(Ray ray, double minDistance, double maxDistance, out Hit hit)
        {
            hit = null;

            bool isHitAnything = false;
            double closestDistance = maxDistance;

            foreach (IHittable hittable in this)
            {
                if (hittable.Hit(ray, minDistance, closestDistance, out Hit tempHit))
                {
                    isHitAnything = true;
                    closestDistance = tempHit.Distance;
                    hit = tempHit;
                }
            }

            return isHitAnything;
        }
    }
}
