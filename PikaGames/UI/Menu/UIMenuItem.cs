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
                    Color = UiTheme.MenuActiveTextColor;
                    ShadowColor = UiTheme.MenuActiveTextShadowColor;
                    ShadowSize = UiTheme.MenuActiveTextShadowSize;
                }
                else
                {
                    Color = UiTheme.MenuTextColor;
                    ShadowColor = UiTheme.MenuTextShadowColor;
                    ShadowSize = UiTheme.MenuTextShadowSize;
                }
            }
        }

        private readonly Action _action;

        public UiMenuItem(UiMenu menu, string text, Action action = null) : base(menu, 0, menu.ItemCount * menu.ItemSize, text)
        {
            _action = action;
            IsSelected = false;
        }

        public virtual void Activate()
        {
            _action?.Invoke();
        }
    }
}
