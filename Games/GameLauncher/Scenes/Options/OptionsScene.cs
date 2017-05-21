using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.Scenes;
using PikaGames.Games.Core.UI;
using PikaGames.Games.Core.UI.ButtonBar;
using PikaGames.Games.Core.UI.Text;
using PikaGames.Games.Core.Utils;

namespace GameLauncher.Scenes.Options
{
    public abstract class OptionsScene : Scene
    {
        protected OptionsMenuScene OptionsMenu { get; }
        
        protected UiContainer Container { get; private set; }

        public string Title { get; }
        private UiText _title;

        private UiButtonBar _buttonBar;

        protected OptionsScene(OptionsMenuScene optionsMenu, string title)
        {
            OptionsMenu = optionsMenu;
            Title = title;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _title = new UiTitle(null, 50, 50, Title);

            Container = new UiContainer(50, 100 + _title.Height);

            _buttonBar = new UiButtonBar(Container, (int)Game.VirtualSize.X - 25 - (int)Container.Position.X, (int)Game.VirtualSize.Y - 25 - (int)Container.Position.Y);
            _buttonBar.AddButton(Buttons.A, "Select");
            _buttonBar.AddButton(Buttons.B, "Back");
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
