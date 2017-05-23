using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.UI.Menu;
using PikaGames.Games.Core.UI.Text;
using PikaGames.Games.Core.Utils;

namespace PikaGames.Games.Core.UI.Controls
{
    public class UiInput : UiControl
    {
        public string Value
        {
            get => Label.Text;
            set => Label.Text = value;
        }

        public override int Width { get; set; } = 200;
        public override int Height { get; set; } = 50;
        
        public int CursorPos { get; set; }

        private readonly Texture2D _cursor;
        private int _cursorX;
        private float _cursorAlpha;

        public UiInput(UiContainer container, int x, int y, string label) : base(container, x, y, label)
        {
            _cursor = UiThemeResources.ControlInputCursor;

            Label.X = 5;
            Label.ShadowSize = 0;
            Label.Color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Label.Y = Height/2 - Label.Height/2;

            if (IsFocused)
            {
                var delta = (float)gameTime.TotalGameTime.TotalMilliseconds / 2;
                _cursorAlpha = (float)MathUtils.SinInterpolation(0f, 0.5f, delta)*2;

                string before = Value.Substring(0, CursorPos);
                if (string.IsNullOrEmpty(before))
                    _cursorX = 0;
                else
                    _cursorX = (int)Label.Position.X + (int)Resources.Fonts.GameFont.MeasureString(before).X;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(_cursor, new Rectangle(_cursorX + 1, (int)Label.Position.Y, 1, Label.Height), Color.White * _cursorAlpha);
        }
    }
}
