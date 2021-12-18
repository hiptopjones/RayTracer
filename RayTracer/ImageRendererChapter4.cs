using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    // Ray Tracing In One Weekend, Chapter 4
    class ImageRendererChapter4
    {
        // Returns the color of the background (a simple gradient).
        public Color GetRayColor(Ray ray)
        {
            Vector3 rayDirection = ray.Direction.Normalized;
            double percent = 0.5 * (rayDirection.Y + 1);

            Color startColor = new Color(1, 1, 1);
            Color endColor = new Color(0.5, 0.7, 1.0);

            Color currentColor = Color.Lerp(startColor, endColor, percent);

            return currentColor;
        }

        public Color[] GenerateImage(int imageWidth, int imageHeight)
        {
            // Image
            double aspectRatio = (double)imageWidth / imageHeight;

            // Camera
            double viewportHeight = 2d;
            double viewportWidth = aspectRatio * viewportHeight;
            double focalLength = 1d;

            Point3 origin = new Point3(0, 0, 0);
            Vector3 horizontalOffset = new Vector3(viewportWidth, 0, 0);
            Vector3 verticalOffset = new Vector3(0, viewportHeight, 0);
            Vector3 focalLengthOffset = new Vector3(0, 0, focalLength);
            Point3 lowerLeftCorner = origin - horizontalOffset / 2 - verticalOffset / 2 - focalLengthOffset;

            // Render
            Color[] pixels = new Color[imageWidth * imageHeight];

            for (int y = imageHeight - 1; y >= 0; y--)
            {
                for (int x = 0; x < imageWidth; x++)
                {
                    double u = (double)x / (imageWidth - 1);
                    double v = (double)y / (imageHeight - 1);

                    Ray ray = new Ray(origin, lowerLeftCorner + u * horizontalOffset + v * verticalOffset - origin);
                    Color color = GetRayColor(ray);

                    pixels[y * imageWidth + x] = color;
                }
            }

            return pixels;
        }
    }
}
