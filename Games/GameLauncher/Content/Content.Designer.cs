

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GameLauncher
{

	public static class Resources
	{
        private static ContentManager Content;
        private static GraphicsDevice GraphicsDevice;
	

		public static class Images
		{

			public static Texture2D PikaGamesLogo { get; private set; }

			public static Texture2D InputKeyboard { get; private set; }

			public static Texture2D InputPhone { get; private set; }

			public static Texture2D InputNetwork { get; private set; }

			public static Texture2D InputGamePad { get; private set; }

			internal static void Init()
			{
				PikaGamesLogo = Content.Load<Texture2D>("Images/PikaGamesLogo");
				InputKeyboard = Content.Load<Texture2D>("Images/Inputs/Keyboard");
				InputPhone = Content.Load<Texture2D>("Images/Inputs/Phone");
				InputNetwork = Content.Load<Texture2D>("Images/Inputs/Network");
				InputGamePad = Content.Load<Texture2D>("Images/Inputs/GamePad");
			}
		}


		public static class Music
		{

			public static Song Metropolis { get; private set; }

			internal static void Init()
			{
				Metropolis = Content.Load<Song>("Music/Metropolis");
			}
		}


        internal static void Init(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            Content = new ContentManager(contentManager.ServiceProvider, "Content");
            GraphicsDevice = graphicsDevice;

			Images
.Init();
			Music
.Init();
        }
	}
}
