using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.NuclexGui.Visuals.Flat;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.Utils;

namespace PikaGames.Games.Core.UI.Menu
{
    public class UiMenu : UiContainer
    {
        public Color DefaultColor { get; set; } = MaterialDesignColors.LightBlue500;
        public Color DefaultShadowColor { get; set; } = MaterialDesignColors.LightBlue900;

        public Color ActiveColor { get; set; } = MaterialDesignColors.Yellow500;
        public Color ActiveShadowColor { get; set; } = MaterialDesignColors.Yellow900;

        private int _menuSelectedIndex = 0;

        public Frame.HorizontalTextAlignment Alignment { get; set; } = Frame.HorizontalTextAlignment.Left;

        public int ItemSpacing = 10;
        public int ItemSize => (Children.Count > 0 ? Children.Max(i => i.Height) : 25) + ItemSpacing;

        public UiMenuItem ActiveItem => (UiMenuItem) Children[_menuSelectedIndex];

        private int _x, _y;
            

        public UiMenu(UiContainer container, int x, int y) : base(container, x, y)
        {
            _x = x;
            _y = y;
        }

        public void AddMenuItem(string text, Action action)
        {
            var menuItem = new UiMenuItem(this, text, action);
            AlignItems();
        }

        private void AlignItems()
        {
            if (Alignment == Frame.HorizontalTextAlignment.Left)
            {
                X = _x + 0;
                foreach (var item in Children)
                {
                    item.X = 0;
                }
            }
            else if (Alignment == Frame.HorizontalTextAlignment.Right)
            {
                var x = Children.Max(c => c.Width);
                X = _x + x;
                foreach (var item in Children)
                {
                    item.X = x;
                }
            }
            else if (Alignment == Frame.HorizontalTextAlignment.Center)
            {
                var x = Children.Max(c => c.Width);
                X = _x - (x / 2);
                foreach (var item in Children)
                {
                    item.X = (x-item.Width)/2;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (GameBase.Instance.Players.Any(p => p.Input.IsPressed(InputCommand.Up)))
            {
                ActiveItem.IsSelected = false;
                if (_menuSelectedIndex == 0) _menuSelectedIndex = Children.Count - 1;
                else _menuSelectedIndex--;
                
                GameBase.Instance.SoundManager.Play(Resources.Sfx.SpaceMorph);
            }
            else if (GameBase.Instance.Players.Any(p => p.Input.IsPressed(InputCommand.Down)))
            {
                ActiveItem.IsSelected = false;
                _menuSelectedIndex++;
                if (_menuSelectedIndex == Children.Count) _menuSelectedIndex = 0;
                
                GameBase.Instance.SoundManager.Play(Resources.Sfx.SpaceMorph);
            }
            else if (GameBase.Instance.Players.Any(p => p.Input.IsPressed(InputCommand.A)))
            {
                var menuItem = (UiMenuItem) Children[_menuSelectedIndex];
                menuItem.Activate();
            }

            ActiveItem.IsSelected = true;

            base.Update(gameTime);
        }
    }
}
