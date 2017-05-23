

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace PikaGames.Games.Core
{

	public static class Resources
	{
        private static ContentManager Content;
        private static GraphicsDevice GraphicsDevice;
	

		public static class Fonts
		{

			public static SpriteFont GameFont { get; private set; }

			public static SpriteFont DebugFont { get; private set; }

			internal static void Init()
			{
				GameFont = Content.Load<SpriteFont>("Fonts/GameFont");
				DebugFont = Content.Load<SpriteFont>("Fonts/DebugFont");
			}
		}


		public static class Images
		{

			public static Texture2D Logo { get; private set; }

			public static Texture2D Background { get; private set; }

			public static Texture2D Buttons_A { get; private set; }

			public static Texture2D Buttons_B { get; private set; }

			public static Texture2D Splash_PikaGames { get; private set; }

			internal static void Init()
			{
				Logo = Content.Load<Texture2D>("Images/Logo");
				Background = Content.Load<Texture2D>("Images/Background");
				Buttons_A = Content.Load<Texture2D>("Images/Buttons/A");
				Buttons_B = Content.Load<Texture2D>("Images/Buttons/B");
				Splash_PikaGames = Content.Load<Texture2D>("Images/Splash/PikaGames");
			}
		}


		public static class Sfx
		{

			public static SoundEffect SpaceMorph { get; private set; }

			public static SoundEffect Splash_PikaGames { get; private set; }

			public static SoundEffect UI_Zap { get; private set; }

			internal static void Init()
			{
				SpaceMorph = Content.Load<SoundEffect>("Sfx/SpaceMorph");
				Splash_PikaGames = Content.Load<SoundEffect>("Sfx/Splash/PikaGames");
				UI_Zap = Content.Load<SoundEffect>("Sfx/UI/Zap");
			}
		}


        internal static void Init(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            Content = new ContentManager(contentManager.ServiceProvider, "Content");
            GraphicsDevice = graphicsDevice;

			Fonts.Init();
			Images.Init();
			Sfx.Init();
        }
	}
}
