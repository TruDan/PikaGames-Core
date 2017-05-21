using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Entities;
using PikaGames.Games.Core.Sound;

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
            game.Initialize();

            _nextGame = game;
        }

        public void UnloadGame()
        {
            if(_currentGame == _rootGame)
                Exit();

            _nextGame = _rootGame;
        }

        protected override void Initialize()
        {
            if (ViewportAdapter == null)
                ViewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, (int)VirtualSize.X, (int)VirtualSize.Y);

            _rootGame?.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

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

            if(_currentGame?.RequestingExit ?? false)
                UnloadGame();
            
            base.Update(gameTime);
            
            if (_nextGame != null)
            {
                _currentGame.UnloadContent();
                
                _currentGame = _nextGame;
                _currentGame.LoadContent();
                _nextGame = null;
            }

            _currentGame?.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _currentGame?.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
