using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    abstract class Material
    {
        public abstract bool Scatter(Ray originalRay, Hit hit, out Color attentuationColor, out Ray scatteredRay);
    }
}
