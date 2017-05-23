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
        protected UiContainer Container { get; set; }

        public virtual int X { get; set; } = 0;
        public virtual int Y { get; set; } = 0;

        public virtual Vector2 Position => (Container?.Position ?? Vector2.Zero) + new Vector2(X, Y);
        public Rectangle Bounds => new Rectangle((int) Position.X, (int) Position.Y, Width, Height);

        public virtual int Width { get; set; } = 0;
        public virtual int Height { get; set; } = 0;

        public bool HasBackground { get; set; } = false;

        public Texture2D Background { get; set; }
        public Texture2D BackgroundShadow { get; set; }
        public int BackgroundShadowSize { get; set; }

        public bool IsVisible { get; set; } = true;

        protected UiItem(UiContainer container)
        {
            Container = container;
            Container?.AddItem(this);
        }

        protected UiItem(UiContainer container, int x, int y) : this(container)
        {
            X = x;
            Y = y;
        }

        internal void SetParent(UiContainer container)
        {
            Container?.RemoveItem(this);
            Container = container;
            Container?.AddItem(this);
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (HasBackground)
            {
                // Background
                for (int i = 0; i < BackgroundShadowSize; i++)
                {
                    spriteBatch.Draw(BackgroundShadow,
                        new Rectangle((int) Position.X + i, (int) Position.Y + i, Width, Height), Color.White);
                }

                spriteBatch.Draw(Background, Bounds, Color.White);
            }
        }
    }
}
