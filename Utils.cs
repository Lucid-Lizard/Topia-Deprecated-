using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topia
{
    public static class Utils
    {
        public class Float2
        {
            public float x; public float y;
            public Float2(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public Int2 ToInt2()
            {
                return new((int)x, (int)y);
            }
        }

        public class Int2
        {
            public int x; public int y;

            public Int2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public Vector2 ToVec2()
            {
                return new(this.x, this.y);
            }

            public Rectangle ToDestination(Int2 scale)
            {
                return new(this.x, this.y, scale.x, scale.y);
            }

            public static Int2 operator +(Int2 a, Int2 b)
            {
                return new(a.x + b.x, a.y + b.y);
            }

            public static Int2 operator -(Int2 a, Int2 b)
            {
                return new(a.x - b.x, a.y - b.y);
            }
            public static Int2 operator *(Int2 a, Int2 b)
            {
                return new(a.x * b.x, a.y * b.y);
            }
            public static Int2 operator /(Int2 a, float b)
            {
                return new((int)(a.x / b), (int)(a.y / b));
            }

            public static implicit operator Int2(Vector2 v)
            {
                return new((int)v.X, (int)v.Y);
            }
        }

        public class Int4
        {
            public int x; public int y; public int w; public int h;

            public Int4(int x, int y, int w, int h)
            {
                this.x = x;
                this.y = y;
                this.w = w;
                this.h = h;
            }

            public Rectangle ToRect()
            {
                return new(x, y, w, h);
            }

            public Int2 Bounds()
            {
                return new(w, h);
            }
        }

        public static int BoolToInt(bool b) { return b ? 1 : 0; }
    }
}
