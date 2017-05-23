using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaGames.Games.Core.UI.Controls
{
    public class UiToggle : UiControl
    {
        public override int Width { get; set; } = 50;

        public UiToggle(UiContainer container, int x, int y, string label) : base(container, x, y, label)
        {

        }
    }
}
