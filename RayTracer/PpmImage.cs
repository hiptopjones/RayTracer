using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    // https://en.wikipedia.org/wiki/Netpbm#PPM_example
    //P3           # "P3" means this is a RGB color image in ASCII
    //3 2          # "3 2" is the width and height of the image in pixels
    //255          # "255" is the maximum value for each color
    //# The part above is the header
    //# The part below is the image data: RGB triplets
    //255   0   0  # red
    //  0 255   0  # green
    //  0   0 255  # blue
    //255 255   0  # yellow
    //255 255 255  # white
    //  0   0   0  # black
    public class PpmImage
    {
        public static string GenerateImage(int width, int height)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"P3");
            builder.AppendLine($"{width} {height}");
            builder.AppendLine($"255");

            for (int y = height - 1; y >= 0;  y--)
            {
                for (int x = 0; x < width; x++)
                {
                    double r = x / (width - 1d);
                    double g = y / (height - 1d);
                    double b = 0.25;

                    int ir = (int)(r * 255.999);
                    int ig = (int)(g * 255.999);
                    int ib = (int)(b * 255.999);
                    builder.AppendLine($"{ir,4} {ig,4} {ib,4}");
                }
            }

            return builder.ToString();
        }
    }
}
