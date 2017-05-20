using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;

namespace PikaGames.Games.Core.Scenes
{
    public sealed class SplashScene : Scene
    {
        private int _duration;

        public override void LoadContent()
        {
            base.LoadContent();


        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (gameTime.TotalGameTime > TimeSpan.FromSeconds(1))
            {
                SceneManager.ChangeScene(SceneManager.DefaultScene);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            base.Draw(gameTime, spriteBatch, viewportAdapter);

            spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);

            var center = viewportAdapter.Center;
            var pos = new Vector2(center.X - Resources.Image.Logo.Width/2, center.Y/2 - Resources.Image.Logo.Height/2);

            spriteBatch.Draw(Resources.Image.Logo, pos, Color.White);

            spriteBatch.End();

        }
    }
}
