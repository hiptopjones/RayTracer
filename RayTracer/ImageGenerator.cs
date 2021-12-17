using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class ImageGenerator
    {
        // Ray Tracing In One Weekend, Chapter 2
        public Color[] GenerateGradientImage(int width, int height)
        {
            Color[] pixels = new Color[width * height];

            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    double r = x / (width - 1d);
                    double g = y / (height - 1d);
                    double b = 0.25;

                    Color pixel = new Color
                    {
                        R = (int)(r * 255.999),
                        G = (int)(g * 255.999),
                        B = (int)(b * 255.999)
                    };

                    pixels[y * height + x] = pixel;
                }
            }

            return pixels;
        }
    }
}
