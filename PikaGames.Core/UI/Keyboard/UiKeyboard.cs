using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PikaGames.Games.Core.UI.Controls;
using PikaGames.Games.Core.UI.Grid;

namespace PikaGames.Games.Core.UI.Keyboard
{
    public class UiKeyboard : UiGridMenu
    {
        public event Action<string> OnValueChanged;

        public string Value { get; private set; } = string.Empty;
        public int CursorPos { get; private set; }

        public bool IsCaps { get; private set; }

        public int KeyWidth { get; } = 50;
        public int KeyHeight { get; } = 60;
        public int KeySpacing { get; } = 10;

        private readonly UiInput _input;

        public UiKeyboard(UiContainer container, UiInput input, int x, int y) : base(container, 6, 16)
        {
            X = x;
            Y = y;
            _input = input;
            Value = input.Value;
            CursorPos = Value.Length;

            InitKeys();
        }

        private void AddButton(int x, int y, KeyboardKeyType type, string label = "", int rowSpan = 1, int colSpan = 1)
        {
            AddItem(new UiKeyboardKey(this, type, label), x, y, rowSpan, colSpan);
        }

        private void AddKey(int x, int y, char key, string label, int rowSpan = 1, int colSpan = 1)
        {
            if(string.IsNullOrWhiteSpace(label))
                label = key.ToString();
            
            AddItem(new UiKeyboardKey(this, key, label), x, y, rowSpan, colSpan);
        }

        private void AddKey(int x, int y, char key, int rowSpan = 1, int colSpan = 1)
        {
            AddItem(new UiKeyboardKey(this, key), x, y, rowSpan, colSpan);
        }

        private void InitKeys()
        {
            int x = 0, y = 0;

            AddButton(x, y, KeyboardKeyType.CursorLeft, "Cursor", rowSpan: 2, colSpan: 3);
            x += 3;
            AddKey(x++, y, '1');
            AddKey(x++, y, '2');
            AddKey(x++, y, '3');
            AddKey(x++, y, '4');
            AddKey(x++, y, '5');
            AddKey(x++, y, '6');
            AddKey(x++, y, '7');
            AddKey(x++, y, '8');
            AddKey(x++, y, '9');
            AddKey(x++, y, '0');
            AddButton(x, y, KeyboardKeyType.CursorRight,  "Cursor", rowSpan: 2, colSpan: 3);


            x = 0; y++;
            x += 3;
            AddKey(x++, y, 'Q');
            AddKey(x++, y, 'W');
            AddKey(x++, y, 'E');
            AddKey(x++, y, 'R');
            AddKey(x++, y, 'T');
            AddKey(x++, y, 'Y');
            AddKey(x++, y, 'U');
            AddKey(x++, y, 'I');
            AddKey(x++, y, 'O');
            AddKey(x,   y, 'P');


            x = 0; y++;
            AddButton(x, y, KeyboardKeyType.CursorLeft, "Symbols", rowSpan: 2, colSpan: 3);
            x += 3;
            AddKey(x++, y, 'A');
            AddKey(x++, y, 'S');
            AddKey(x++, y, 'D');
            AddKey(x++, y, 'F');
            AddKey(x++, y, 'G');
            AddKey(x++, y, 'H');
            AddKey(x++, y, 'J');
            AddKey(x++, y, 'K');
            AddKey(x++, y, 'L');
            AddKey(x++, y, '@');
            AddButton(x, y, KeyboardKeyType.CursorLeft, "Accents", rowSpan: 2, colSpan: 3);

            x = 0; y++;
            x += 3;
            AddKey(x++, y, 'Z');
            AddKey(x++, y, 'X');
            AddKey(x++, y, 'C');
            AddKey(x++, y, 'V');
            AddKey(x++, y, 'B');
            AddKey(x++, y, 'N');
            AddKey(x++, y, 'M');
            AddKey(x++, y, '-');
            AddKey(x++, y, '_');
            AddKey(x,   y, '.');

            x = 0; y++;
            AddButton(x++, y, KeyboardKeyType.Caps, "Caps", rowSpan: 2, colSpan: 3);
            x += 2;
            AddButton(x++, y, KeyboardKeyType.Delete, "Backspace", colSpan: 5);
            x += 4;
            AddKey(x++, y, ' ', "Space", colSpan: 5);
            x += 4;
            AddButton(x, y, KeyboardKeyType.Done, "Done", rowSpan: 2, colSpan: 3);
        }

        internal void AddChar(char c)
        {
            var str = c.ToString();

            Value = Value.Substring(0, CursorPos) + str + Value.Substring(CursorPos, Value.Length - CursorPos);

            CursorPos = MathHelper.Clamp(CursorPos + 1, 0, Value.Length);

            OnValueChanged?.Invoke(Value);
            UpdateInput();
        }

        private void UpdateInput()
        {
            _input.CursorPos = CursorPos;
            _input.Value = Value;
        }

        internal void KeyAction(KeyboardKeyType keyType)
        {
            if (keyType == KeyboardKeyType.Delete)
            {
                Value = Value.Substring(0, CursorPos-1) + Value.Substring(CursorPos, Value.Length - CursorPos);
                CursorPos = MathHelper.Clamp(CursorPos - 1, 0, Value.Length);
                OnValueChanged?.Invoke(Value);
            }
            else if (keyType == KeyboardKeyType.Caps)
            {
                IsCaps = !IsCaps;

                foreach (var key in Children)
                {
                    var k = (UiKeyboardKey) ((UiGridItem) key).Child;
                    char c;
                    char.TryParse(IsCaps ? k.Char.ToString().ToUpperInvariant() : k.Char.ToString().ToLowerInvariant(), out c);
                    k.Char = c;
                }

            }
            else if (keyType == KeyboardKeyType.CursorLeft)
            {
                CursorPos = MathHelper.Clamp(CursorPos - 1, 0, Value.Length);
            }
            else if (keyType == KeyboardKeyType.CursorRight)
            {
                CursorPos = MathHelper.Clamp(CursorPos + 1, 0, Value.Length);
            }

            UpdateInput();
        }
        
    }
}
