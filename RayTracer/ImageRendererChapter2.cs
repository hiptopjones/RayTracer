using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    // Ray Tracing In One Weekend, Chapter 2
    class ImageRendererChapter2
    {
        public Color[] GenerateImage(int imageWidth, int imageHeight)
        {
            Color[] pixels = new Color[imageWidth * imageHeight];

            for (int y = imageHeight - 1; y >= 0; y--)
            {
                for (int x = 0; x < imageWidth; x++)
                {
                    Color color = new Color
                    {
                        R = (double)x / (imageWidth - 1),
                        G = (double)y / (imageHeight - 1),
                        B = 0.25
                    };

                    pixels[y * imageWidth + x] = color;
                }
            }

            return pixels;
        }
    }
}
