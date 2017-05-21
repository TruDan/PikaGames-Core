using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PikaGames.Games.Core.UI.ButtonBar
{
    public class UiButtonBarItem : UiContainer
    {
        private static int ImageSize = 24;

        private UiImage _image;
        private UiText _label;
        
        public UiButtonBarItem(UiButtonBar bar, int x, int y, Buttons button, string text) : base(bar, x, y)
        {
            Texture2D texture = null;
            switch (button)
            {
                case Buttons.A:
                    texture = Resources.Images.Buttons_A;
                    break;
                case Buttons.B:
                    texture = Resources.Images.Buttons_B;
                    break;
            }

            if(texture != null)
                _image = new UiImage(this, 0, 0, texture);

            _label = new UiText(this, ImageSize, 0, text);
        }
    }
}
