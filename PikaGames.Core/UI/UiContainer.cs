using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.NuclexGui.Visuals.Flat;
using PikaGames.Games.Core.Utils;

namespace PikaGames.Games.Core.UI
{
    public class UiContainer : UiItem
    {
        public override int Width => ItemCount == 0 ? 0 : Children.Where(c => c.IsVisible).Max(c => c.X + c.Width) - Children.Where(c => c.IsVisible).Min(c => c.X);
        public override int Height => ItemCount == 0 ? 0 : Children.Where(c => c.IsVisible).Max(c => c.Y + c.Height) - Children.Where(c => c.IsVisible).Min(c => c.Y);

        private bool _isFocused;

        public bool IsFocused
        {
            get => Container != null ? Container.IsFocused : _isFocused;
            set => _isFocused = value;
        }

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

            foreach (var item in Children.ToArray())
            {
                item.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (var item in Children.ToArray())
            {
                if(item.IsVisible)
                    item.Draw(spriteBatch);
            }
        }

        public void AlignChildren(Frame.HorizontalTextAlignment alignment)
        {
            if (alignment == Frame.HorizontalTextAlignment.Left)
            {
                foreach (var item in Children)
                {
                    item.X = 0;
                }
            }
            else if (alignment == Frame.HorizontalTextAlignment.Right)
            {
                foreach (var item in Children)
                {
                    item.X = Width - item.Width;
                }
            }
            else if (alignment == Frame.HorizontalTextAlignment.Center)
            {
                foreach (var item in Children)
                {
                    item.X = (Width - item.Width) / 2;
                }
            }
        }

        public void AlignChildren(Frame.VerticalTextAlignment alignment)
        {
            if (alignment == Frame.VerticalTextAlignment.Top)
            {
                foreach (var item in Children)
                {
                    item.Y = 0;
                }
            }
            else if (alignment == Frame.VerticalTextAlignment.Bottom)
            {
                foreach (var item in Children)
                {
                    item.Y = Height - item.Height;
                }
            }
            else if (alignment == Frame.VerticalTextAlignment.Center)
            {
                foreach (var item in Children)
                {
                    item.Y = (Height - item.Height) / 2;
                }
            }
        }

        public void DistributeChildren(int width)
        {
            var count = Children.Count;

            if (count == 0) return;

            var childWidth = width / count;

            int i = 0;
            foreach (var item in Children)
            {
                var relativeX = (childWidth - item.Width)/2;
                item.X = (childWidth * i) + relativeX;
                i++;
            }
        }
    }
}
