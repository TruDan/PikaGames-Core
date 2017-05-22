using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

			internal static void Init()
			{
				Logo = Content.Load<Texture2D>("Images/Logo");
				Background = Content.Load<Texture2D>("Images/Background");
				Buttons_A = Content.Load<Texture2D>("Images/Buttons/A");
				Buttons_B = Content.Load<Texture2D>("Images/Buttons/B");
			}
		}


		public static class Sfx
		{

			public static SoundEffect SpaceMorph { get; private set; }

			internal static void Init()
			{
				SpaceMorph = Content.Load<SoundEffect>("Sfx/SpaceMorph");
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