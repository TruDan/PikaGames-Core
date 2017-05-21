using System;
using Microsoft.Xna.Framework;
using PikaGames.Games.Core.Utils;

namespace PikaGames.Games.Core.UI.Menu
{
    public class UiMenuItem: UiText
    {
        
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;

                if (_isSelected)
                {
                    Color = ((UiMenu) Container).ActiveColor;
                    ShadowColor = ((UiMenu)Container).ActiveShadowColor;
                }
                else
                {
                    Color = ((UiMenu)Container).DefaultColor;
                    ShadowColor = ((UiMenu)Container).DefaultShadowColor;
                }
            }
        }

        private readonly Action _action;

        public UiMenuItem(UiMenu menu, string text, Action action) : base(menu, 0, menu.ItemCount * menu.ItemSize, text, menu.DefaultColor, menu.DefaultShadowColor)
        {
            _action = action;
        }

        public void Activate()
        {
            _action.Invoke();
        }
    }
}
