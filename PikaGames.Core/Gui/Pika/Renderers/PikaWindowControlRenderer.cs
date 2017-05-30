using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using MonoGame.Extended.NuclexGui.Controls.Desktop;

namespace PikaGames.Games.Core.Gui.Pika.Renderers
{
    public class PikaWindowControlRenderer : IPikaControlRenderer<GuiWindowControl>
    {
        /// <summary>
        ///     Renders the specified control using the provided graphics interface
        /// </summary>
        /// <param name="control">Control that will be rendered</param>
        /// <param name="graphics">
        ///     Graphics interface that will be used to draw the control
        /// </param>
        public void Render(GuiWindowControl control, IPikaGuiGraphics graphics)
        {
            var controlBounds = control.GetAbsoluteBounds();
            graphics.DrawElement("window", controlBounds);

            if (control.Title != null)
                graphics.DrawString("window", controlBounds, control.Title);
        }
    }
}
