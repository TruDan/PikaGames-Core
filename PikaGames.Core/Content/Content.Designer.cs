

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

			public static SpriteFont TitleFont { get; private set; }

			public static SpriteFont DefaultFont { get; private set; }

			public static SpriteFont GameFont { get; private set; }

			public static SpriteFont DebugFont { get; private set; }

			internal static void Init()
			{
				TitleFont = Content.Load<SpriteFont>("TitleFont");
				DefaultFont = Content.Load<SpriteFont>("DefaultFont");
				GameFont = Content.Load<SpriteFont>("fonts/Press Start 2P");
				DebugFont = Content.Load<SpriteFont>("fonts/DebugFont");
			}
		}


		public static class Images
		{

			public static Texture2D PikaGamesLogo { get; private set; }

			public static Texture2D InputKeyboard { get; private set; }

			public static Texture2D InputPhone { get; private set; }

			public static Texture2D InputNetwork { get; private set; }

			public static Texture2D InputGamePad { get; private set; }

			public static Texture2D Logo { get; private set; }

			public static Texture2D Background { get; private set; }

			public static Texture2D Cursor { get; private set; }

			public static Texture2D Buttons_A { get; private set; }

			public static Texture2D Buttons_B { get; private set; }

			public static Texture2D Buttons_Pika { get; private set; }

			public static Texture2D PikaSheet { get; private set; }

			public static Texture2D Icons { get; private set; }

			public static Texture2D Splash_PikaGames { get; private set; }

			internal static void Init()
			{
				PikaGamesLogo = Content.Load<Texture2D>("images/PikaGamesLogo");
				InputKeyboard = Content.Load<Texture2D>("images/Inputs/Keyboard");
				InputPhone = Content.Load<Texture2D>("images/Inputs/Phone");
				InputNetwork = Content.Load<Texture2D>("images/Inputs/Network");
				InputGamePad = Content.Load<Texture2D>("images/Inputs/GamePad");
				Logo = Content.Load<Texture2D>("images/Logo");
				Background = Content.Load<Texture2D>("images/Background");
				Cursor = Content.Load<Texture2D>("images/Cursor");
				Buttons_A = Content.Load<Texture2D>("images/buttons/A");
				Buttons_B = Content.Load<Texture2D>("images/buttons/B");
				Buttons_Pika = Content.Load<Texture2D>("images/buttons/Pika");
				PikaSheet = Content.Load<Texture2D>("PikaSheet");
				Icons = Content.Load<Texture2D>("Icons");
				Splash_PikaGames = Content.Load<Texture2D>("images/splash/PikaGames");
			}
		}


		public static class Music
		{

			public static Song Metropolis { get; private set; }

			internal static void Init()
			{
				Metropolis = Content.Load<Song>("music/Metropolis");
			}
		}


		public static class Sfx
		{

			public static SoundEffect SpaceMorph { get; private set; }

			public static SoundEffect Splash_PikaGames { get; private set; }

			public static SoundEffect UI_Zap { get; private set; }

			internal static void Init()
			{
				SpaceMorph = Content.Load<SoundEffect>("sfx/SpaceMorph");
				Splash_PikaGames = Content.Load<SoundEffect>("sfx/splash/PikaGames");
				UI_Zap = Content.Load<SoundEffect>("sfx/ui/Zap");
			}
		}


        internal static void Init(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            Content = new ContentManager(contentManager.ServiceProvider, "Content");
            GraphicsDevice = graphicsDevice;

			Fonts
.Init();
			Images
.Init();
			Music
.Init();
			Sfx
.Init();
        }
	}
}
