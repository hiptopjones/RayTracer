using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public struct Color
    {
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }
        public double A { get; set; }

        public Color(double r, double g, double b)
            : this(r, g, b, 0)
        {
        }

        public Color(double r, double g, double b, double a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color(Color other)
        {
            R = other.R;
            G = other.G;
            B = other.B;
            A = other.A;
        }

        public static Color operator +(Color a, Color b)
        {
            return new Color
            {
                R = a.R + b.R,
                G = a.G + b.G,
                B = a.B + b.B,
                A = a.A + b.A
            };
        }

        public static Color operator -(Color a, Color b)
        {
            return new Color
            {
                R = a.R - b.R,
                G = a.G - b.G,
                B = a.B - b.B,
                A = a.A - b.A
            };
        }

        public static Color operator *(Color a, double c)
        {
            return new Color
            {
                R = c * a.R,
                G = c * a.G,
                B = c * a.B,
                A = c * a.A
            };
        }

        public static Color operator /(Color a, double c)
        {
            return new Color
            {
                R = a.R / c,
                G = a.G / c,
                B = a.B / c,
                A = a.A / c
            };
        }

        public static Color Lerp(Color start, Color end, double percent)
        {
            return start + (end - start) * percent;
        }

        public Color32 ToColor32()
        {
            // TODO: Clamp members to 0-1 first?
            Color32 color32 = new Color32
            {
                R = (byte)(R * 255),
                G = (byte)(G * 255),
                B = (byte)(B * 255),
                A = (byte)(A * 255)
            };

            return color32;
        }

        public override string ToString()
        {
            return $"C[ {R:0.000}, {G:0.000}, {B:0.000}, {A:0.000} ]";
        }
    }
}
