using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.NuclexGui;
using MonoGame.Extended.ViewportAdapters;

namespace PikaGames.Games.Core.Gui
{
    public class PikaGuiInputService : IGuiInputService
    {
        public KeyboardListener KeyboardListener { get; }
        public MouseListener MouseListener { get; }
        public GamePadListener GamePadListener { get; }
        public TouchListener TouchListener { get; }

        public PikaGuiInputService(InputListenerComponent inputListener, ViewportAdapter viewportAdapter)
        {
            inputListener.Listeners.Add((InputListener)(this.KeyboardListener = new KeyboardListener()));
            inputListener.Listeners.Add((InputListener)(this.MouseListener = new MouseListener(viewportAdapter)));
            inputListener.Listeners.Add((InputListener)(this.GamePadListener = new GamePadListener()));
            inputListener.Listeners.Add((InputListener)(this.TouchListener = new TouchListener(viewportAdapter)));
        }
    }
}
