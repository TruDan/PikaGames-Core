using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GameLauncher.Scenes;
using Microsoft.Xna.Framework.Graphics;
using PikaGames.Games.Core;

namespace GameLauncher
{
    public class GameLauncherGame : GameBase
    {
        internal MainMenuScene MainMenuScene;
        internal GameSelectScene GameSelectScene;
        internal OptionsMenuScene OptionsMenuScene;
        
        public GameLauncherGame(RootGame rootGame) : base(rootGame) { }

        protected override void Initialize()
        {
            Debug.WriteLine("Initialise GameLauncher");

            base.Initialize();
            Game.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            
            MainMenuScene = new MainMenuScene(this);
            GameSelectScene = new GameSelectScene();
            OptionsMenuScene = new OptionsMenuScene();

            SceneManager.DefaultScene = MainMenuScene;

            InitialiseLocalMultiplayer();
        }

    }
}
