using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PikaGames.Games.Core;

namespace PikaGames.PaperCast.World
{
    public class Tile
    {
        public const int Size = 32;

        public int X { get; }
        public int Y { get; }

        public Vector2 Position => new Vector2(X * Size, Y * Size);

        private Texture2D _texture;

        public PaperCastPlayer Owner;
        public PaperCastPlayer PendingOwner;

        protected Game Game { get; }

        public Tile(Game game, int x, int y)
        {
            Game = game;
            X = x;
            Y = y;

            UpdateTexture();
        }

        internal void UpdateTexture()
        {
            if (PendingOwner != null)
            {
                _texture = Games.Core.Resources.Texture.CreateRectangle(Size, Size, PendingOwner.Color * 0.75f);
            }
            else if (Owner != null)
            {
                _texture = Games.Core.Resources.Texture.CreateRectangle(Size, Size, Owner.Color * 0.9f);
            }
            else
            {
                _texture = Games.Core.Resources.Texture.CreateBorderedRectangle(Size, Size, Color.Black * 0.25f, Color.Black * 0.75f);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
