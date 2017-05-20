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
        private GameMapScene _gameMapScene;

        public PaperCastGame()
        {
            
        }


        protected override void Initialize()
        {
            base.Initialize();

            _gameMapScene = new GameMapScene();

            SceneManager.DefaultScene = _gameMapScene;
        }
    }
}
