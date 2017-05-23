using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using PikaGames.Games.Core.Utils;
using PikaGames.Games.RaceCast.World;

namespace PikaGames.Games.RaceCast
{
	public class Level
	{
		private Texture2D _blankTexture = TextureUtils.CreateBorderedRectangle(Tile.Size, Tile.Size, Color.Black * 0.25f, Color.Black * 0.75f);
		private RaceCastGame Game { get; }
		public int Height { get; set; } = 64;
		public int Width { get; set; } = 64;

		public int PixelWidth => Width * Tile.Size;
		public int PixelHeight => Height * Tile.Size;

		private Tile[] Tiles { get; }
		public Level(RaceCastGame game)
		{
			Game = game;
			Tiles = new Tile[Height * Width];
			Init();
		}

		private void Init()
		{
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					Tiles[GetIndex(x, y)] = new Tile(_blankTexture, x, y);
				}
			}
		}

		private int GetIndex(int x, int y)
		{
			return (x * this.Width) + y;
		}

		public Tile GetTile(int x, int y)
		{
			if (x < 0 || x >= Width) return null;
			if (y < 0 || y >= Height) return null;

			return Tiles[GetIndex(x, y)];
		}

		public Tile GetTile(Vector2 pos)
		{
			return GetTile((int) Math.Floor(pos.X / Tile.Size), (int) Math.Floor(pos.Y / Tile.Size));
		}

		public void Draw(SpriteBatch spriteBatch, GameTime time, Camera2D camera)
		{
			spriteBatch.Begin(transformMatrix: camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

			foreach (var tile in Tiles)
			{
				tile.Draw(spriteBatch);
			}

			spriteBatch.End();

			foreach (var player in Game.Players.Cast<RaceCastPlayer>())
			{
				player.DrawRaceCast(time, spriteBatch, camera);
			}
		}
	}
}
