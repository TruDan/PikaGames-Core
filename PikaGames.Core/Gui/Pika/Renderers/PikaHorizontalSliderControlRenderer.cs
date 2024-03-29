﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using MonoGame.Extended.NuclexGui.Controls.Desktop;

namespace PikaGames.Games.Core.Gui.Pika.Renderers
{
    public class PikaHorizontalSliderControlRenderer : IPikaControlRenderer<GuiHorizontalSliderControl>
    {
        /// <summary>
        ///     Renders the specified control using the provided graphics interface
        /// </summary>
        /// <param name="control">Control that will be rendered</param>
        /// <param name="graphics">
        ///     Graphics interface that will be used to draw the control
        /// </param>
        public void Render(GuiHorizontalSliderControl control, IPikaGuiGraphics graphics
        )
        {
            var controlBounds = control.GetAbsoluteBounds();

            var thumbWidth = controlBounds.Width * control.ThumbSize;
            var thumbX = (controlBounds.Width - thumbWidth) * control.ThumbPosition;

            graphics.DrawElement("rail.horizontal", controlBounds);

            var thumbBounds = new RectangleF(controlBounds.X + thumbX, controlBounds.Y, thumbWidth, controlBounds.Height);

            if (control.ThumbDepressed)
                graphics.DrawElement("slider.horizontal.depressed", thumbBounds);
            else
            {
                if (control.MouseOverThumb)
                    graphics.DrawElement("slider.horizontal.highlighted", thumbBounds);
                else graphics.DrawElement("slider.horizontal.normal", thumbBounds);
            }
        }
    }
}
