using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PikaGames.Games.Core.Utils
{
	public class FrameCounter
	{
		private int _frames = 0;
		private Stopwatch _sw = Stopwatch.StartNew();

	    private GameBase _game;

		public int FrameRate { get; set; }
		public FrameCounter(GameBase game)
		{
		    _game = game;
		}

		public void Update()
		{
			if (_sw.ElapsedMilliseconds >= 1000)
			{
				int frames = Interlocked.Exchange(ref _frames, 0);
				FrameRate = frames;

				_sw.Restart();
#if DEBUG
				if (Debugger.IsAttached)
				{
					Debug.WriteLine($"{_game.GetType().Name} FPS: {FrameRate}");
				}
#endif
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Interlocked.Increment(ref _frames);

            spriteBatch.Begin();
            spriteBatch.DrawString(Resources.Fonts.DefaultFont, FrameRate + " FPS", new Vector2(5f, 2f), new Color(new Vector3(0, 255, 0)), 0, Vector2.Zero, 0.85f, SpriteEffects.None, 0);
            spriteBatch.End();
		}
	}
}
