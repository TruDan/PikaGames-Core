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
using PikaGames.Games.Core.UI.PlayerBar;
using PikaGames.Games.Core.Utils;
using PikaGames.PaperCast;

namespace GameLauncher.Scenes
{
    public class MainMenuScene : Scene
    {
        private GameLauncherGame Launcher { get; }
        
        private UiMenu _menu;

        private UiPlayerBar _playerBar;

        public MainMenuScene(GameLauncherGame launcher)
        {
            Launcher = launcher;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            var center = Game.ViewportAdapter.Center;

            _menu = new UiMenu(UiContainer, center.X, center.Y);
            _menu.Alignment = Frame.HorizontalTextAlignment.Center;

            _menu.AddMenuItem("Game Select", () => Game.SceneManager.ChangeScene(Launcher.GameSelectScene));
            _menu.AddMenuItem("Options", () => Game.SceneManager.ChangeScene(Launcher.OptionsMenuScene));
            _menu.AddMenuItem("Debug", () => Game.SceneManager.ChangeScene(new DebugScene()));
            _menu.AddMenuItem("Exit", () => Game.Exit());

            _playerBar = new UiPlayerBar(UiContainer);

            Game.SoundManager.PlayBackground(Resources.Music.Metropolis);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            base.Draw(gameTime, spriteBatch, viewportAdapter);

            spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);

            var center = viewportAdapter.Center;
            var pos = new Vector2(center.X - PikaGames.Games.Core.Resources.Images.Logo.Width / 2, center.Y / 2 - PikaGames.Games.Core.Resources.Images.Logo.Height / 2);

            spriteBatch.Draw(PikaGames.Games.Core.Resources.Images.Logo, pos, Color.White);
            
            spriteBatch.End();
        }

    }
}
