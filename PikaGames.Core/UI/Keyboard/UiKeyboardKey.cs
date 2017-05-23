using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.NuclexGui.Visuals.Flat;
using PikaGames.Games.Core.UI.Controls;
using PikaGames.Games.Core.Utils;

namespace PikaGames.Games.Core.UI.Keyboard
{
    public enum KeyboardKeyType
    {
        Type,
        Delete,
        Caps,
        CursorLeft,
        CursorRight,
        Done,
        Other
    }

    public class UiKeyboardKey : UiButton
    {
        public char Char
        {
            get { return _c; }
            set
            {
                _c = value;
                if(_keyboardKeyType == KeyboardKeyType.Type)
                    Label.Text = _c.ToString();
            }
        }

        public override int Width => Container.Width;
        public override int Height => Container.Height;

        private KeyboardKeyType _keyboardKeyType;
        private UiKeyboard _keyboard;
        private char _c;

        public UiKeyboardKey(UiKeyboard keyboard, KeyboardKeyType type, char c, string label) : base(keyboard, 0, 0, label)
        {
            _keyboard = keyboard;
            _keyboardKeyType = type;

            Char = c;
        }

        public UiKeyboardKey(UiKeyboard keyboard, char c, string label) : this(keyboard, KeyboardKeyType.Type, c, label) { }

        public UiKeyboardKey(UiKeyboard keyboard, char c) : this(keyboard, KeyboardKeyType.Type, c, c.ToString()) { }

        public UiKeyboardKey(UiKeyboard keyboard, KeyboardKeyType type, string label) : this(keyboard, type, '~', label) { }

        public UiKeyboardKey(UiKeyboard keyboard, string label) : this(keyboard, KeyboardKeyType.Other, '~', label) { }

        public UiKeyboardKey(UiKeyboard keyboard) : this(keyboard, KeyboardKeyType.Other, '~', "") { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            AlignChildren(Frame.HorizontalTextAlignment.Center);
            AlignChildren(Frame.VerticalTextAlignment.Center);
        }

        public override void Focus()
        {
            base.Focus();

            if (_keyboardKeyType == KeyboardKeyType.Type)
            {
                _keyboard.AddChar(Char);
            }
            else
            {
                _keyboard.KeyAction(_keyboardKeyType);
            }
        }
    }
}
