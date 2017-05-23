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
using PikaGames.Games.Core.UI;
using PikaGames.Games.Core.Utils;
using PikaGames.PaperCast.Ui;
using PikaGames.PaperCast.World;

namespace PikaGames.PaperCast.Scenes
{
    public class GameMapScene : Scene
    {
        private Camera2D _camera;
        private PaperCastGame _game;

        public Level Level { get; private set; }
        
        public bool IsPaused = false;
        private Texture2D _pauseBackground;
        private Rectangle _pauseBackgroundArea;
        private Vector2 _pausedPosition;
        private int _playerListItemHeight;

        private PauseScreen _pauseScreen;

        private UiContainer _playersHud;
        private IDictionary<PlayerIndex, UiContainer> _playerHuds = new Dictionary<PlayerIndex, UiContainer>();

        private Vector2 _playerListPosition;

        public GameMapScene(PaperCastGame game)
        {
            _game = game;
            Level = new Level(game, 64, 64);

            _pauseBackgroundArea = new Rectangle(0, 0, (int)game.VirtualSize.X, (int)game.VirtualSize.Y);

            _pauseBackground = TextureUtils.CreateRectangle(_pauseBackgroundArea.Width, _pauseBackgroundArea.Height, UiTheme.BackgroundTheme.GetVariant(MaterialThemeVariant.Hue900));

        }

        public override void LoadContent()
        {
            base.LoadContent();

            var font = Games.Core.Resources.Fonts.GameFont;
            var center = Game.ViewportAdapter.Center.ToVector2()/2;

            var textSize = font.MeasureString("Paused") * 4;
            _pausedPosition = center / 2 - textSize / 2;

            _pausedPosition.X = Math.Max(100, _pausedPosition.X);
            
            _playerListPosition = new Vector2(Math.Max(_pausedPosition.X, center.X*2) + 25, _pausedPosition.Y + textSize.Y + 50);
            _playerListItemHeight = (int) (font.MeasureString("Player 0").Y * 1.5f);

            _pauseScreen = new PauseScreen((PaperCastGame) Game);

            _playersHud = new UiContainer(0, Game.ViewportAdapter.ViewportHeight-100);

            foreach (var playerIndex in Enum.GetValues(typeof(PlayerIndex)))
            {
                _playerHuds.Add((PlayerIndex) playerIndex, new PlayerHud(_game, (PlayerIndex)playerIndex, _playersHud, (Game.ViewportAdapter.ViewportWidth/4) * ((int)playerIndex), 0));
            }
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
                _pauseScreen.Update(gameTime);
            }
            else
            {
                Level.Update(gameTime);
                _playersHud.Update(gameTime);
            }
            
            UpdateCamera();
        }

        private void UpdateCamera()
        {
            _camera.LookAtMultiple(new Vector2(Level.Width, Level.Height), 100, Game.Players.Where(p => p.IsAlive).Select(p => p.Position).ToArray());
        }

		private Texture2D TintedLightBlue = Resources.Images.InputGamePad.TintSolid(MaterialDesignColors.LightBlue.GetVariant(MaterialThemeVariant.HueA100));
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            base.Draw(gameTime, spriteBatch, viewportAdapter);

			spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);

			Level.Draw(gameTime, _camera, spriteBatch);

            _playersHud.Draw(spriteBatch);

            if (IsPaused)
            {
				spriteBatch.Draw(_pauseBackground, _pauseBackgroundArea, Color.White * 0.85f);

                _pauseScreen.Draw(spriteBatch);
                
                // Player List
                for (int i = 0; i < Game.Players.Count; i++)
                {
                    //var player = Game.Players[i];

                    spriteBatch.Draw(TintedLightBlue, new Rectangle((int)_playerListPosition.X, (int)_playerListPosition.Y + i * (_playerListItemHeight + 8), _playerListItemHeight-2, _playerListItemHeight-2), Color.White);

                    spriteBatch.DrawString(Games.Core.Resources.Fonts.GameFont, "Player " + i, _playerListPosition + new Vector2(_playerListItemHeight + 8, i * (_playerListItemHeight + 8)), UiTheme.AccentTheme.GetVariant(MaterialThemeVariant.Base), 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
                }
            }

			spriteBatch.End();
		}

    }
}
