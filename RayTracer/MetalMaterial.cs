using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class MetalMaterial : Material
    {
        public Color Albedo { get; set; }
        public double Fuzz { get; set; }

        public override bool Scatter(Ray originalRay, Hit hit, out Color attentuationColor, out Ray scatteredRay)
        {
            Vector3 reflectedDirection = Reflect(originalRay.Direction, hit.Normal);
            scatteredRay = new Ray(hit.Position, reflectedDirection + Fuzz * RandomUtilities.GetVectorInUnitSphere());
            attentuationColor = Albedo;
            return Vector3.DotProduct(scatteredRay.Direction, hit.Normal) > 0;
        }

        private Vector3 Reflect(Vector3 v, Vector3 n)
        {
            return v - 2 * Vector3.DotProduct(v, n) * n;
        }
    }
}
