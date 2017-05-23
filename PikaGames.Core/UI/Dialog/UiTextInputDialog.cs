using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended.NuclexGui.Visuals.Flat;
using PikaGames.Games.Core.UI.Controls;
using PikaGames.Games.Core.UI.Keyboard;
using PikaGames.Games.Core.Utils;

namespace PikaGames.Games.Core.UI.Dialog
{
    public class UiTextInputDialog : UiDialog
    {
        private Action<string> _callbackAction;

        private UiInput _inputBox;
        private UiKeyboard _keyboard;

        public UiTextInputDialog(string title, Action<string> callbackAction) : base(title)
        {
            _callbackAction = callbackAction;
            
            _inputBox = new UiInput(Content, 0, 0, "");

            _keyboard = new UiKeyboard(Content, _inputBox, 0, _inputBox.Height + 50);
            _keyboard.IsFocused = true;

            _inputBox.Width = _keyboard.Width;
        }

        private float _cursorAlpha;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Content.Y = (Height + 100 - Content.Height) / 2;
            Content.X = (Width - 50 - Content.Width) / 2;

        }
    }
}
