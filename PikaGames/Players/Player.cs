using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace PikaGames.Games.Core.Players
{
    public class Player
    {

        public int Width = 1;
        public int Height = 1;

        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public Vector2 Position => new Vector2(X, Y);

        private readonly Texture2D _texture;

        public Player(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
