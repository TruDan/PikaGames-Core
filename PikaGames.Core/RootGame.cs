using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Entities;
using PikaGames.Games.Core.Sound;
using PikaGames.Games.Core.UI;

namespace PikaGames.Games.Core
{
    public class RootGame : Game
    {
        public static RootGame Instance { get; private set; }

        public Vector2 WindowSize = new Vector2(720 * 2, 480 * 2);
        public Vector2 VirtualSize = new Vector2(720 * 2, 480 * 2);

        public BoxingViewportAdapter ViewportAdapter;

        public SoundManager SoundManager;
        
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        
        public bool RequestingExit = false;

        private GameBase _rootGame;
        private GameBase _currentGame;
        private GameBase _nextGame;

        private SpriteBatch _spriteBatch;
        private Sprite _transitionImage;

        private bool _isTransitioning = false;
        private bool _beginTransitionFade = false;

        public RootGame(GameBase rootGame)
        {
            Instance = this;

            _rootGame = rootGame;
            _currentGame = rootGame;

            SoundManager = new SoundManager();

            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            _graphicsDeviceManager.PreferredBackBufferWidth = (int)WindowSize.X;
            _graphicsDeviceManager.PreferredBackBufferHeight = (int)WindowSize.Y;
            _graphicsDeviceManager.ApplyChanges();
        }

        public void LoadGame(GameBase game)
        {
            if (_isTransitioning) return;
            _isTransitioning = true;

            game.Initialize();

            _nextGame = game;

            _transitionImage.Alpha = 0;
            _transitionImage.IsVisible = true;
            _beginTransitionFade = true;
        }

        public void UnloadGame()
        {
            if (_isTransitioning) return;
            _isTransitioning = true;

            if (_currentGame == _rootGame)
                Exit();

            _nextGame = _rootGame;

            _transitionImage.Alpha = 0;
            _transitionImage.IsVisible = true;
            _beginTransitionFade = true;
        }

        protected override void Initialize()
        {
            if (ViewportAdapter == null)
                ViewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, (int)VirtualSize.X, (int)VirtualSize.Y);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _rootGame?.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            var transitionTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            transitionTexture.SetData(new Color[] { UiTheme.SceneTransitionBackgroundColor });
            _transitionImage = new Sprite(transitionTexture);
            _transitionImage.Scale = new Vector2(VirtualSize.X, VirtualSize.Y);
            _transitionImage.Alpha = 0.0f;
            _transitionImage.IsVisible = false;

            _rootGame?.LoadContent();
        }

        protected override void UnloadContent()
        {
            _rootGame?.UnloadContent();

            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (RequestingExit)
                Exit();

            if (_isTransitioning)
            {
                UpdateTransition(gameTime);
            }

            if(_currentGame?.RequestingExit ?? false)
                UnloadGame();

            base.Update(gameTime);
            
            _currentGame?.Update(gameTime);
        }

        private void UpdateTransition(GameTime gameTime)
        {
            var dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_beginTransitionFade)
            {
                if (_transitionImage.Alpha < 1.0f)
                    _transitionImage.Alpha += 0.2f;
                else
                    _beginTransitionFade = false;
            }
            else
            {
                if (_nextGame != null)
                {
                    _currentGame.UnloadContent();
                    _currentGame = _nextGame;
                    _currentGame.LoadContent();
                    _nextGame = null;
                }

                if (_transitionImage.Alpha > 0.0f)
                    _transitionImage.Alpha -= 0.2f;
                else
                {
                    _transitionImage.IsVisible = false;
                    _isTransitioning = false;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            _currentGame?.Draw(gameTime);
            
            _spriteBatch.Begin();
            _spriteBatch.Draw(_transitionImage.TextureRegion.Texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White * _transitionImage.Alpha);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
