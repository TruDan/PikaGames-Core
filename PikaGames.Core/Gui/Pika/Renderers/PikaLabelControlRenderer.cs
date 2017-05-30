using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.NuclexGui.Controls;

namespace PikaGames.Games.Core.Gui.Pika.Renderers
{
    public class PikaLabelControlRenderer : IPikaControlRenderer<GuiLabelControl>
    {
        /// <summary>
        ///     Renders the specified control using the provided graphics interface
        /// </summary>
        /// <param name="control">Control that will be rendered</param>
        /// <param name="graphics">
        ///     Graphics interface that will be used to draw the control
        /// </param>
        public void Render(GuiLabelControl control, IPikaGuiGraphics graphics)
        {
            graphics.DrawString("label", control.GetAbsoluteBounds(), control.Text);
        }
    }
}
