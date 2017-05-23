using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PikaGames.Games.Core.UI.Dialog
{
    public class UiDialogContainer : UiContainer
    {
        public override int Width { get; set; }
        public override int Height { get; set; }


        public UiDialogContainer(int width, int height)
        {
            Width = width;
            Height = height;
        }

    }
}
