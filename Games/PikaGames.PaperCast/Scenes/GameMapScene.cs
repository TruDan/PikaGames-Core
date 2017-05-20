using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Scenes;
using PikaGames.PaperCast.World;

namespace PikaGames.PaperCast.Scenes
{
    public class GameMapScene : Scene
    {

        private Camera2D _camera;
        private const float CameraSmooth = 0.1f;

        private Level _level;

        private PaperCastPlayer _player;

        protected override void Initialise()
        {
            _camera = new Camera2D(Game.ViewportAdapter);

            _level = new Level(Game, 64, 64);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _level.Update(gameTime);

            if (_player == null)
            {
                _player = new PaperCastPlayer(_level, Color.Aqua);

                Game.AddPlayer(_player);
            }

            UpdateCamera();
        }

        private void UpdateCamera()
        {
            var viewport = Game.ViewportAdapter;
            
            var newPosition = _player.Position - new Vector2(viewport.VirtualWidth / 2f, viewport.VirtualHeight / 2f);
            var playerOffsetX = _player.Width / 2;
            var playerOffsetY = _player.Height / 2;
            var x = MathHelper.Lerp(_camera.Position.X, newPosition.X + playerOffsetX, CameraSmooth);
            x = MathHelper.Clamp(x, 0.0f, (_level.Width * Tile.Size) - viewport.VirtualWidth);
            var y = MathHelper.Lerp(_camera.Position.Y, newPosition.Y + playerOffsetY, CameraSmooth);
            y = MathHelper.Clamp(y, 0.0f, (_level.Height * Tile.Size) - viewport.VirtualHeight);
            _camera.Position = new Vector2((int)x, (int)y);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            base.Draw(gameTime, spriteBatch, viewportAdapter);
            
            _level.Draw(gameTime, _camera, spriteBatch);
        }

    }
}
