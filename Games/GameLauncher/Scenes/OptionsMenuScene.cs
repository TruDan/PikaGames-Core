using System.Linq;
using GameLauncher.Scenes.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.Scenes;
using PikaGames.Games.Core.UI;
using PikaGames.Games.Core.UI.ButtonBar;
using PikaGames.Games.Core.UI.Menu;
using PikaGames.Games.Core.UI.Text;
using PikaGames.Games.Core.Utils;

namespace GameLauncher.Scenes
{
    public class OptionsMenuScene : Scene
    {
        private Scene _previousScene;
        
        private UiText _title;

        private UiMenu _menu;
        private UiButtonBar _buttonBar;

        private readonly VideoOptionsScene _videoOptionsScene;
        private readonly AudioOptionsScene _audioOptionsScene;
        private readonly ControlsOptionsScene _controlsOptionsScene;

        public OptionsMenuScene()
        {
            _videoOptionsScene = new VideoOptionsScene(this);
            _audioOptionsScene = new AudioOptionsScene(this);
            _controlsOptionsScene = new ControlsOptionsScene(this);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            if(_previousScene == null)
                _previousScene = SceneManager.PreviousScene;
            
            _title = new UiTitle(UiContainer, 50, 50, "Options");
            
            _menu = new UiMenu(UiContainer, 50, 50 + _title.Height + 50);
            
            _menu.AddMenuItem("Video", () => Game.SceneManager.ChangeScene(_videoOptionsScene));
            _menu.AddMenuItem("Audio", () => Game.SceneManager.ChangeScene(_audioOptionsScene));
            _menu.AddMenuItem("Controls", () => Game.SceneManager.ChangeScene(_controlsOptionsScene));


            _buttonBar = new UiButtonBar(UiContainer, (int)Game.VirtualSize.X - 25, (int)Game.VirtualSize.Y - 25);
            _buttonBar.AddButton(Buttons.A, "Select");
            _buttonBar.AddButton(Buttons.B, "Back");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Game.Players.Any(p => p.Input.IsPressed(InputCommand.B)))
            {
                Game.SceneManager.ChangeScene(_previousScene);  
            }
        }
    }
}
