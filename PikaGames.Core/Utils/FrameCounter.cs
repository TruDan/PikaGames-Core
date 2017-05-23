using System.Diagnostics;
using System.Threading;

namespace PikaGames.Games.Core.Utils
{
	public class FrameCounter
	{
		private int _frames = 0;
		private Stopwatch _sw = Stopwatch.StartNew();

		public int FrameRate { get; set; }
		public FrameCounter()
		{
			
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
					Debug.WriteLine($"FPS: {FrameRate}");
				}
#endif
			}
		}

		public void OnDraw()
		{
			Interlocked.Increment(ref _frames);
		}
	}
}
