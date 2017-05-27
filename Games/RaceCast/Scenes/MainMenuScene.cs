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
using PikaGames.Games.Core.Utils;

namespace PikaGames.Games.RaceCast.Scenes
{

	public class MainMenuScene : Scene
	{
		private UiContainer _container;
		private UiMenu _menu;
		private UiButtonBar _buttonBar;

		public override void LoadContent()
		{
			base.LoadContent();

			var center = Game.ViewportAdapter.Center;

			_container = new UiContainer();

			_menu = new UiMenu(null, center.X, center.Y);
			_menu.Alignment = Frame.HorizontalTextAlignment.Center;

			_menu.AddMenuItem("Play", () => Game.SceneManager.ChangeScene(((RaceCastGame)Game).GameSetupScene));
			_menu.AddMenuItem("Main Menu", () => Game.Exit());

			_buttonBar = new UiButtonBar(_container, (int)Game.VirtualSize.X - 25, (int)Game.VirtualSize.Y - 25);
			_buttonBar.AddButton(Buttons.A, "Select");
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			_menu.Update(gameTime);
			_container.Update(gameTime);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
		{
			base.Draw(gameTime, spriteBatch, viewportAdapter);

			spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);

			//var center = viewportAdapter.Center;
			//var pos = new Vector2(center.X - Resources.Images.PaperCastLogo.Width / 2, center.Y / 2 - Resources.Images.PaperCastLogo.Height / 2);

			//spriteBatch.Draw(Resources.Images.PaperCastLogo, pos, Color.White);

			_container.Draw(spriteBatch);

			_menu.Draw(spriteBatch);

			spriteBatch.End();
		}

	}
}
