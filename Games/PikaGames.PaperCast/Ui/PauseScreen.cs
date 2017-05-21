using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.UI;
using PikaGames.Games.Core.UI.Menu;
using PikaGames.Games.Core.UI.Text;
using PikaGames.Games.Core.Utils;

namespace PikaGames.PaperCast.Ui
{
    public class PauseScreen : UiContainer
    {
        private readonly PaperCastGame _game;

        private UiText _title;

        private UiMenu _menu;

        public PauseScreen(PaperCastGame game)
        {
            _game = game;

            Init();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_game.Players.Any(p => p.Input.IsPressed(InputCommand.B)))
            {
                _game.GameMapScene.IsPaused = false;
            }
        }

        private void Init()
        {
            _title = new UiText(this, 50, 50, "Paused", MaterialDesignColors.Amber500, MaterialDesignColors.Amber900);
            _title.Scale = 4f;
            
            _menu = new UiMenu(this, 50, 50 + _title.Height + 50);

            _menu.AddMenuItem("Resume", () => _game.GameMapScene.IsPaused = false);
            _menu.AddMenuItem("Main Menu", () => _game.SceneManager.ChangeScene(_game.MainMenuScene));
        }


    }
}
