using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PikaGames.Games.Core.UI
{
    public class UiImage : UiItem
    {
        private readonly Texture2D _texture;

        public override int Width => _texture.Width;
        public override int Height => _texture.Height;

        public UiImage(UiContainer container, int x, int y, Texture2D texture) : base(container, x, y)
        {
            _texture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(_texture, Bounds, Color.White);
        }
    }
}
