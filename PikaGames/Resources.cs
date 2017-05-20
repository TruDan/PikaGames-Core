using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PikaGames.Games.Core
{
    public static class Resources
    {
        private static ContentManager Content;
        private static GraphicsDevice GraphicsDevice;

        public static class Font
        {
            public static SpriteFont Debug { get; private set; }

            public static SpriteFont Game { get; private set; }


            internal static void Init()
            {
                Debug = Content.Load<SpriteFont>("Fonts/DebugFont");
                Game = Content.Load<SpriteFont>("Fonts/GameFont");;
            }
        }


        public static class Image
        {

            public static Texture2D Logo { get; private set; }

            public static Texture2D Background { get; private set; }

            internal static void Init()
            {
                Logo = Load("Logo");
                Background = Load("Background");
            }


            private static Dictionary<string, Texture2D> _cache = new Dictionary<string, Texture2D>();

            private static Texture2D Load(string name)
            {
                var filename = "Images/" + name;

                if (!_cache.ContainsKey(filename))
                {
                    try
                    {
                        _cache[filename] = Content.Load<Texture2D>(filename);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                }

                return _cache[filename];
            }
        }

        public static class Texture
        {
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

        internal static void Init(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            Content = new ContentManager(contentManager.ServiceProvider, "Content");
            GraphicsDevice = graphicsDevice;

            Font.Init();
            Image.Init();
        }

    }
}
