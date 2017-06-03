using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.NuclexGui.Controls.Desktop;

namespace PikaGames.Games.Core.Gui
{
    public static class GuiWindowExtensions
    {

        public static void ShowDialog(this GuiWindowControl window)
        {
            var children = RootGame.Instance.GuiManager.Screen.Desktop.Children;
            if(children.Contains(window)) return;
                children.Add(window);

            window.BringToFront();
        }



    }
}
