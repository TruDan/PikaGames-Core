using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.NuclexGui.Visuals.Flat;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.Scenes;
using PikaGames.Games.Core.UI;
using PikaGames.Games.Core.UI.ButtonBar;
using PikaGames.Games.Core.UI.Menu;
using PikaGames.Games.Core.UI.Text;

namespace PikaGames.Games.RaceCast.Scenes
{
	public class GameSetupScene : Scene
	{
		private UiContainer _container;
		private UiButtonBar _buttonBar;
		private UiText _title;
		private UiMenu _menu;


		public override void LoadContent()
		{
			base.LoadContent();

			_container = new UiContainer();

			_title = new UiTitle(_container, 50, 50, "Game Setup");

			_menu = new UiMenu(_container, 50, 50 + _title.Height + 50);

			_menu.AddMenuItem("Start Game!", () => Game.SceneManager.ChangeScene(new RaceScene(GetGameSetup())));

			_buttonBar = new UiButtonBar(_container, (int)Game.VirtualSize.X - 25, (int)Game.VirtualSize.Y - 25);
			_buttonBar.AddButton(Buttons.A, "Select");
			_buttonBar.AddButton(Buttons.B, "Back");
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			_container.Update(gameTime);

			if (Game.Players.ToArray().Any(x => x.Input.IsDown(InputCommand.B)))
			{
				Game.Exit();
			}
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
		{
			base.Draw(gameTime, spriteBatch, viewportAdapter);

			spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);

			//var center = viewportAdapter.Center;
			//var pos = new Vector2(center.X - Resources.Images.RaceCastLogo.Width / 2, center.Y / 2 - Resources.Images.RaceCastLogo.Height / 2);

			//spriteBatch.Draw(Resources.Images.RaceCastLogo, pos, Color.White);

			_container.Draw(spriteBatch);

			spriteBatch.End();
		}

		private GameSetup GetGameSetup()
		{
			return new GameSetup(); //TODO: make this hold all selected game options (Selected map etc)
		}
	}
}
