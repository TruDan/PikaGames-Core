using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Scenes;

namespace PikaGames.PaperCast.Scenes
{
    public class MainMenuScene : Scene
    {



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            base.Draw(gameTime, spriteBatch, viewportAdapter);

            spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);

            var center = viewportAdapter.Center;
            var pos = new Vector2(center.X - Resources.Images.PaperCastLogo.Width / 2, center.Y / 2 - Resources.Images.PaperCastLogo.Height / 2);

            spriteBatch.Draw(Resources.Images.PaperCastLogo, pos, Color.White);

            spriteBatch.End();

        }

    }
}
