using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PikaGames.Games.Core.UI.ButtonBar
{
    public class UiButtonBar : UiContainer
    {
        public override Vector2 Position => base.Position - new Vector2(Width, Height);

        public UiButtonBar(UiContainer container, int x, int y) : base(container, x, y)
        {
        }

        public void AddButton(Buttons button, string label) {
            var buttonBarItem = new UiButtonBarItem(this, Width + (25 * ItemCount), 0, button, label);
        }


    }
}
