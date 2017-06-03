using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.NuclexGui;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Entities;
using PikaGames.Games.Core.Gui;
using PikaGames.Games.Core.Gui.Pika;
using PikaGames.Games.Core.Sound;
using PikaGames.Games.Core.UI;
using PikaGames.Games.Core.Utils;


namespace PikaGames.Games.Core
{
    internal enum Environment
    {
        Desktop,
        Mobile
    }

    public class RootGame : Game
    {
        private static Environment Environment = Environment.Desktop;

        public static RootGame Instance { get; private set; }

        private static IList<RootGame> SubInstances { get; } = new List<RootGame>();

        private bool IsPrimaryInstance => Instance == this;

        public Vector2 WindowSize = new Vector2(720 * 2, 480 * 2);
        public Vector2 VirtualSize = new Vector2(720, 480);

        public BoxingViewportAdapter ViewportAdapter;

        public SoundManager SoundManager;

        private GraphicsDeviceManager _graphicsDeviceManager;
        
        public bool RequestingExit = false;

        private GameBase _rootGame;
        private GameBase _currentGame;
        private GameBase _nextGame;

        private SpriteBatch _spriteBatch;
        private Sprite _transitionImage;

        private bool _isTransitioning = false;
        private bool _beginTransitionFade = false;

        private InputListenerComponent _inputManager;
        public GuiManager GuiManager;

        public RootGame()
        {
            if (Instance == null)
                Instance = this;
            
            SoundManager = new SoundManager();

            InitialiseDisplayManager();
        }

        protected internal void Assign(GameBase rootGame)
        {
            //if (_rootGame != null) return;

            _rootGame = rootGame;

            _frameCounter = new FrameCounter(rootGame);

            //if(!IsPrimaryInstance)
               // SubInstances.Add(this);
        }

        private void InitialiseDisplayManager()
        {
            if (false && Instance?._graphicsDeviceManager != null)
            {
                _graphicsDeviceManager = Instance._graphicsDeviceManager;

                Services.AddService(typeof(IGraphicsDeviceManager), _graphicsDeviceManager);
                Services.AddService(typeof(IGraphicsDeviceService), _graphicsDeviceManager);
                return;
            }

            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            _graphicsDeviceManager.DeviceCreated += (sender, args) =>
            {
                Debug.WriteLine("Device Created: " + args);
                DebugGraphics();
            };
            _graphicsDeviceManager.DeviceDisposing += (sender, args) =>
            {
                Debug.WriteLine("Device Disposed: " + args);
                DebugGraphics();
            };
            _graphicsDeviceManager.DeviceReset += (sender, args) =>
            {
                Debug.WriteLine("Device Reset: " + args);
                DebugGraphics();
            };


            if (Environment == Environment.Mobile)
            {
                Debug.WriteLine("IS MOBILE");
                _graphicsDeviceManager.IsFullScreen = true;
                _graphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft;
                _graphicsDeviceManager.ApplyChanges();
            }
            else if (Environment == Environment.Desktop)
            {
                Debug.WriteLine("IS DESKTOP");
                _graphicsDeviceManager.PreferredBackBufferWidth = (int)WindowSize.X;
                _graphicsDeviceManager.PreferredBackBufferHeight = (int)WindowSize.Y;
                _graphicsDeviceManager.ApplyChanges();

                Window.AllowUserResizing = false;
                IsMouseVisible = true;
            }
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
            Debug.WriteLine(" - ROOTGAME " + Instance._rootGame?.GetType().Name + " - Initialize");

            if (ViewportAdapter == null)
                ViewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, (int)VirtualSize.X, (int)VirtualSize.Y);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

#if DEBUG
			Window.AllowUserResizing = true;

			this._graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
			base.IsFixedTimeStep = false;
			this._graphicsDeviceManager.ApplyChanges();
#endif

            _inputManager = new InputListenerComponent(this);

            var service = new PikaGuiInputService(_inputManager, ViewportAdapter);
            GuiManager = new GuiManager(Services, service);
            GuiManager.Visualizer =
                PikaGuiVisualizer.FromResource(Services, "PikaGames.Games.Core.Gui.Pika.Skins.PikaSkin.json");

