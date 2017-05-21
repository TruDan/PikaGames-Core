using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.Scenes;
using PikaGames.Games.Core.Utils;

namespace PikaGames.PaperCast.Scenes
{
    public class MainMenuScene : Scene
    {
        private float _splashInitialY = 0f;
        private Vector2 _splashPosition = Vector2.Zero;
        private string _splashText = "";
        private Color _splashColor = MaterialDesignColors.LightBlue500;
        private Color _splashShadowColor = MaterialDesignColors.LightBlue900;

        private bool _inMenu = false;
        private int _menuSelectedIndex = 0;
        private Vector2 _menuPosition;
        private string[] _menu = new string[]
        {
            "New Game",
            "Options",
            "Exit"
        };

        public override void LoadContent()
        {
            base.LoadContent();

            var center = Game.ViewportAdapter.Center;
            _splashInitialY = Game.ViewportAdapter.ViewportHeight - center.Y/2;

            var font = Games.Core.Resources.Fonts.GameFont;

            var size = font.MeasureString(_splashText);
            _splashPosition = new Vector2(center.X - size.X, _splashInitialY - size.Y);

            var menuSize = _menu.Select(m => font.MeasureString(m).X).OrderByDescending(m => m).FirstOrDefault();

            _menuPosition = new Vector2(center.X - menuSize, center.Y);
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
                    if (Game.Players.Any(p => p.Input.IsPressed(InputCommand.Up)))
                    {
                        if (_menuSelectedIndex == 0) _menuSelectedIndex = _menu.Length - 1;
                        else _menuSelectedIndex--;

                        Game.SoundManager.Play(Resources.Sfx.SpaceMorph);
                    }
                    else if (Game.Players.Any(p => p.Input.IsPressed(InputCommand.Down)))
                    {
                        _menuSelectedIndex++;
                        if (_menuSelectedIndex == _menu.Length) _menuSelectedIndex = 0;

                        Game.SoundManager.Play(Resources.Sfx.SpaceMorph);
                    }
                    else if (Game.Players.Any(p => p.Input.IsPressed(InputCommand.A)))
                    {
                        // Start Game
                        Game.SceneManager.ChangeScene(((PaperCastGame) Game).GameMapScene);
                    }
                }

                SetSplashText("Press Start!", MaterialDesignColors.Yellow500, MaterialDesignColors.Yellow900);
            }
            else
            {
                SetSplashText("Press A to join", MaterialDesignColors.Green500, MaterialDesignColors.Green900);
            }

            if (!_inMenu)
            {
                var delta = (float) gameTime.TotalGameTime.TotalMilliseconds / 10;
                _splashPosition.Y =
                    (float) MathUtils.SinInterpolation(_splashInitialY, _splashInitialY + 5, delta);
            }
        }

        private void SetSplashText(string text, Color textColor, Color shadowColor)
        {
            if (text == _splashText && textColor == _splashColor && shadowColor == _splashShadowColor) return;

            _splashText = text;
            _splashColor = textColor;
            _splashShadowColor = shadowColor;

            var center = Game.ViewportAdapter.Center;
            var font = Games.Core.Resources.Fonts.GameFont;

            var size = font.MeasureString(_splashText);
            _splashPosition = new Vector2(center.X - size.X, _splashInitialY - size.Y);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            base.Draw(gameTime, spriteBatch, viewportAdapter);

            spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);

            var center = viewportAdapter.Center;
            var pos = new Vector2(center.X - Resources.Images.PaperCastLogo.Width / 2, center.Y / 2 - Resources.Images.PaperCastLogo.Height / 2);

            spriteBatch.Draw(Resources.Images.PaperCastLogo, pos, Color.White);

            if (!_inMenu)
            {
                spriteBatch.DrawString(Games.Core.Resources.Fonts.GameFont, _splashText,
                    _splashPosition + new Vector2(2, 2), _splashShadowColor, 0f, Vector2.Zero, 2f,
                    SpriteEffects.None, 0);
                spriteBatch.DrawString(Games.Core.Resources.Fonts.GameFont, _splashText, _splashPosition,
                    _splashColor, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
            }
            else
            {
                for (int i = 0; i < _menu.Length; i++)
                {
                    Color color = MaterialDesignColors.LightBlue400;
                    Color shadow = MaterialDesignColors.LightBlue900;

                    if (i == _menuSelectedIndex)
                    {
                        color = MaterialDesignColors.LightBlue50;
                        shadow = MaterialDesignColors.LightBlue700;
                    }

                    for (int j = 0; j < (i == _menuSelectedIndex ? 4 : 6); j++)
                    {
                        spriteBatch.DrawString(Games.Core.Resources.Fonts.GameFont, _menu[i], new Vector2(_menuPosition.X + j, _menuPosition.Y + 50 * i - j), shadow, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
                    }


                    spriteBatch.DrawString(Games.Core.Resources.Fonts.GameFont, _menu[i], new Vector2(_menuPosition.X, _menuPosition.Y + 50*i), color, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
                }
            }

            spriteBatch.End();
        }

    }
}
