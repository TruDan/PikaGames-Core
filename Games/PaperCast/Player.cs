using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PikaGames.Games.PaperCast.Level;
using Color = Microsoft.Xna.Framework.Color;

namespace PikaGames.Games.PaperCast
{
    public class Player : DrawableGameComponent
    {
        const int Speed = 4;

        public int X { get; set; } = 6 * Tile.Size;
        public int Y { get; set; } = 6 * Tile.Size;

        public Vector2 Position => new Vector2(X, Y);

        public Level.Level Level { get; private set; }
        public Color Color { get; set; } = Color.Aqua;

        private int _trailSize = 0;

        private Direction _currentDirection = Direction.None;
        private Direction _nextDirection = Direction.None;

        private Texture2D _texture;

        public Player(Game game , Level.Level level) : base(game)
        {
            Level = level;

            DrawOrder = 4;

            var tile = Level.GetTileFromPosition(X, Y);
            for(int i=-1;i<=1;i++)
            {
                for (int j = -1; j <= 1;j++)
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

        public override void Initialize()
        {

            _texture = new Texture2D(GraphicsDevice, Tile.Size, Tile.Size);
            Color[] data = new Color[Tile.Size * Tile.Size];
            for (int i = 0; i < data.Length; i++) data[i] = Color;
            _texture.SetData(data);

            base.Initialize();
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
            if (targetTile == null)
            {
                targetX = (int)Math.Floor((double)X / Tile.Size) * Tile.Size;
                targetY = (int)Math.Floor((double)Y / Tile.Size) * Tile.Size;
            }

            X = targetX;
            Y = targetY;
        }

        public override void Draw(GameTime gameTime)
        {
            //((PaperCastGame) Game).SpriteBatch.Draw(_texture, Position, Color.White);

            //var posText = "(" + X + "," + Y + ")";
            //var v = PaperCastGame.DebugFont.MeasureString(posText);
            //((PaperCastGame)Game).SpriteBatch.DrawString(PaperCastGame.DebugFont, posText, new Vector2(X + (Tile.Size / 2) - v.X, Y + (Tile.Size / 2) - v.Y), Color.Yellow);

            base.Draw(gameTime);
        }

    }
}
