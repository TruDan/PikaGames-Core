using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using MonoGame.Extended.NuclexGui.Controls.Desktop;

namespace PikaGames.Games.Core.Gui.Pika.Renderers
{
    public class PikaVerticalSliderControlRenderer : IPikaControlRenderer<GuiVerticalSliderControl>
    {
        /// <summary>
        ///     Renders the specified control using the provided graphics interface
        /// </summary>
        /// <param name="control">Control that will be rendered</param>
        /// <param name="graphics">
        ///     Graphics interface that will be used to draw the control
        /// </param>
        public void Render(
            GuiVerticalSliderControl control, IPikaGuiGraphics graphics
        )
        {
            var controlBounds = control.GetAbsoluteBounds();

            var thumbHeight = controlBounds.Height * control.ThumbSize;
            var thumbY = (controlBounds.Height - thumbHeight) * control.ThumbPosition;

            graphics.DrawElement("rail.vertical", controlBounds);

            var thumbBounds = new RectangleF(
                controlBounds.X, controlBounds.Y + thumbY, controlBounds.Width, thumbHeight
            );

            if (control.ThumbDepressed)
                graphics.DrawElement("slider.vertical.depressed", thumbBounds);
            else
            {
                if (control.MouseOverThumb)
                    graphics.DrawElement("slider.vertical.highlighted", thumbBounds);
                else graphics.DrawElement("slider.vertical.normal", thumbBounds);
            }
        }
    }
}
