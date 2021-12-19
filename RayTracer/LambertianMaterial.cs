using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class LambertianMaterial : Material
    {
        public Color Albedo { get; set; }

        public override bool Scatter(Ray originalRay, Hit hit, out Color attentuation, out Ray scatteredRay)
        {
            Vector3 scatterDirection = Vector3.Normalize(hit.Normal + RandomUtilities.GetVectorInUnitSphere().Normalized);

            // Catch degenerate scatter direction
            if (scatterDirection.IsNearZero())
            {
                scatterDirection = hit.Normal;
            }

            scatteredRay = new Ray(hit.Position, scatterDirection);
            attentuation = Albedo;
            return true;
        }
    }
}
