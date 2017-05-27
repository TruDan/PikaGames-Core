using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PikaGames.Games.Core.Entities
{
    public class Entity
    {
        public int Width = 1;
        public int Height = 1;

	    public Vector2 Position { get; set; } = Vector2.Zero;

        public bool IsAlive { get; protected set; }



        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

    }
}
