using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended.NuclexGui.Visuals.Flat;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.UI.Menu;

namespace PikaGames.Games.Core.UI.Controls
{
    public class UiControlGroup : UiMenu
    {
        public UiControl ActiveControl => (UiControl) ActiveItem;

        public UiControlGroup(UiContainer container, int x, int y) : base(container, x, y) { }

        public void AddSlider(string name)
        {
            var slider = new UiSliderControl(this, 0, Children.Count * ItemSize, name);
        }
        
    }
}
