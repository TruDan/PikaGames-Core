using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PikaGames.Games.Core;
using PikaGames.Games.Core.Players;
using PikaGames.PaperCast.World;

namespace PikaGames.PaperCast
{
    public class PaperCastPlayer : Player
    {
        const int Speed = 4;


        public Level Level { get; private set; }
        public Color Color { get; set; } = Color.Aqua;
        
        private int _trailSize = 0;

        private Direction _currentDirection = Direction.None;
        private Direction _nextDirection = Direction.None;

        public PaperCastPlayer(Level level, Color color) : base(GetTexture(color, Tile.Size))
        {
            Level = level;
            Color = color;

            Width = Tile.Size;
            Height = Tile.Size;

            X = 6 * Tile.Size;
            Y = 6 * Tile.Size;


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
            // DEBUG
            KeyboardState kbs = Keyboard.GetState();
            if (kbs.IsKeyDown(Keys.W))
            {
                _nextDirection = Direction.North;
            }
            else if (kbs.IsKeyDown(Keys.S))
            {
                _nextDirection = Direction.South;
            }
            else if (kbs.IsKeyDown(Keys.D))
            {
                _nextDirection = Direction.East;
            }
            else if (kbs.IsKeyDown(Keys.A))
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
                targetX = (int)Math.Floor((double)X / Tile.Size) * Tile.Size;
                targetY = (int)Math.Floor((double)Y / Tile.Size) * Tile.Size;
            }

            X = targetX;
            Y = targetY;
        }


        private static Texture2D GetTexture(Color color, int size)
        {
            return Resources.Texture.CreateRectangle(size, size, color);
        }
    }
}
