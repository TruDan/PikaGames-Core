using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;

namespace ClientLauncher.UI
{
    public class PikaButton
    {
        private const int BtnTextureLeftPosX = 0;
        private const int BtnTextureFillPosX = 36;
        private const int BtnTextureRightPosX = 40;


        public Vector2 Position { get; set; } = Vector2.Zero;

        public string Text { get; set; } = "Play";

        public int MinWidth { get; set; } = 200;
        public int Padding { get; set; } = 50;

        public int Width { get; private set; } = 200;
        public int Height { get; private set; } = 200;

        public bool HasEars { get; set; } = false;

        public int Scale { get; set; } = 2;

        private static readonly Rectangle BtnEarsTextureLeft = new Rectangle(0, 0, 36, 60);
        private static readonly Rectangle BtnEarsTextureFill = new Rectangle(36, 0, 4, 60);
        private static readonly Rectangle BtnEarsTextureRight = new Rectangle(40, 0, 40, 60);


        private static readonly Rectangle BtnTextureLeft = new Rectangle(0, 60, 12, 60);
        private static readonly Rectangle BtnTextureFill = new Rectangle(12, 60, 4, 60);
        private static readonly Rectangle BtnTextureRight = new Rectangle(16, 60, 16, 60);

        private Vector2 TextPosition;
        private Vector2 TextSize;
        private Rectangle LeftRectangle;
        private Rectangle FillRectangle;
        private Rectangle RightRectangle;

        public PikaButton()
        {

        }

        public void Update(GameTime gameTime)
        {
            TextSize = PikaGames.Games.Core.Resources.Fonts.GameFont.MeasureString(Text) * Scale;

            Width = (int) Math.Max(MinWidth, TextSize.X + 2*Padding) * Scale;
            Height = BtnEarsTextureLeft.Height * Scale;
            

            if (HasEars)
            {
                TextPosition = new Vector2(Position.X + (Width - 4*Scale - TextSize.X)/2, Position.Y + (16 * Scale) + ((40 * Scale) - TextSize.Y)/2);

                LeftRectangle = new Rectangle((int) Position.X, (int) Position.Y, BtnEarsTextureLeft.Width * Scale,
                    Height);
                FillRectangle = new Rectangle((int) Position.X + (BtnEarsTextureLeft.Width * Scale), (int) Position.Y,
                    Width - (BtnEarsTextureLeft.Width + BtnEarsTextureRight.Width) * Scale, Height);
                RightRectangle = new Rectangle((int) Position.X + Width - (BtnEarsTextureRight.Width * Scale),
                    (int) Position.Y, BtnEarsTextureRight.Width * Scale, Height);
            }
            else
            {
                TextPosition = new Vector2(Position.X + (Width - 4 * Scale - TextSize.X) / 2, Position.Y + (8 * Scale) + (TextSize.Y) / 2);

                LeftRectangle = new Rectangle((int)Position.X, (int)Position.Y, BtnTextureLeft.Width * Scale,
                    Height);
                FillRectangle = new Rectangle((int)Position.X + (BtnTextureLeft.Width * Scale), (int)Position.Y,
                    Width - (BtnTextureLeft.Width + BtnTextureRight.Width) * Scale, Height);
                RightRectangle = new Rectangle((int)Position.X + Width - (BtnTextureRight.Width * Scale),
                    (int)Position.Y, BtnTextureRight.Width * Scale, Height);
            }
        }

    public void Draw(SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp, depthStencilState: DepthStencilState.None, rasterizerState: RasterizerState.CullNone, sortMode: SpriteSortMode.Immediate);

            if (HasEars)
            {
                spriteBatch.Draw(PikaGames.Games.Core.Resources.Images.Buttons_Pika, LeftRectangle, BtnEarsTextureLeft,
                    Color.White);
                spriteBatch.Draw(PikaGames.Games.Core.Resources.Images.Buttons_Pika, FillRectangle, BtnEarsTextureFill,
                    Color.White);
                spriteBatch.Draw(PikaGames.Games.Core.Resources.Images.Buttons_Pika, RightRectangle,
                    BtnEarsTextureRight, Color.White);
            }
            else
            {
                spriteBatch.Draw(PikaGames.Games.Core.Resources.Images.Buttons_Pika, LeftRectangle, BtnTextureLeft,
                    Color.White);
                spriteBatch.Draw(PikaGames.Games.Core.Resources.Images.Buttons_Pika, FillRectangle, BtnTextureFill,
                    Color.White);
                spriteBatch.Draw(PikaGames.Games.Core.Resources.Images.Buttons_Pika, RightRectangle,
                    BtnTextureRight, Color.White);
            }

//            spriteBatch.DrawString(PikaGames.Games.Core.Resources.Fonts.GameFont, Text, TextPosition, Color.Black  * 0.5f, 0, Vector2.Zero, new Vector2(Scale), SpriteEffects.None, 0);
            spriteBatch.DrawString(PikaGames.Games.Core.Resources.Fonts.GameFont, Text, TextPosition + new Vector2(Scale, Scale), Color.Black*0.75f, 0, Vector2.Zero, new Vector2(Scale), SpriteEffects.None, 0);

            spriteBatch.DrawString(PikaGames.Games.Core.Resources.Fonts.GameFont, Text, TextPosition, Color.White, 0, Vector2.Zero, new Vector2(Scale), SpriteEffects.None, 0);
            spriteBatch.End();
        }

    }
}
