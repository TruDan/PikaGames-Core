using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientLauncher.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.NuclexGui;
using MonoGame.Extended.NuclexGui.Controls.Desktop;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Scenes;

namespace ClientLauncher.Scenes
{
    public class MainMenuScene : Scene
    {
        private SettingsWindow _settingsWindow;

        private Vector2 _logoPosition;

        public MainMenuScene()
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            var button = new GuiButtonControl
            {
                Name = "button",
                Bounds = new UniRectangle(new UniScalar(0.5f, -100), new UniScalar(0.5f, -15), new UniScalar(0f, 200), new UniScalar(0f, 30)),
                Text = "Play"
            };

            var button2 = new GuiButtonControl
            {
                Name = "button2",
                Bounds = new UniRectangle(new UniScalar(0.5f, -100), new UniScalar(0.5f, 25), new UniScalar(0f, 200), new UniScalar(0f, 30)),
                Text = "Settings"
            };

            ((ClientLauncherGame) Game).GuiManager.Screen.Desktop.Children.Add(button);
            ((ClientLauncherGame)Game).GuiManager.Screen.Desktop.Children.Add(button2);


            _settingsWindow = new SettingsWindow();

            button2.Pressed += (sender, args) =>
            {
                if (((ClientLauncherGame) Game).GuiManager.Screen.Desktop.Children.Any(i => i.Name == "settingsWindow"))
                    return;

                ((ClientLauncherGame) Game).GuiManager.Screen.Desktop.Children.Add(_settingsWindow);
                _settingsWindow.BringToFront();
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _logoPosition = new Vector2((Game.VirtualSize.X - PikaGames.Games.Core.Resources.Images.Logo.Width)/2, (Game.VirtualSize.Y - PikaGames.Games.Core.Resources.Images.Logo.Height)/3);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            base.Draw(gameTime, spriteBatch, viewportAdapter);

            spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(PikaGames.Games.Core.Resources.Images.Logo, _logoPosition, Color.White);
            spriteBatch.End();
        }
    }
}
