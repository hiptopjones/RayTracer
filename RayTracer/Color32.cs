using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public struct Color32
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public Color32(byte r, byte g, byte b)
            : this(r, g, b, 0)
        {
        }

        public Color32(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color32(Color32 other)
        {
            R = other.R;
            G = other.G;
            B = other.B;
            A = other.A;
        }

        public static Color32 operator +(Color32 a, Color32 b)
        {
            return new Color32
            {
                R = (byte)(a.R + b.R),
                G = (byte)(a.G + b.G),
                B = (byte)(a.B + b.B),
                A = (byte)(a.A + b.A)
            };
        }

        public static Color32 operator -(Color32 a, Color32 b)
        {
            return new Color32
            {
                R = (byte)(a.R - b.R),
                G = (byte)(a.G - b.G),
                B = (byte)(a.B - b.B),
                A = (byte)(a.A - b.A)
            };
        }

        public static Color32 operator *(Color32 a, double c)
        {
            return new Color32
            {
                R = (byte)(c * a.R),
                G = (byte)(c * a.G),
                B = (byte)(c * a.B),
                A = (byte)(c * a.A)
            };
        }

        public static Color32 operator /(Color32 a, double c)
        {
            return new Color32
            {
                R = (byte)(a.R / c),
                G = (byte)(a.G / c),
                B = (byte)(a.B / c),
                A = (byte)(a.A / c)
            };
        }

        public static Color32 Lerp(Color32 start, Color32 end, double percent)
        {
            return start + (end - start) * percent;
        }

        public Color ToColor()
        {
            Color color = new Color
            {
                R = R / (256 - 1),
                G = G / (256 - 1),
                B = B / (256 - 1),
                A = A / (256 - 1)
            };

            return color;
        }

        public override string ToString()
        {
            return $"C[ {R,3}, {G,3}, {B,3}, {A,3} ]";
        }
    }
}
