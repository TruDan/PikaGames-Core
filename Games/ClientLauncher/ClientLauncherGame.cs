using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientLauncher.Scenes;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.NuclexGui;
using PikaGames.Games.Core;
using PikaGames.Games.Core.Gui;
using PikaGames.Games.Core.Gui.Pika;

namespace ClientLauncher
{
    public class ClientLauncherGame : GameBase
    {
        public MainMenuScene MainMenuScene;


        public ClientLauncherGame(RootGame rootGame) : base(rootGame)
        {
            MainMenuScene = new MainMenuScene();
        }

        protected override void Initialize()
        {
            base.Initialize();

            SceneManager.DefaultScene = MainMenuScene;

            InitialiseLocalMultiplayer();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
