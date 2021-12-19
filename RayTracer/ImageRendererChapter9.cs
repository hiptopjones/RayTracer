using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    // Ray Tracing In One Weekend, Chapter 9
    class ImageRendererChapter9
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private Random Random { get; } = new Random();

        // Calculate the color for each ray based on what it hits
        private Color GetRayColor(Ray ray, HittableList world, int maxBounceDepth)
        {
            if (maxBounceDepth <= 0)
            {
                return Color.Black;
            }

            if (world.Hit(ray, 0.001, double.MaxValue, out Hit hit))
            {
                if (hit.Material != null)
                {
                    if (hit.Material.Scatter(ray, hit, out Color attenuationColor, out Ray scatteredRay))
                    {
                        return attenuationColor * GetRayColor(scatteredRay, world, maxBounceDepth - 1);
                    }
                }

                return Color.Black;
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
            int samplesPerPixel = 100;
            int maxBounceDepth = 50;

            // World

            Material groundMaterial = new LambertianMaterial
            {
                Albedo = new Color(0.8, 0.8, 0.0)
            };
            Material centerMaterial = new LambertianMaterial
            {
                Albedo = new Color(0.7, 0.3, 0.3)
            };
            Material leftMaterial = new MetalMaterial
            {
                Albedo = new Color(0.8, 0.8, 0.8),
                Fuzz = 0
            };
            Material rightMaterial = new MetalMaterial
            {
                Albedo = new Color(0.8, 0.6, 0.2),
                Fuzz = 0
            };

            HittableList world = new HittableList
            {
                new Sphere(new Point3(0, -100.5, -1), 100, groundMaterial),
                new Sphere(new Point3(0, 0, -1), 0.5, centerMaterial),
                new Sphere(new Point3(-1, 0, -1), 0.5, leftMaterial),
                new Sphere(new Point3(1, 0, -1), 0.5, rightMaterial),

            };

            // Camera
            double viewportHeight = 2d;
            double viewportWidth = aspectRatio * viewportHeight;
            double focalLength = 1d;
            Vector3 focalLengthOffset = new Vector3(0, 0, focalLength);

            Camera camera = new Camera
            {
                Origin = new Point3(0, 0, 0),
                HorizontalOffset = new Vector3(viewportWidth, 0, 0),
                VerticalOffset = new Vector3(0, viewportHeight, 0)
            };
            camera.LowerLeftCorner = camera.Origin - camera.HorizontalOffset / 2 - camera.VerticalOffset / 2 - focalLengthOffset;

            // Render
            Color[] pixels = new Color[imageWidth * imageHeight];

            for (int y = imageHeight - 1; y >= 0; y--)
            {
                Logger.Info($"Scanlines remaining: {y}");

                for (int x = 0; x < imageWidth; x++)
                {
                    Color color = Color.Black;

                    for (int i = 0; i < samplesPerPixel; i++)
                    {
                        double u = (x + Random.NextDouble()) / (imageWidth - 1);
                        double v = (y + Random.NextDouble()) / (imageHeight - 1);

                        Ray ray = camera.GetRay(u, v);
                        color += GetRayColor(ray, world, maxBounceDepth);
                    }

                    color /= samplesPerPixel;
                    pixels[y * imageWidth + x] = new Color
                    {
                        // Gamma-correct for gamma=2.0
                        R = Math.Sqrt(color.R),
                        G = Math.Sqrt(color.G),
                        B = Math.Sqrt(color.B)
                    };
                }
            }

            Logger.Info($"Done!");
            return pixels;
        }
    }
}
