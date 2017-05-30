using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientLauncher.Scenes;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.NuclexGui;
using PikaGames.Games.Core;
using PikaGames.Games.Core.Gui.Pika;

namespace ClientLauncher
{
    public class ClientLauncherGame : GameBase
    {
        internal MainMenuScene MainMenuScene;

        private InputListenerComponent _inputManager;
        internal GuiManager GuiManager;

        protected override void Initialize()
        {
            base.Initialize();
            MainMenuScene = new MainMenuScene();

            SceneManager.DefaultScene = MainMenuScene;

            _inputManager = new InputListenerComponent(Game);
            GuiManager = new GuiManager(Services, new GuiInputService(_inputManager));
            GuiManager.Visualizer =
                PikaGuiVisualizer.FromResource(Services, "PikaGames.Games.Core.Gui.Pika.Skins.PikaSkin.json");

            GuiManager.Screen = new GuiScreen(VirtualSize.X, VirtualSize.Y);
            GuiManager.Screen.Desktop.Bounds = new UniRectangle(new UniScalar(0f, 0), new UniScalar(0f, 0), new UniScalar(1f, 0), new UniScalar(1f, 0));

            GuiManager.Initialize();

            InitialiseLocalMultiplayer();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _inputManager.Update(gameTime);
            GuiManager.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GuiManager.Draw(gameTime);
        }
    }
}
