using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PikaGames.Games.Core.Entities
{
    public class Entity
    {


        public int Width = 1;
        public int Height = 1;

        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public Vector2 Position => new Vector2(X, Y);

        public bool IsAlive { get; protected set; }



        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

    }
}
