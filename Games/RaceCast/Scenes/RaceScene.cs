using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Scenes;
using PikaGames.Games.Core.Utils;
using PikaGames.Games.RaceCast.UI;

namespace PikaGames.Games.RaceCast.Scenes
{
	public class RaceScene : Scene
	{
		public Level Level { get; private set; }
		private Camera2D GameCamera { get; set; }
		private RaceHud[] Huds { get; }

		public RaceScene(GameSetup setup)
		{
			Huds = new RaceHud[4];
		}

		protected override void Initialise()
		{
			base.Initialise();
			Level = new Level((RaceCastGame) Game);
			GameCamera = new Camera2D(Game.ViewportAdapter);

			var players = Game.Players.ToArray();
			for (int i = 0; i < players.Length;  i++)
			{
				var player = players[i] as RaceCastPlayer;
				if (player == null)
				{
					Huds[i] = null;
					continue;
				}
				
				Huds[i] = new RaceHud(player, (Game.ViewportAdapter.ViewportWidth / 4) * i, Game.ViewportAdapter.ViewportHeight - 100);
				player.Init(Level);
			}
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
		{
			base.Draw(gameTime, spriteBatch, viewportAdapter);
			Level.Draw(spriteBatch, gameTime, GameCamera);

			spriteBatch.Begin();
			foreach (var hud in Huds)
			{
				if (hud != null)
					hud.Draw(spriteBatch);
			}
			spriteBatch.End();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			UpdateCamera();

			foreach (var hud in Huds)
			{
				if (hud != null)
					hud.Update(gameTime);
			}
		}

		private void UpdateCamera()
		{
			GameCamera.LookAtMultiple(new Vector2(Level.PixelWidth, Level.PixelHeight), 100, Game.Players.Where(p => p.IsAlive).Select(p => p.Position).ToArray());
		}
	}
}
