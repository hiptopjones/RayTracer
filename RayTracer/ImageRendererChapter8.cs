using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    // Ray Tracing In One Weekend, Chapter 8
    class ImageRendererChapter8
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private Random Random { get; } = new Random();

        public Vector3 CreateRandomVector3(double min, double max)
        {
            return new Vector3
            {
                X = Utilities.Lerp(min, max, Random.NextDouble()),
                Y = Utilities.Lerp(min, max, Random.NextDouble()),
                Z = Utilities.Lerp(min, max, Random.NextDouble())
            };
        }

        private Vector3 GetVectorInUnitSphere()
        {
            while (true)
            {
                Vector3 v = CreateRandomVector3(-1, 1);
                if (v.MagnitudeSquared >= 1)
                {
                    continue;
                }

                return v;
            }
        }

        private Point3 GetPointInUnitSphere()
        {
            return Point3.Origin + GetVectorInUnitSphere();
        }

        private Point3 GetPointOnUnitSphere()
        {
            return Point3.Origin + GetVectorInUnitSphere().Normalized;
        }

        private Point3 GetPointInHemisphere(Vector3 normal)
        {
            Vector3 v = GetVectorInUnitSphere();
            if (Vector3.DotProduct(v, normal) < 0)
            {
                v = -v;
            }

            return Point3.Origin + v;
        }

        // Calculate the color for each ray based on what it hits
        private Color GetRayColor(Ray ray, HittableList world, int maxBounceDepth)
        {
            if (maxBounceDepth <= 0)
            {
                return Color.Black;
            }

            if (world.Hit(ray, 0.001, double.MaxValue, out Hit hit))
            {
                Point3 target = hit.Position + hit.Normal + GetPointInHemisphere(hit.Normal);
                return 0.5 * GetRayColor(new Ray(hit.Position, target - hit.Position), world, maxBounceDepth - 1);
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
            int samplesPerPixel = 20;
            int maxBounceDepth = 20;

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
