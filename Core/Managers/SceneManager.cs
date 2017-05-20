using System;
using System.Collections.Generic;
using System.Text;
using PikaGames.Core;
using PikaGames.Core.Managers;
using PikaGames.Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.ViewportAdapters;

namespace PikaGames.Games.PaperCast.Managers
{
    class SceneManager
    {
        public Vector2 WindowSize = new Vector2(720, 480);
        public Vector2 VirtualSize = new Vector2(360, 240);
        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;
        public ViewportAdapter ViewportAdapter => GameBase.ViewportAdapter;
        public GameWindow GameMap => GameBase.GameWindow;
        public ContentManager Content { private set; get; }

        private GraphicsDeviceManager _graphicsDeviceManager;

        public bool RequestingFullscreen = false;
        public bool RequestingExit = false;

        //--------------------------------------------------
        // SceneManager Singleton variables

        private static SceneManager _instance = null;
        private static readonly object _padlock = new object();

        //--------------------------------------------------
        // Transition

        private SceneBase _currentScene, _newScene;
        private Sprite _transitionImage;
        private bool _isTransitioning = false;
        public bool IsTransitioning { get { return _isTransitioning; } }
        private bool _beginTransitionFade = false;

        //--------------------------------------------------
        // Debug mode

        public bool DebugMode = true;

        //--------------------------------------------------
        // Game fonts

        private SpriteFont _gameFont;
        public SpriteFont GameFont => _gameFont;

        //--------------------------------------------------
        // Intro started

        public bool TitleStarted = false;

        //----------------------//------------------------//

        public static SceneManager Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                        _instance = new SceneManager();
                    return _instance;
                }
            }
        }

        private SceneManager()
        {
            _currentScene = new SceneIntro();
        }

        public void SetGraphicsDeviceManager(GraphicsDeviceManager graphicsDeviceManager)
        {
            _graphicsDeviceManager = graphicsDeviceManager;
        }

        public void SetFullscreen(bool isFullscreen)
        {
            var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;

            var width = isFullscreen ? displayMode.Width : (int)WindowSize.X;
            var height = isFullscreen ? displayMode.Height : (int)WindowSize.Y;

            if (_graphicsDeviceManager != null)
            {
                _graphicsDeviceManager.PreferredBackBufferWidth = width;
                _graphicsDeviceManager.PreferredBackBufferHeight = height;
                _graphicsDeviceManager.IsFullScreen = isFullscreen;
                _graphicsDeviceManager.ApplyChanges();
            }
        }

        public void RequestFullscreen()
        {
            RequestingFullscreen = true;
        }

        public void RequestExit()
        {
            RequestingExit = true;
        }

        public SceneBase GetCurrentScene()
        {
            return _currentScene;
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            var transitionTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            transitionTexture.SetData(new Color[] { Color.Black });
            _transitionImage = new Sprite(transitionTexture);
            _transitionImage.Scale = new Vector2(VirtualSize.X, VirtualSize.Y);
            _transitionImage.Alpha = 0.0f;
            _transitionImage.IsVisible = false;
            _gameFont = Content.Load<SpriteFont>("fonts/GameFont");
            //_gameFontSmall = Content.Load<BitmapFont>("fonts/AlagardSmall");
            //_gameFontBig = Content.Load<BitmapFont>("fonts/AlagardBig");
            //ParticleManager = new ParticleManager<ParticleState>(1024 * 20, ParticleState.UpdateParticle);
            SoundManager.Initialize();
            _currentScene.LoadContent();
        }

        public void UnloadContent()
        {
            _currentScene.UnloadContent();
            SoundManager.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            if (_isTransitioning)
            {
                UpdateTransition(gameTime);
            }

            //ParticleManager.Update(gameTime);

            _currentScene.Update(gameTime);

            if (RequestingFullscreen)
            {
                SetFullscreen(true);
                RequestingFullscreen = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentScene.Draw(spriteBatch, ViewportAdapter);
            spriteBatch.Begin();
            spriteBatch.Draw(_transitionImage.TextureRegion.Texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White * _transitionImage.Alpha);
            spriteBatch.End();
            _currentScene.DrawDebugValue(spriteBatch);
        }

        public void ChangeScene(string newScene)
        {
            if (_isTransitioning) return;
            _newScene = (SceneBase)Activator.CreateInstance(Type.GetType("PikaGames.Core.Scenes." + newScene));
            _transitionImage.Alpha = 0;
            _transitionImage.IsVisible = true;
            _isTransitioning = true;
            _beginTransitionFade = true;
        }

        private void UpdateTransition(GameTime gameTime)
        {
            var dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_beginTransitionFade)
            {
                if (_transitionImage.Alpha < 1.0f)
                    _transitionImage.Alpha += 0.1f;
                else
                    _beginTransitionFade = false;
            }
            else
            {
                if (_newScene != null)
                {
                    _currentScene.UnloadContent();
                    _currentScene = _newScene;
                    _currentScene.LoadContent();
                    _newScene = null;
                }

                if (_transitionImage.Alpha > 0.0f)
                    _transitionImage.Alpha -= 0.1f;
                else
                {
                    _transitionImage.IsVisible = false;
                    _isTransitioning = false;
                }
            }
        }
    }
}
