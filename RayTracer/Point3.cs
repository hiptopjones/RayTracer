using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public struct Point3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point3(double x, double y)
            : this(x, y, 0)
        {
        }

        public Point3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3(Point3 other)
        {
            X = other.X;
            Y = other.Y;
            Z = other.Z;
        }

        public static Point3 operator +(Point3 a) => a;
        public static Point3 operator -(Point3 a)
        {
            return new Point3
            {
                X = -a.X,
                Y = -a.Y,
                Z = -a.Z
            };
        }

        public static Point3 operator +(Point3 a, Point3 b)
        {
            return new Point3
            {
                X = a.X + b.X,
                Y = a.Y + b.Y,
                Z = a.Z + b.Z
            };
        }

        public static Vector3 operator -(Point3 a, Point3 b)
        {
            return new Vector3
            {
                X = a.X - b.X,
                Y = a.Y - b.Y,
                Z = a.Z - b.Z
            };
        }

        public static Point3 operator *(Point3 a, double c)
        {
            return new Point3
            {
                X = c * a.X,
                Y = c * a.Y,
                Z = c * a.Z
            };
        }

        public static Point3 operator /(Point3 a, double c)
        {
            return new Point3
            {
                X = a.X / c,
                Y = a.Y / c,
                Z = a.Z / c
            };
        }

        // TODO: Is this the right place to define operators that cross types?

        public static Point3 operator +(Point3 p, Vector3 v)
        {
            return new Point3
            {
                X = p.X + v.X,
                Y = p.Y + v.Y,
                Z = p.Z + v.Z
            };
        }

        public static Point3 operator +(Vector3 p, Point3 v)
        {
            return new Point3
            {
                X = p.X + v.X,
                Y = p.Y + v.Y,
                Z = p.Z + v.Z
            };
        }

        public static Point3 operator -(Point3 p, Vector3 v)
        {
            return new Point3
            {
                X = p.X - v.X,
                Y = p.Y - v.Y,
                Z = p.Z - v.Z
            };
        }

        public override string ToString()
        {
            return $"P[ {X:0.000}, {Y:0.000}, {Z:0.000} ]";
        }
    }
}
