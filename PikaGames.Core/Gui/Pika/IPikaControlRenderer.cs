using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.NuclexGui.Controls;

namespace PikaGames.Games.Core.Gui.Pika
{
    /// <summary>Interface for a class that renders a control</summary>
    public interface IPikaControlRenderer
    {
    }

    /// <summary>Interface for a class responsible to render a specific control type</summary>
    /// <typeparam name="ControlType">Type of control the implementation class will render</typeparam>
    public interface IPikaControlRenderer<TControlType> : IPikaControlRenderer where TControlType : GuiControl
    {
        /// <summary>Renders the specified control using the provided graphics interface</summary>
        /// <param name="control">Control that will be rendered</param>
        /// <param name="graphics">Graphics interface that will be used to draw the control</param>
        void Render(TControlType control, IPikaGuiGraphics graphics);
    }
}