            GuiManager.Screen = new GuiScreen(VirtualSize.X, VirtualSize.Y);
            GuiManager.Screen.Desktop.Bounds = new UniRectangle(new UniScalar(0f, 0), new UniScalar(0f, 0), new UniScalar(1f, 0), new UniScalar(1f, 0));

            GuiManager.Initialize();
            GuiManager.InputCapturer = new PikaInputCapturer(service);

			_rootGame?.Initialize();

            base.Initialize();
            
            if (IsPrimaryInstance)
            {
                foreach (var rg in SubInstances.ToArray())
                {
                    rg.Initialize();
                }
            }
            else
            {
                SubInstances.Add(this);
                Debug.WriteLine(" - ROOTGAME " + Instance._rootGame?.GetType().Name + " - Registered SubInstance");
            }

            _currentGame = _rootGame;
        }
        
        protected override void LoadContent()
        {
            Debug.WriteLine(" - ROOTGAME " + Instance._rootGame?.GetType().Name + " - LoadContent");

            if (Environment == Environment.Mobile)
            {
                VirtualSize.X = GraphicsDevice.Viewport.Width;
                VirtualSize.Y = GraphicsDevice.Viewport.Height;
            }

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
            Debug.WriteLine(" - ROOTGAME " + Instance._rootGame?.GetType().Name + " - UnloadContent");

            if (IsPrimaryInstance)
            {
                foreach (var rg in SubInstances.ToArray())
                {
                    rg.UnloadContent();
                }
            }
            else
            {
                SubInstances.Remove(this);
                Debug.WriteLine(" - ROOTGAME " + Instance._rootGame?.GetType().Name + " - Unregistered SubInstance");

            }

            _rootGame?.UnloadContent();

            base.UnloadContent();
        }

        private FrameCounter _frameCounter;
#if DEBUG
		private KeyboardState _prevState;
#endif
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
            
            _inputManager.Update(gameTime);
            GuiManager.Update(gameTime);

            _currentGame?.Update(gameTime);

            if (IsPrimaryInstance)
            {
                foreach (var rg in SubInstances.ToArray())
                {
                    rg.Update(gameTime);
                }
            }
#if DEBUG
	        var keyboardState = Keyboard.GetState();
	        if (_prevState != keyboardState)
	        {
		        if (keyboardState.IsKeyDown(Keys.F11))
		        {
			        _graphicsDeviceManager.IsFullScreen = !_graphicsDeviceManager.IsFullScreen;
					_graphicsDeviceManager.ApplyChanges();
		        }
		        _prevState = keyboardState;
	        }
#endif
			_frameCounter.Update();
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

            if (IsPrimaryInstance)
            {
                foreach (var rg in SubInstances.ToArray())
                {
                    rg.Draw(gameTime);
                }
            }
            
            _spriteBatch.Begin();
            _spriteBatch.Draw(_transitionImage.TextureRegion.Texture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White * _transitionImage.Alpha);
            _spriteBatch.End();

            base.Draw(gameTime);

            _frameCounter.Draw(_spriteBatch);

            //_spriteBatch.Begin(transformMatrix: ViewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);
            //_spriteBatch.End();;
            GuiManager.Draw(gameTime);
        }


        public static void InitDesktop()
        {
            Environment = Environment.Desktop;
        }

        public static void InitMobile()
        {
            Environment = Environment.Mobile;
        }

        public static void DebugGraphics()
        {
            try
            {
                Debug.WriteLine("ROOT " + Instance._rootGame?.GetType().Name + " - " +
                                Instance.GraphicsDevice.PresentationParameters.DeviceWindowHandle);

                int i = 0;
                foreach (var game in SubInstances.ToArray())
                {
                    i++;
                    Debug.WriteLine(" #" + i + " " + game._rootGame?.GetType().Name + " - " +
                                    game.GraphicsDevice.PresentationParameters.DeviceWindowHandle);
                }
            }
            catch(Exception e) { }
        }
    }
}
