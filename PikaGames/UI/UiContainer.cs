using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PikaGames.Games.Core.UI
{
    public class UiContainer : UiItem
    {
        public override int Width => ItemCount == 0 ? 0 : Children.Max(c => c.X + c.Width) - Children.Min(c => c.X);
        public override int Height => ItemCount == 0 ? 0 : Children.Max(c => c.Y + c.Height) - Children.Min(c => c.Y);

        protected readonly List<UiItem> Children = new List<UiItem>();
        public int ItemCount => Children.Count;

        public UiContainer(UiContainer container, int x, int y) : base(container, x, y)
        {
        }

        public UiContainer(int x, int y) : this(null, x, y) {}

        public UiContainer() : this(0, 0) {}

        public void AddItem(UiItem item)
        {
            Children.Add(item);
        }

        public void RemoveItem(UiItem item)
        {
            Children.Remove(item);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var item in Children)
            {
                item.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (var item in Children)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
