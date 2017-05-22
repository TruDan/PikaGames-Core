using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PikaGames.Games.Core.Utils
{

    public static class TextureUtils
    {
        private static GraphicsDevice GraphicsDevice;

        internal static void Init(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
        }

        public static Texture2D TintSolid(this Texture2D texture, Color tintColor)
        {
            var data = new Color[texture.Width * texture.Height];
            texture.GetData(data);

            for (int i = 0; i < data.Length; i++)
            {
                var c = data[i];

                if (c.A > 0)
                {
                    c.R = tintColor.R;
                    c.G = tintColor.G;
                    c.B = tintColor.B;
                }

                data[i] = c;
            }

            var t = new Texture2D(texture.GraphicsDevice, texture.Width, texture.Height);
            t.SetData(data);

            return t;
        }

        public static Texture2D TintInverse(this Texture2D texture, Color tintColor)
        {
            var data = new Color[texture.Width * texture.Height];
            texture.GetData(data);

            for (int i = 0; i < data.Length; i++)
            {
                var c = data[i];

                c.R = (byte)(c.R - (1 - c.R / 255d) * tintColor.R);
                c.G = (byte)(c.G - (1 - c.R / 255d) * tintColor.G);
                c.B = (byte)(c.B - (1 - c.R / 255d) * tintColor.B);
                c.A = (byte)(c.A - (1 - c.R / 255d) * tintColor.A);

                data[i] = c;
            }

            var t = new Texture2D(texture.GraphicsDevice, texture.Width, texture.Height);
            t.SetData(data);

            return t;
        }

        public static Texture2D Tint(this Texture2D texture, Color tintColor)
        {
            var data = new Color[texture.Width * texture.Height];
            texture.GetData(data);

            for (int i = 0; i < data.Length; i++)
            {
                var c = data[i];

                c.R = (byte)(c.R + (1 - c.R / 255d) * tintColor.R);
                c.G = (byte)(c.G + (1 - c.R / 255d) * tintColor.G);
                c.B = (byte)(c.B + (1 - c.R / 255d) * tintColor.B);
                c.A = (byte)(c.A + (1 - c.R / 255d) * tintColor.A);
                data[i] = c;
            }

            var t = new Texture2D(texture.GraphicsDevice, texture.Width, texture.Height);
            t.SetData(data);

            return t;
        }

        public static Texture2D CreateRectangle(int width, int height, Color fillColor)
        {
            var texture = new Texture2D(GraphicsDevice, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++) data[i] = fillColor;
            texture.SetData(data);
            return texture;
        }

        public static Texture2D CreateBorderedRectangle(int width, int height, Color fillColor, Color borderColor)
        {
            var texture = new Texture2D(GraphicsDevice, width, height);
            Color[] data = new Color[width * height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    data[x * width + y] = fillColor;

                    if (x == 0 || y == 0 || x == (width - 1) || y == (height - 1))
                    {
                        data[x * width + y] = borderColor;
                    }
                }
            }

            texture.SetData(data);
            return texture;
        }
    }
}
