using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.NuclexGui.Visuals.Flat;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.UI.ButtonBar;
using PikaGames.Games.Core.UI.Text;

namespace PikaGames.Games.Core.UI.Dialog
{
    public class UiDialog : UiContainer
    {
        private UiTitle _title;

        protected UiContainer Content;

        private UiButtonBar _buttonBar;

        public override int Width => Container.Width - 100;
        public override int Height => Container.Height - 100;

        public UiDialog(string title) : base(GameBase.Instance.DialogContainer, 50, 50)
        {
            _title = new UiTitle(this, 25, 25, title);
            _buttonBar = new UiButtonBar(this, Width - 25, Height - 25);

            _buttonBar.AddButton(Buttons.B, "Back");
            _buttonBar.AddButton(Buttons.A, "Select");

            Content = new UiContainer(this, 25, _title.Height + 100);
            
            HasBackground = true;
            Background = UiThemeResources.DialogBackground;
            BackgroundShadow = UiThemeResources.DialogBackgroundShadow;
            BackgroundShadowSize = UiTheme.DialogBackgroundShadowSize;

            GameBase.Instance.SceneManager.CurrentScene.UiContainer.IsFocused = false;
            GameBase.Instance.DialogContainer.IsFocused = true;
        }

        public void Close()
        {
            GameBase.Instance.DialogContainer.IsFocused = false;
            GameBase.Instance.SceneManager.CurrentScene.UiContainer.IsFocused = true;
            Container.RemoveItem(this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!IsFocused)
                return;

            if (GameBase.Instance.Players.Any(p => p.Input.IsPressed(InputCommand.B)))
            {
                Close();
            }
        }
    }
}
