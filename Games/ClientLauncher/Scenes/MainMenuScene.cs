using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Scenes;

namespace ClientLauncher.Scenes
{
    public class MainMenuScene : Scene
    {

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
