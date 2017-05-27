using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientLauncher.Scenes;
using Microsoft.Xna.Framework;
using PikaGames.Games.Core;

namespace ClientLauncher
{
    public class ClientLauncherGame : GameBase
    {
        internal MainMenuScene MainMenuScene;

        protected override void Initialize()
        {
            base.Initialize();
            MainMenuScene = new MainMenuScene();

            SceneManager.DefaultScene = MainMenuScene;

            InitialiseLocalMultiplayer();
        }
    }
}
