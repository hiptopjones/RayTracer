using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    // Ray Tracing In One Weekend, Chapter 5
    class ImageRendererChapter5
    {
        bool IsHitSphere(Point3 sphereCenter, double sphereRadius, Ray ray)
        {
            Vector3 rayOriginToSphereCenter = ray.Origin - sphereCenter;

            // Coefficients for quadratic form: at^2 + bt + c
            double a = Vector3.DotProduct(ray.Direction, ray.Direction);
            double b = 2 * Vector3.DotProduct(rayOriginToSphereCenter, ray.Direction);
            double c = Vector3.DotProduct(rayOriginToSphereCenter, rayOriginToSphereCenter) - sphereRadius * sphereRadius;

            // The discriminant is the part of the quadratic formula underneath the square root symbol
            // It tells us how many solutions there are
            //  - Positive means 2 solutions (ray intersects the sphere)
            //  - Negative means no solutions (ray misses the sphere)
            //  - Zero means one solution (ray tangent to the sphere)
            double discriminant = (b * b) - (4 * a * c);

            // We are only interested in intersection
            return discriminant > 0;
        }

        // Calculate the color for each ray based on what it hits
        Color GetRayColor(Ray ray)
        {
            Point3 sphereCenter = new Point3(0, 0, -1);
            if (IsHitSphere(sphereCenter, 0.5, ray))
            {
                return Color.Red;
            }

            Vector3 rayDirection = ray.Direction;
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

                    Vector3 rayDirection = (lowerLeftCorner + u * horizontalOffset + v * verticalOffset - origin).Normalized;
                    Ray ray = new Ray(origin, rayDirection);
                    Color color = GetRayColor(ray);

                    pixels[y * imageWidth + x] = color;
                }
            }

            return pixels;
        }
    }
}
