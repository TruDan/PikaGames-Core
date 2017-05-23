using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PikaGames.Games.Core;
using PikaGames.Games.Core.Entities;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.Utils;
using PikaGames.PaperCast.World;

namespace PikaGames.PaperCast
{
    public class PaperCastPlayer : Player
    {
        const int Speed = 4;

        public int Score { get; set; } = 0;

        public int TileX => (int) Math.Floor((double)Position.X / Tile.Size);
        public int TileY => (int) Math.Floor((double)Position.Y / Tile.Size);

        public Level Level { get; private set; }
        
        private int _trailSize = 0;

        private Direction _currentDirection = Direction.None;
        private Direction _nextDirection = Direction.None;

        public PaperCastPlayer(PlayerIndex playerIndex, Level level, Color color) : base(GetTexture(color, Tile.Size), playerIndex)
        {
            Level = level;
            Color = color;

            Width = Tile.Size;
            Height = Tile.Size;

        }

        public void Move(Direction direction)
        {
            if (_currentDirection == direction || _nextDirection == direction)
                return;

            _nextDirection = direction;
        }

        public override void Update(GameTime deltaTime)
        {
	        var dt = (float)deltaTime.ElapsedGameTime.TotalMilliseconds;

            base.Update(deltaTime);

            var g = ((PaperCastGame) Level.Game);
            if (g.GameMapScene.IsPaused || !g.GameMapScene.IsActive)
                return;

            if (IsConnected && !IsAlive)
            {
                if (Input.IsPressed(InputCommand.A, InputCommand.Start))
                {
                    IsAlive = true;
                    Level.SpawnPlayer(this);
                }
            }

            if (!IsAlive)
                return;

            if (Input.IsDown(InputCommand.Up))
            {
                _nextDirection = Direction.North;
            }
            else if (Input.IsDown(InputCommand.Down))
            {
                _nextDirection = Direction.South;
            }
            else if (Input.IsDown(InputCommand.Right))
            {
                _nextDirection = Direction.East;
            }
            else if (Input.IsDown(InputCommand.Left))
            {
                _nextDirection = Direction.West;
            }
            else
            {
                //_nextDirection = Direction.None;
            }


            if (_currentDirection == Direction.None || _currentDirection != _nextDirection)
            {
                if (Position.X % Tile.Size == 0f && Position.Y % Tile.Size == 0f)
                {
                    _currentDirection = _nextDirection;
                }
            }

            var tile = Level.GetTileFromPosition(Position.X, Position.Y);

            if (Position.X % Tile.Size == 0f && Position.Y % Tile.Size == 0f)
            {
                if (tile != null)
                {
                    if (tile.Owner == this)
                    {
                        // check for claim]
                        if (_trailSize > 0)
                        {
                            Level.ClaimTrail(this);
                            Level.UpdateClaims(this);
                        }

                        _trailSize = 0;
                    }
                    else if (tile.PendingOwner == this)
                    {
                        // whoops.
                        Level.KillPlayer(this);
                    }
                    else
                    {
                        if (tile.PendingOwner != null)
                        {
                            // kill other player
                            Level.KillPlayer(tile.PendingOwner);
                        }

                        tile.PendingOwner = this;
                        _trailSize++;
                        tile.UpdateTexture();
                    }
                }
            }

            if (_currentDirection == Direction.None)
            {
                return;
            }

            var targetX = Position.X;
            var targetY = Position.Y;

            if (_currentDirection == Direction.North)
            {
                targetY -= Speed;
            }
            else if (_currentDirection == Direction.South)
            {
                targetY += Speed;
            }
            else if (_currentDirection == Direction.East)
            {
                targetX += Speed;
            }
            else if (_currentDirection == Direction.West)
            {
                targetX -= Speed;
            }

            var targetTile = Level.GetTileFromPosition(targetX, targetY);
            if (targetTile == null || targetX < 0 || targetY < 0 || targetX > (Level.TileWidth*Tile.Size -Width) || targetY > (Level.TileHeight * Tile.Size - Height))
            {
                targetX = TileX * Tile.Size;
                targetY = TileY * Tile.Size;
            }

			Position = new Vector2(targetX, targetY);
        }

        public void Kill()
        {
            if (!IsAlive)
                return;

            IsAlive = false;

            _trailSize = 0;
            _currentDirection = Direction.None;
            _nextDirection = Direction.None;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!IsAlive)
                return;

            base.Draw(gameTime, spriteBatch);

        }


        private static Texture2D GetTexture(Color color, int size)
        {
            return TextureUtils.CreateRectangle(size, size, color);
        }
    }
}
