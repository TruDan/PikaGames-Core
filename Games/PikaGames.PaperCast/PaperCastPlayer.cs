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

        public int TileX => (int) Math.Floor((double)X / Tile.Size);
        public int TileY => (int) Math.Floor((double)Y / Tile.Size);

        public Level Level { get; private set; }
        public Color Color { get; set; } = Color.Aqua;
        
        private int _trailSize = 0;

        private Direction _currentDirection = Direction.None;
        private Direction _nextDirection = Direction.None;

        public PaperCastPlayer(PlayerIndex playerIndex, Level level, Color color) : base(GetTexture(color, Tile.Size), playerIndex)
        {
            Level = level;
            Color = color;

            Width = Tile.Size;
            Height = Tile.Size;

            var x = 12;
            var y = 12;
            
            if (playerIndex == PlayerIndex.Two || playerIndex == PlayerIndex.Four)
            {
                x = Level.Width - x;
            }

            if (playerIndex == PlayerIndex.Three || playerIndex == PlayerIndex.Four)
            {
                y = Level.Height - y;
            }

            X = x * Tile.Size;
            Y = y * Tile.Size;


            var tile = Level.GetTileFromPosition(X, Y);
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    var t = Level.GetTile(tile.X + i, tile.Y + j);
                    if (t != null)
                    {
                        t.Owner = this;
                        t.UpdateTexture();
                    }
                }
            }
        }


        public void Move(Direction direction)
        {
            if (_currentDirection == direction || _nextDirection == direction)
                return;

            _nextDirection = direction;
        }

        public override void Update(GameTime deltaTime)
        {
            base.Update(deltaTime);

            if (((PaperCastGame) Level.Game).GameMapScene.IsPaused)
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
                if (X % Tile.Size == 0 && Y % Tile.Size == 0)
                {
                    _currentDirection = _nextDirection;
                }
            }

            var tile = Level.GetTileFromPosition(X, Y);

            if (X % Tile.Size == 0 && Y % Tile.Size == 0)
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
                        // kill
                    }
                    else
                    {
                        if (tile.PendingOwner != null)
                        {
                            // kill other player
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

            var targetX = X;
            var targetY = Y;

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
            if (targetTile == null || targetX < 0 || targetY < 0 || targetX > (Level.Width*Tile.Size -Width) || targetY > (Level.Height * Tile.Size - Height))
            {
                targetX = TileX * Tile.Size;
                targetY = TileY * Tile.Size;
            }

            X = targetX;
            Y = targetY;
        }


        private static Texture2D GetTexture(Color color, int size)
        {
            return TextureUtils.CreateRectangle(size, size, color);
        }
    }
}
