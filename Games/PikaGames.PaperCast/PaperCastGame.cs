using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PikaGames.Games.Core;
using PikaGames.PaperCast.Scenes;

namespace PikaGames.PaperCast
{
    public class PaperCastGame : GameBase
    {
        private MainMenuScene _mainMenuScene;
        private GameMapScene _gameMapScene;

        public PaperCastGame()
        {
            
        }


        protected override void Initialize()
        {
            base.Initialize();
            Resources.Init(ContentManager, GraphicsDevice);

            _mainMenuScene = new MainMenuScene();
            _gameMapScene = new GameMapScene();

            SceneManager.DefaultScene = _mainMenuScene;
        }
    }
}
