﻿using System;
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

namespace PikaGames.PaperCast.Scenes
{
    public class MainMenuScene : Scene
    {
        private float _splashInitialY = 0f;
        private Vector2 _splashPosition = Vector2.Zero;
        private string _splashText = "";

        private bool _inMenu = false;

        //private UiContainer _container;
        private UiMenu _menu;
        private UiButtonBar _buttonBar;

        public override void LoadContent()
        {
            base.LoadContent();

            var center = Game.ViewportAdapter.Center;
            _splashInitialY = Game.ViewportAdapter.ViewportHeight - center.Y/2;

            var font = Games.Core.Resources.Fonts.GameFont;

            var size = font.MeasureString(_splashText);
            _splashPosition = new Vector2(center.X - size.X, _splashInitialY - size.Y);
            
            //_container = new UiContainer();

            _menu = new UiMenu(UiContainer, center.X, center.Y);
            _menu.Alignment = Frame.HorizontalTextAlignment.Center;
            
            _menu.AddMenuItem("Play", () => Game.SceneManager.ChangeScene(((PaperCastGame)Game).GameMapScene));
            _menu.AddMenuItem("Main Menu", () => Game.Exit());

            _buttonBar = new UiButtonBar(UiContainer, (int)Game.VirtualSize.X - 25, (int)Game.VirtualSize.Y - 25);
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

                SetSplashText("Press Start!");
            }
            else
            {
                SetSplashText("Press A to join");
            }

            //_container.Update(gameTime);

            if (!_inMenu)
            {
                var delta = (float) gameTime.TotalGameTime.TotalMilliseconds / 10;
                _splashPosition.Y =
                    (float) MathUtils.SinInterpolation(_splashInitialY, _splashInitialY + 5, delta);
            }
        }

        private void SetSplashText(string text)
        {
            if (text == _splashText) return;

            _splashText = text;

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

           // _container.Draw(spriteBatch);

            if (!_inMenu)
            {
                spriteBatch.DrawString(Games.Core.Resources.Fonts.GameFont, _splashText,
                    _splashPosition + new Vector2(2, 2), UiTheme.TitleShadowColor, 0f, Vector2.Zero, 2f,
                    SpriteEffects.None, 0);
                spriteBatch.DrawString(Games.Core.Resources.Fonts.GameFont, _splashText, _splashPosition,
                    UiTheme.TitleColor, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
            }
            else
            {
                _menu.Draw(spriteBatch);
            }


            spriteBatch.End();
        }

    }
}
