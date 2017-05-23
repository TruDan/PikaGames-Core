using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended.NuclexGui.Visuals.Flat;
using PikaGames.Games.Core.Entities;
using PikaGames.Games.Core.UI.Text;
using PikaGames.Games.Core.Utils;

namespace PikaGames.Games.Core.UI.PlayerBar
{
    public class UiPlayerBarItem : UiContainer
    {
        public PlayerIndex PlayerIndex { get; }

        private readonly UiText _playerName;
        
        private readonly UiContainer _pressAToPlay;
        private readonly UiText _connectController;

        public UiPlayerBarItem(UiPlayerBar container, int x, int y, PlayerIndex playerIndex) : base(container, x, y)
        {
            PlayerIndex = playerIndex;

            _playerName = new UiTitle(this, 0, 0, "Player " + (int)(playerIndex + 1));
            _playerName.ShadowSize = UiTheme.TextShadowSize;
            _playerName.Scale = 1.2f;

            _pressAToPlay = new UiContainer(this, 0, 0);

            new UiText(_pressAToPlay, 0, 25, "Press ");
            new UiImage(_pressAToPlay, _pressAToPlay.Width, 25, Resources.Images.Buttons_A);
            new UiText(_pressAToPlay, _pressAToPlay.Width, 25, " to play");

            _connectController = new UiText(this, 0, 25, "Disconnected", UiTheme.TextDisabledColor, UiTheme.TextDisabledShadowColor, UiTheme.TextDisabledShadowSize);
        }

        private Player GetPlayer()
        {
            return GameBase.Instance.Players.FirstOrDefault(p => p.Input.PlayerIndex == PlayerIndex);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var p = GetPlayer();
            if (p == null)
            {
                IsVisible = false;
            }
            else
            {
                IsVisible = true;

                if (p.IsConnected)
                {
                    _connectController.IsVisible = false;

                    _playerName.Color = p.Color;
                    _playerName.ShadowColor = MaterialDesignColors.GetVariant(p.Color, MaterialThemeVariant.Hue900);
                    _playerName.ShadowSize = UiTheme.TextShadowSize;

                    if (p.IsAlive)
                    {
                        _pressAToPlay.IsVisible = false;
                    }
                    else
                    {
                        _pressAToPlay.IsVisible = true;
                    }
                }
                else
                {
                    _connectController.IsVisible = true;
                    _pressAToPlay.IsVisible = false;

                    _playerName.Color = UiTheme.TextDisabledColor;
                    _playerName.ShadowColor = UiTheme.TextDisabledShadowColor;
                    _playerName.ShadowSize = UiTheme.TextDisabledShadowSize;
                }

            }

            AlignChildren(Frame.HorizontalTextAlignment.Center);
        }
    }
}
