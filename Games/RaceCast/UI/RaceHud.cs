using System;
using Microsoft.Xna.Framework;
using PikaGames.Games.Core.UI;
using PikaGames.Games.Core.UI.Text;
using PikaGames.Games.Core.Utils;
using PikaGames.Games.RaceCast.Util;

namespace PikaGames.Games.RaceCast.UI
{
	public class RaceHud : UiContainer
	{
		private UiContainer _hud;
		private UiText _playerName;
		private UiText _playerSpeed;


		private RaceCastPlayer Player { get; }
		public RaceHud(RaceCastPlayer player, int x, int y) : base(x, y)
		{
			Player = player;

			_hud = new UiContainer(this, 25, 25);
			_playerName = new UiTitle(_hud, 0, 0, player.ToString());
			_playerName.ShadowSize = UiTheme.TextShadowSize;
			_playerName.Scale = 1.2f;

			_playerSpeed = new UiText(_hud, 0, _playerName.Y + _playerName.Height + 15, "0 KPH");
		}

		public override void Update(GameTime gameTime)
		{
			_playerSpeed.Text = $"{(int)((Player.Vehicle.GetVelocity().Magnitude() / 10) * 3.6)} KPH";
		}
	}
}
