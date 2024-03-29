﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PikaGames.Games.Core.UI.Text
{
    public class UiText : UiItem
    {
        public SpriteFont Font { get; set; } = Resources.Fonts.GameFont;

        private string _text;
        public string Text
        {
            get => _text;
            set {
                _text = value;
                UpdateSize();
            }
        }

        public Color Color { get; set; } = UiTheme.TextColor;
        public Color ShadowColor { get; set; } = UiTheme.TextShadowColor;

        public int ShadowSize { get; set; } = UiTheme.TextShadowSize;

        private float _scale = 1f;
        public float Scale
        {
            get => _scale;
            set {
                _scale = value;
                UpdateSize();
            }
        }

        public override int Width => (int) _textSize.X;
        public override int Height => (int) _textSize.Y;

        private Vector2 _textSize;

        public UiText(UiContainer container, int x, int y, string text, Color color, Color shadowColor,
            int shadowSize = 5) : this(container, x, y, text, color)
        {
            ShadowColor = shadowColor;
            ShadowSize = shadowSize;
        }

        public UiText(UiContainer container, int x, int y, string text, Color color) : this(container, x, y, text)
        {
            Color = color;
        }

        public UiText(UiContainer container, int x, int y, string text) : base(container, x, y)
        {
            Text = text;

            UpdateSize();
        }

        private void UpdateSize()
        {
            _textSize = Font.MeasureString(Text) * Scale;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (ShadowSize > 0)
            {
                for (int i = 0; i < ShadowSize; i++)
                {
                    spriteBatch.DrawString(Font, Text, Position + new Vector2(i, i), ShadowColor, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
                }
            }

            spriteBatch.DrawString(Font, Text, Position, Color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
