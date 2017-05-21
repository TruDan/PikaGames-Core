using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PikaGames.Games.Core.UI.Menu;

namespace PikaGames.Games.Core.UI.Controls
{
    public class UiControl : UiMenuItem
    {
        public UiControl(UiMenu menu, int x, int y, string label) : base(menu, label)
        {

        }
    }
}
