﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using PikaGames.Games.PaperCast.Managers;

namespace PikaGames.Core.Extensions
{
    static class SpriteBatchExtensions
    {
        public static void DrawTextWithShadow(this SpriteBatch spriteBatch, BitmapFont font, string text,
            Vector2 position, Color color, Color shadowColor)
        {
            spriteBatch.DrawString(font, text, position + Vector2.One, shadowColor);
            spriteBatch.DrawString(font, text, position, color);
        }

        public static void DrawTextWithShadow(this SpriteBatch spriteBatch, BitmapFont font, string text,
            Vector2 position, Color color)
        {
            spriteBatch.DrawTextWithShadow(font, text, position, color, Color.Black);
        }

        public static void DrawRightText(this SpriteBatch spriteBatch, BitmapFont font, string text,
            Vector2 position, Color color, Color? shadowColor = null)
        {
            var width = SceneManager.Instance.VirtualSize.X;
            var pos = new Vector2(width - font.MeasureString(text).Width - position.X, position.Y);
            if (shadowColor.HasValue)
                spriteBatch.DrawTextWithShadow(font, text, pos, color, shadowColor.Value);
            else
                spriteBatch.DrawString(font, text, pos, color);
        }
    }
}
