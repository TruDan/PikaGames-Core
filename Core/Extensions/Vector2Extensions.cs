using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PikaGames.Core.Extensions
{
    static class Vector2Extensions
    {
        public static float ToAngle(this Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public static Vector2 ScaleTo(this Vector2 vector, float length)
        {
            return vector * (length / vector.Length());
        }

        public static Point ToPoint(this Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }
    }
}
