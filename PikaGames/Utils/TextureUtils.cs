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
