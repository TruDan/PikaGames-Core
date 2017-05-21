using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PikaGames.PaperCast
{

	public static class Resources
	{
        private static ContentManager Content;
        private static GraphicsDevice GraphicsDevice;
	

		public static class Images
		{

			public static Texture2D PaperCastLogo { get; private set; }

			public static Texture2D InputKeyboard { get; private set; }

			public static Texture2D InputPhone { get; private set; }

			public static Texture2D InputNetwork { get; private set; }

			public static Texture2D InputGamePad { get; private set; }

			internal static void Init()
			{
				PaperCastLogo = Content.Load<Texture2D>("Images/PaperCastLogo");
				InputKeyboard = Content.Load<Texture2D>("Images/InputKeyboard");
				InputPhone = Content.Load<Texture2D>("Images/InputPhone");
				InputNetwork = Content.Load<Texture2D>("Images/InputNetwork");
				InputGamePad = Content.Load<Texture2D>("Images/InputGamePad");
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

			Images.Init();
			Sfx.Init();
        }
	}
}