using System;
using Microsoft.Xna.Framework;
using PikaGames.Games.Core.UI.Text;
using PikaGames.Games.Core.Utils;

namespace PikaGames.Games.Core.UI.Menu
{
    public class UiMenuItem : UiContainer
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
                    _label.Color = UiTheme.MenuActiveTextColor;
                    _label.ShadowColor = UiTheme.MenuActiveTextShadowColor;
                    _label.ShadowSize = UiTheme.MenuActiveTextShadowSize;
                }
                else
                {
                    _label.Color = UiTheme.MenuTextColor;
                    _label.ShadowColor = UiTheme.MenuTextShadowColor;
                    _label.ShadowSize = UiTheme.MenuTextShadowSize;
                }
            }
        }

        private readonly Action _action;

        private UiText _label;

        public UiMenuItem(UiMenu menu, string text, Action action = null) : base(menu, 0, menu.ItemCount * menu.ItemSize)
        {
            _action = action;
            _label = new UiText(this, 0, 0, text);

            IsSelected = false;
        }

        public virtual void Activate()
        {
            _action?.Invoke();
        }
    }
}
