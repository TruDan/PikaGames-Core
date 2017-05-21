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

        protected readonly List<UiItem> Children = new List<UiItem>();
        public int ItemCount => Children.Count;

        public UiContainer(UiContainer container, int x, int y) : base(container, x, y)
        {
            
        }

        public UiContainer() : this(null, 0, 0)
        {
            
        }

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
