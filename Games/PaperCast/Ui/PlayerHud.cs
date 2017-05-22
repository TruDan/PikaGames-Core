using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PikaGames.Games.Core.UI;
using PikaGames.Games.Core.UI.Text;

namespace PikaGames.PaperCast.Ui
{
    public class PlayerHud : UiContainer
    {
        public PlayerIndex PlayerIndex { get; }
        private PaperCastGame _game;

        private UiContainer _hud;

        private UiText _playerName;
        private UiText _playerScore;

        private UiContainer _pressAToPlay;

        private UiText _press;
        private UiText _toPlay;
        private UiImage _a;

        public PlayerHud(PaperCastGame game, PlayerIndex playerIndex, UiContainer container, int x, int y) : base(container, x, y)
        {
            _game = game;
            PlayerIndex = playerIndex;

            _hud = new UiContainer(this, 25, 25);

            _playerName = new UiTitle(_hud, 0, 0, "Player " + (int)(playerIndex+1));
            _playerName.ShadowSize = UiTheme.TextShadowSize;
            _playerName.Scale = 1.2f;

            _playerScore = new UiText(_hud, 0, _playerName.Y + _playerName.Height + 15, "0");

            _pressAToPlay = new UiContainer(this, 0, 0);

            _press = new UiText(_pressAToPlay, 0, 25, "Press ");
            _a = new UiImage(_pressAToPlay, _pressAToPlay.Width, 25, Games.Core.Resources.Images.Buttons_A);
            _toPlay = new UiText(_pressAToPlay, _pressAToPlay.Width, 25, " to play");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var player = (PaperCastPlayer) _game.Players.FirstOrDefault(p => p.Input.PlayerIndex == PlayerIndex);
            if (player == null || !player.IsConnected)
                IsVisible = false;
            else
            {
                IsVisible = true;

                _playerScore.Text = player.Score.ToString();

                if (!player.IsAlive)
                {
                    _pressAToPlay.IsVisible = true;
                    _hud.IsVisible = false;
                }
                else
                {
                    _pressAToPlay.IsVisible = false;
                    _hud.IsVisible = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

        }
    }
}
