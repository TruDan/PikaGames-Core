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
using PikaGames.PaperCast;

namespace GameLauncher.Scenes
{
    public class MainMenuScene : Scene
    {
        private GameLauncherGame Launcher { get; }

        private float _splashInitialY = 0f;
        private Vector2 _splashPosition = Vector2.Zero;
        private string _splashText = "";
        private Color _splashColor = MaterialDesignColors.LightBlue500;
        private Color _splashShadowColor = MaterialDesignColors.LightBlue900;

        private bool _inMenu = false;

        private UiContainer _container;
        private UiMenu _menu;
        private UiButtonBar _buttonBar;

        public MainMenuScene(GameLauncherGame launcher)
        {
            Launcher = launcher;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            var center = Game.ViewportAdapter.Center;
            _splashInitialY = Game.ViewportAdapter.ViewportHeight - center.Y / 2;

            var font = PikaGames.Games.Core.Resources.Fonts.GameFont;

            var size = font.MeasureString(_splashText);
            _splashPosition = new Vector2(center.X - size.X, _splashInitialY - size.Y);

            _container = new UiContainer();

            _menu = new UiMenu(null, center.X, center.Y);
            _menu.Alignment = Frame.HorizontalTextAlignment.Center;

            _menu.AddMenuItem("Game Select", () => Game.SceneManager.ChangeScene(Launcher.GameSelectScene));
            _menu.AddMenuItem("Options", () => Game.SceneManager.ChangeScene(Launcher.OptionsMenuScene));
            _menu.AddMenuItem("Exit", () => Game.Exit());

            _buttonBar = new UiButtonBar(_container, (int)Game.VirtualSize.X - 25, (int)Game.VirtualSize.Y - 25);
            _buttonBar.AddButton(Buttons.A, "Select");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Game.Players.Any())
            {
                if (!_inMenu && Game.Players.Any(p => p.Input.IsPressed(InputCommand.A, InputCommand.Start)))
                {
                    _inMenu = true;
                }
                else if (_inMenu)
                {
                    _menu.Update(gameTime);
                }

                SetSplashText("Press Start!", MaterialDesignColors.Yellow500, MaterialDesignColors.Yellow900);
            }
            else
            {
                SetSplashText("Press A to join", MaterialDesignColors.Green500, MaterialDesignColors.Green900);
            }

            _container.Update(gameTime);

            if (!_inMenu)
            {
                var delta = (float)gameTime.TotalGameTime.TotalMilliseconds / 10;
                _splashPosition.Y =
                    (float)MathUtils.SinInterpolation(_splashInitialY, _splashInitialY + 5, delta);
            }
        }

        private void SetSplashText(string text, Color textColor, Color shadowColor)
        {
            if (text == _splashText && textColor == _splashColor && shadowColor == _splashShadowColor) return;

            _splashText = text;
            _splashColor = textColor;
            _splashShadowColor = shadowColor;

            var center = Game.ViewportAdapter.Center;
            var font = PikaGames.Games.Core.Resources.Fonts.GameFont;

            var size = font.MeasureString(_splashText);
            _splashPosition = new Vector2(center.X - size.X, _splashInitialY - size.Y);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            base.Draw(gameTime, spriteBatch, viewportAdapter);

            spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);

            var center = viewportAdapter.Center;
            var pos = new Vector2(center.X - Resources.Images.PikaGamesLogo.Width / 2, center.Y / 2 - Resources.Images.PikaGamesLogo.Height / 2);

            spriteBatch.Draw(Resources.Images.PikaGamesLogo, pos, Color.White);

            _container.Draw(spriteBatch);

            if (!_inMenu)
            {
                spriteBatch.DrawString(PikaGames.Games.Core.Resources.Fonts.GameFont, _splashText,
                    _splashPosition + new Vector2(2, 2), _splashShadowColor, 0f, Vector2.Zero, 2f,
                    SpriteEffects.None, 0);
                spriteBatch.DrawString(PikaGames.Games.Core.Resources.Fonts.GameFont, _splashText, _splashPosition,
                    _splashColor, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
            }
            else
            {
                _menu.Draw(spriteBatch);
            }


            spriteBatch.End();
        }

    }
}
