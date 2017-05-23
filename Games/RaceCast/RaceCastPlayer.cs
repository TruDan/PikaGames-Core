using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using PikaGames.Games.Core.Entities;
using PikaGames.Games.Core.Input;
using PikaGames.Games.RaceCast.Scenes;
using PikaGames.Games.RaceCast.UI;
using PikaGames.Games.RaceCast.Util;
using PikaGames.Games.RaceCast.World;

namespace PikaGames.Games.RaceCast
{
	public class RaceCastPlayer : Player
	{
		private RaceCastGame Game { get; }
		public Vehicle Vehicle { get; }

		private string Name { get; }
		public RaceCastPlayer(RaceCastGame game, Texture2D texture, PlayerIndex playerIndex, string name) : base(texture, playerIndex)
		{
			Name = name;
			Game = game;
			IsAlive = true;

			Vehicle = new Vehicle();
		}

		private Level Level { get; set; } = null;
		public void Init(Level level)
		{
			Level = level;

			Vector2 halfSize = new Vector2(_texture.Width / 2f, _texture.Height / 2f);
			Vehicle.Setup(level, halfSize, 3.5f);
			Vehicle.SetLocation(Position + halfSize, 0);
		}

		public void DrawRaceCast(GameTime gameTime, SpriteBatch spriteBatch, Camera2D cam)
		{
			Vehicle.Draw(spriteBatch, _texture, cam);
		}

		public override void Update(GameTime gameTime)
		{
			var deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
			base.Update(gameTime);

			if (Level != null)
			{
				UpdateMovement(deltaTime);
			}
		}

		private void UpdateMovement(double dt)
		{
			bool leftDown = Input.IsDown(InputCommand.Left);
			bool rightDown = Input.IsDown(InputCommand.Right);
			bool upDown = Input.IsDown(InputCommand.Up);
			bool downDown = Input.IsDown(InputCommand.Down);
			bool handBrake = Input.IsDown(InputCommand.X);

			float steering = 0; //-1 is full left, 0 is center, 1 is full right
			float throttle = 0; //0 is coasting, 1 is full throttle
			float brakes = 0; //0 is no brakes, 1 is full brakes

			if (leftDown)
				steering = -1;
			else if (rightDown)
				steering = 1;
			else
				steering = 0;

			if (upDown)
			{
				if (downDown)
				{
					brakes = 1;
				}
				else
				{
					throttle = -1;
				}
			}
			else
			{
				throttle = 0;
			}

			if (downDown)
			{
				if (upDown)
				{
					brakes = 1;
				}
				else
				{
					throttle = 1;
				}
			}
			else
			{
				brakes = 0;
			}

			Vehicle.SetSteering(steering);
			Vehicle.SetThrottle(throttle, true);
			Vehicle.SetBrakes(brakes);
			Vehicle.LockWheels(handBrake, false);

			Vehicle.Update((float)dt);

			Position = Vehicle.GetPosition();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
