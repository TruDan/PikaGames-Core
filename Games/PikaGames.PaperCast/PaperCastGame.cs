using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PikaGames.Games.Core;
using PikaGames.Games.Core.Entities;
using PikaGames.Games.Core.Utils;
using PikaGames.PaperCast.Scenes;
using PikaGames.PaperCast.World;

namespace PikaGames.PaperCast
{
    public class PaperCastGame : GameBase
    {
        private static readonly Color[] PlayerColors = new Color[]
        {
            MaterialDesignColors.LightBlue500,
            MaterialDesignColors.LightGreen500,
            MaterialDesignColors.Purple500,
            MaterialDesignColors.Orange500
        };

        internal MainMenuScene MainMenuScene;
        internal GameMapScene GameMapScene;

        public PaperCastGame()
        {
            
        }

        public override Player CreatePlayer(PlayerIndex playerIndex)
        {
            return new PaperCastPlayer(playerIndex, GameMapScene.Level, PlayerColors[(int)playerIndex]);
        }

        protected override void Initialize()
        {
            base.Initialize();
            Resources.Init(ContentManager, GraphicsDevice);

            MainMenuScene = new MainMenuScene();
            GameMapScene = new GameMapScene(this);

            SceneManager.DefaultScene = MainMenuScene;

            InitialiseLocalMultiplayer();
        }
    }
}
