using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PikaGames.Games.PaperCast.Level
{
    public class Tile : DrawableGameComponent
    {
        public const int Size = 32;

        public int X { get; }
        public int Y { get; }

        public Vector2 Position => new Vector2(X * Size, Y * Size);

        private Texture2D _texture;

        public Player Owner;
        public Player PendingOwner;

        public Tile(Game game, int x, int y) : base(game)
        {
            X = x;
            Y = y;
            DrawOrder = 1;
        }

        public override void Initialize()
        {
            _texture = new Texture2D(GraphicsDevice, Size, Size, false, SurfaceFormat.Color);
            UpdateTexture();

            base.Initialize();
        }

        internal void UpdateTexture()
        {
            if (_texture == null) return;

            Color[] data = new Color[Size * Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (PendingOwner != null)
                    {
                        data[i * Size + j] = PendingOwner.Color * 0.75f;
                    }
                    else if (Owner != null)
                    {
                        data[i * Size + j] = Owner.Color * 0.9f;
                    }
                    else if (i == 0 || j == 0 || i == (Size - 1) || j == (Size - 1))
                    {
                        data[i * Size + j] = Color.Black * 0.25f;
                    }
                }
            }

            _texture.SetData(data);
        }

        public override void Draw(GameTime gameTime)
        {
            //((PaperCastGame)Game).SpriteBatch.Draw(_texture, Position, Color.White);
            
            base.Draw(gameTime);
        }
    }
}
