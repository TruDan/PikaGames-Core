using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.UI;
using PikaGames.Games.Core.UI.Dialog;
using PikaGames.Games.Core.UI.Keyboard;
using PikaGames.Games.Core.UI.Menu;
using PikaGames.Games.Core.UI.Text;

namespace PikaGames.Games.Core.Scenes
{
    public class DebugScene : Scene
    {

        private UiText _title;

        private UiContainer _debug;

        public DebugScene()
        {
            
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _title = new UiTitle(UiContainer, 50, 50, "Debug");

            _debug = new UiContainer(UiContainer, 50, 100 + _title.Height);

            // Add Debug items below

            var m = new UiMenu(_debug, 0, 0);
            m.AddMenuItem("Open Dialog", () => { new UiDialog("Test Dialog"); });
            m.AddMenuItem("Open Text Input Dialog", () => { new UiTextInputDialog("Test Dialog", (s) => Debug.WriteLine("Response: " + s)); });

            //new UiKeyboard(_debug);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Game.Players.Any(p => p.Input.IsPressed(InputCommand.B)))
            {
                Game.SceneManager.Previous();
            }
        }
    }
}
