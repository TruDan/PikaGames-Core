using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
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

			internal static void Init()
			{
				PaperCastLogo = Content.Load<Texture2D>("Images/PaperCastLogo");
			}
		}


        internal static void Init(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            Content = new ContentManager(contentManager.ServiceProvider, "Content");
            GraphicsDevice = graphicsDevice;

			Images.Init();
        }
	}
}