using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PikaGames.Games.Core.Utils
{
    public static class MathUtils
    {
        public static double PiOver180 = Math.PI / 180.0;

        public static double SinInterpolation(double a, double b, double t)
        {
            return a + Math.Sin(t * PiOver180) * (b - a);
        }

        public static double TriangularWave(double a, double b, double t)
        {
            return a + Math.Abs((b - a) - t % (2 * (b - a)));
        }

	    public static Vector2 Project(this Vector2 a, Vector2 v)
	    {
			float thisDotV = (a.X * v.X + a.Y * v.Y);
			return v * thisDotV;
		}

		public static Vector2 Project(this Vector2 a, Vector2 v, out float mag)
		{
			float thisDotV = (a.X * v.X + a.Y * v.Y);
			mag = thisDotV;

			return v * thisDotV;
		}

	    public static float Magnitude(this Vector2 v)
	    {
		    return (float) Math.Sqrt((v.X * v.X) + (v.Y * v.Y));
		}
	}
}
