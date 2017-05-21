using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.Scenes;
using PikaGames.Games.Core.Utils;
using PikaGames.PaperCast.World;

namespace PikaGames.PaperCast.Scenes
{
    public class GameMapScene : Scene
    {

        private Camera2D _camera;
        private const float CameraSmooth = 0.1f;

        public Level Level { get; private set; }

        private PaperCastPlayer _player;

        public bool IsPaused = false;
        private Texture2D _pauseBackground;
        private Rectangle _pauseBackgroundArea;
        private Vector2 _pausedPosition;
        private int _playerListItemHeight;


        private int _menuSelectedIndex = 0;
        private Vector2 _menuPosition;
        private string[] _menu = new string[]
        {
            "Resume",
            "Main Menu",
            "Options",
            "Exit"
        };

        private Vector2 _playerListPosition;

        public GameMapScene(PaperCastGame game)
        {
            Level = new Level(game, 64, 64);

            _pauseBackgroundArea = new Rectangle(0, 0, (int)game.VirtualSize.X, (int)game.VirtualSize.Y);

            _pauseBackground = TextureUtils.CreateRectangle(_pauseBackgroundArea.Width, _pauseBackgroundArea.Height,
                MaterialDesignColors.BlueGrey900);

        }

        public override void LoadContent()
        {
            base.LoadContent();

            var font = Games.Core.Resources.Fonts.GameFont;
            var center = Game.ViewportAdapter.Center.ToVector2()/2;

            var textSize = font.MeasureString("Paused") * 4;
            _pausedPosition = center / 2 - textSize / 2;

            _pausedPosition.X = Math.Max(100, _pausedPosition.X);
            
            _menuPosition = new Vector2(_pausedPosition.X + 25, _pausedPosition.Y + textSize.Y + 50);
            _playerListPosition = new Vector2(Math.Max(_pausedPosition.X, center.X*2) + 25, _pausedPosition.Y + textSize.Y + 50);
            _playerListItemHeight = (int) (font.MeasureString("Player 0").Y * 1.5f);
        }

        protected override void Initialise()
        {
            _camera = new Camera2D(Game.ViewportAdapter);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Game.Players.Any(p => p.Input.IsPressed(InputCommand.Start)))
            {
                IsPaused = !IsPaused;
            }

            if (IsPaused)
            {
                if (Game.Players.Any(p => p.Input.IsPressed(InputCommand.Up)))
                {
                    if (_menuSelectedIndex == 0) _menuSelectedIndex = _menu.Length - 1;
                    else _menuSelectedIndex--;

                    Game.SoundManager.Play(Resources.Sfx.SpaceMorph);
                }
                else if (Game.Players.Any(p => p.Input.IsPressed(InputCommand.Down)))
                {
                    _menuSelectedIndex++;
                    if (_menuSelectedIndex == _menu.Length) _menuSelectedIndex = 0;

                    Game.SoundManager.Play(Resources.Sfx.SpaceMorph);
                }
                else if (Game.Players.Any(p => p.Input.IsPressed(InputCommand.A)))
                {
                    switch (_menuSelectedIndex)
                    {
                        case 0:
                            // Resume
                            IsPaused = false;
                            break;

                        case 1:
                            // Main Menu
                            Game.SceneManager.ChangeScene(((PaperCastGame) Game).MainMenuScene);
                            break;
                        case 2:
                            // Options

                            break;
                        case 3:
                            // Exit
                            Game.Exit();
                            break;
                    }
                }
            }
            else
            {
                Level.Update(gameTime);
            }

            if (_player == null)
            {
                _player = (PaperCastPlayer) Game.Players.FirstOrDefault();
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
            x = MathHelper.Clamp(x, 0.0f, (Level.Width * Tile.Size) - viewport.VirtualWidth);
            var y = MathHelper.Lerp(_camera.Position.Y, newPosition.Y + playerOffsetY, CameraSmooth);
            y = MathHelper.Clamp(y, 0.0f, (Level.Height * Tile.Size) - viewport.VirtualHeight);
            _camera.Position = new Vector2((int)x, (int)y);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            base.Draw(gameTime, spriteBatch, viewportAdapter);
            
            Level.Draw(gameTime, _camera, spriteBatch);

            if (IsPaused)
            {
                spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);
                spriteBatch.Draw(_pauseBackground, _pauseBackgroundArea, Color.White * 0.85f);
                spriteBatch.End();

                spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);
                for (int j = 0; j < 6; j++)
                {
                    spriteBatch.DrawString(Games.Core.Resources.Fonts.GameFont, "Paused", new Vector2(_pausedPosition.X + j, _pausedPosition.Y + j), MaterialDesignColors.Amber900, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0);
                }
                spriteBatch.DrawString(Games.Core.Resources.Fonts.GameFont, "Paused", _pausedPosition, MaterialDesignColors.Amber500, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0);

                for (int i = 0; i < _menu.Length; i++)
                {
                    Color color = MaterialDesignColors.LightBlue500;
                    Color shadow = MaterialDesignColors.LightBlue900;

                    if (i == _menuSelectedIndex)
                    {
                        color = MaterialDesignColors.LightBlue100;
                        shadow = MaterialDesignColors.LightBlue500;
                    }

                    for (int j = 0; j < (i == _menuSelectedIndex ? 4 : 6); j++)
                    {
                        spriteBatch.DrawString(Games.Core.Resources.Fonts.GameFont, _menu[i], new Vector2(_menuPosition.X + j + (_menuSelectedIndex == i ? 10 : 0), _menuPosition.Y + 50 * i - j), shadow, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
                    }


                    spriteBatch.DrawString(Games.Core.Resources.Fonts.GameFont, _menu[i], new Vector2(_menuPosition.X + (_menuSelectedIndex == i ? 10 : 0), _menuPosition.Y + 50 * i), color, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0);
                }


                // Player List
                for (int i = 0; i < Game.Players.Count; i++)
                {
                    var player = Game.Players[i];

                    spriteBatch.Draw(Resources.Images.InputGamePad.TintSolid(MaterialDesignColors.LightBlueA100), new Rectangle((int)_playerListPosition.X, (int)_playerListPosition.Y + i * (_playerListItemHeight + 8), _playerListItemHeight-2, _playerListItemHeight-2), Color.White);

                    spriteBatch.DrawString(Games.Core.Resources.Fonts.GameFont, "Player " + i, _playerListPosition + new Vector2(_playerListItemHeight + 8, i * (_playerListItemHeight + 8)), MaterialDesignColors.LightBlue500, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
                }


                spriteBatch.End();
            }
        }

    }
}
