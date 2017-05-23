using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Sound;

namespace PikaGames.Games.Core.Scenes
{
    public class SplashScene : Scene
    {
        enum Phase
        {
            FadeIn,
            Display,
            FadeOut
        }

        private const float FadeInMs = 2000f;
        private const float DisplayMs = 2500f;
        private const float FadeOutMs = 500f;

        private Texture2D _pikaGames;

        private Phase _phase = Phase.FadeIn;

        private float _alpha = 0f;
        private DateTime _fadeOutTime;
        
        public override void LoadContent()
        {
            base.LoadContent();

            _pikaGames = Resources.Images.Splash_PikaGames;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var dt = gameTime.ElapsedGameTime.Milliseconds;

            if (_phase == Phase.FadeIn)
            {
                if (_alpha == 0)
                {
                    Game.SoundManager.Play(Resources.Sfx.Splash_PikaGames);
                }

                _alpha += dt / FadeInMs;

                if (_alpha >= 1f)
                {
                    _phase = Phase.Display;
                    _fadeOutTime = DateTime.UtcNow + TimeSpan.FromMilliseconds(DisplayMs);
                }
            }
            else if (_phase == Phase.Display)
            {
                _alpha = 1f;

                if (DateTime.UtcNow >= _fadeOutTime)
                {
                    _phase = Phase.FadeOut;
                }
            }
            else if (_phase == Phase.FadeOut)
            {
                _alpha -= dt / FadeOutMs;

                if (_alpha <= 0)
                {
                    SceneManager.ChangeScene(SceneManager.DefaultScene);
                }
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            base.Draw(gameTime, spriteBatch, viewportAdapter);

            spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_pikaGames, Game.ViewportAdapter.BoundingRectangle, Color.White * _alpha);
            spriteBatch.End();
        }
    }
}
