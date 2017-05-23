using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PikaGames.Games.Core;
using PikaGames.Games.Core.Entities;
using PikaGames.Games.Core.Utils;
using PikaGames.Games.RaceCast.Scenes;
using PikaGames.Games.RaceCast.UI;

namespace PikaGames.Games.RaceCast
{
    public class RaceCastGame : GameBase
    {
		internal GameSetupScene GameSetupScene { get; private set; }
		internal MainMenuScene MainMenuScene { get; private set; }

		public RaceCastGame()
	    {
		    
	    }

        protected override void Initialize()
        {
            base.Initialize();
			GameSetupScene = new GameSetupScene();
			//MainMenuScene = new MainMenuScene();

			SceneManager.DefaultScene = GameSetupScene;

			InitialiseLocalMultiplayer();
		}

	    public override Player CreatePlayer(PlayerIndex playerIndex)
	    {
			//TODO: Select random car texture.

			var texture = new Texture2D(GraphicsDevice, 32, 64);
			Color[] data = new Color[32 * 64];

		    for (int i = 0; i < data.Length; i++)
		    {
			    data[i] = Color.Red;
		    }

		    data[0] = Color.LightBlue;
			data[1] = Color.LightBlue;
			data[33] = Color.LightBlue;
			data[34] = Color.LightBlue;

			data[31] = Color.LightBlue;
			data[32] = Color.LightBlue;
			data[64] = Color.LightBlue;
			data[63] = Color.LightBlue;

			texture.SetData(data);

			var player = new RaceCastPlayer(this, texture, playerIndex, "Driver " + ((int)playerIndex + 1));
			player.Position = new Vector2(32, 32);
		    return player;
	    }
    }
}
