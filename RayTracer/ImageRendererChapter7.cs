using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    // Ray Tracing In One Weekend, Chapter 6
    class ImageRendererChapter7
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private Random Random { get; } = new Random();

        // Calculate the color for each ray based on what it hits
        Color GetRayColor(Ray ray, HittableList world)
        {
            if (world.Hit(ray, 0, double.MaxValue, out Hit hit))
            {
                return 0.5 * new Color(hit.Normal.X + 1, hit.Normal.Y + 1, hit.Normal.Z + 1);
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

            // World
            HittableList world = new HittableList
            {
                new Sphere(new Point3(0, 0, -1), 0.5),
                new Sphere(new Point3(0, -100.5, -1), 100)
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
                        color += GetRayColor(ray, world);
                    }

                    pixels[y * imageWidth + x] = color / samplesPerPixel;
                }
            }

            Logger.Info($"Done!");
            return pixels;
        }
    }
}
