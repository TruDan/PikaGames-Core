using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PikaGames.Games.RaceCast.World
{
	public class Tile
	{
		public const int Size = 32;

		public int X { get; }
		public int Y { get; }

		private Texture2D _texture;

		public float Friction { get; private set; } = 5f;
		public bool IsSolid { get; private set; } = false;
		public Tile(Texture2D texture, int x, int y)
		{
			X = x;
			Y = y;

			_texture = texture;
		}

		public void Draw(SpriteBatch batch)
		{
			batch.Draw(_texture, new Vector2(X * Size, Y * Size), Color.White);
		}
	}
}
