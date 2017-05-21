using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.Scenes;
using PikaGames.Games.Core.UI;
using PikaGames.Games.Core.Utils;

namespace PikaGames.PaperCast.Scenes.Options
{
    public abstract class OptionsScene : Scene
    {
        protected OptionsMenuScene OptionsMenu { get; }
        
        protected UiContainer Container { get; private set; }

        public string Title { get; }
        private UiText _title;

        protected OptionsScene(OptionsMenuScene optionsMenu, string title)
        {
            OptionsMenu = optionsMenu;
            Title = title;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _title = new UiText(null, 50, 50, Title, MaterialDesignColors.Amber500, MaterialDesignColors.Amber900);
            _title.Scale = 4f;

            Container = new UiContainer(50, 100 + _title.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Game.Players.Any(p => p.Input.IsPressed(InputCommand.B)))
            {
                Game.SceneManager.Previous();
            }
            else
            {
                _title.Update(gameTime);
                Container.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            base.Draw(gameTime, spriteBatch, viewportAdapter);
            
            spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);

            _title.Draw(spriteBatch);
            Container.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
