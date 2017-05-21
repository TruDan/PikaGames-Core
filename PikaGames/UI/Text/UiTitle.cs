using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PikaGames.Games.Core.UI.Text
{
    public class UiTitle : UiText
    {
        public UiTitle(UiContainer container, int x, int y, string text) : base(container, x, y, text, UiTheme.TitleColor, UiTheme.TitleShadowColor, UiTheme.TitleShadowSize)
        {
            Scale = 4f;
        }
    }
}
