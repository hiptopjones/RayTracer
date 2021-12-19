using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Utilities
    {
        public static double Lerp(double start, double end, double percent)
        {
            return start + (end - start) * percent;
        }
    }
}
