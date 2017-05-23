using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PikaGames.Games.Core.UI.Menu;
using PikaGames.Games.Core.UI.Text;

namespace PikaGames.Games.Core.UI.Controls
{
    public class UiControl : UiContainer, ISelectable
    {
        public bool IsSelected { get; set; }
        
        protected UiText Label { get; }

        public UiControl(UiContainer container, int x, int y, string label) : base(container, x, y)
        {
            Label = new UiText(this, 0, 0, label);

            HasBackground = true;
            Background = UiThemeResources.ControlBackground;
            BackgroundShadow = UiThemeResources.ControlBackgroundShadow;
            BackgroundShadowSize = UiTheme.ControlBackgroundShadowSize;
        }

        public virtual void Focus()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (HasBackground)
            {
                if (IsSelected)
                {
                    Background = UiThemeResources.ControlActiveBackground;
                    BackgroundShadow = UiThemeResources.ControlActiveBackgroundShadow;
                    BackgroundShadowSize = UiTheme.ControlActiveBackgroundShadowSize;

                    Label.Color = UiTheme.ControlActiveForegroundColor;
                    Label.ShadowColor = UiTheme.ControlActiveForegroundShadowColor;
                    Label.ShadowSize = UiTheme.ControlActiveBackgroundShadowSize;
                }
                else
                {
                    Background = UiThemeResources.ControlBackground;
                    BackgroundShadow = UiThemeResources.ControlBackgroundShadow;
                    BackgroundShadowSize = UiTheme.ControlBackgroundShadowSize;

                    Label.Color = UiTheme.ControlForegroundColor;
                    Label.ShadowColor = UiTheme.ControlForegroundShadowColor;
                    Label.ShadowSize = UiTheme.ControlBackgroundShadowSize;
                }
            }
        }
    }
}
