using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;

namespace PikaGames.Games.Core.UI
{
    public abstract class UiItem
    {
        protected UiContainer Container { get; }

        public int X { get; set; }
        public int Y { get; set; }

        public Vector2 Position => (Container?.Position ?? Vector2.Zero) + new Vector2(X, Y);

        public virtual int Width => 0;
        public virtual int Height => 0;

        protected UiItem(UiContainer container, int x, int y)
        {
            Container = container;
            X = x;
            Y = y;

            Container?.AddItem(this);
        }


        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
