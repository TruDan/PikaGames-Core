using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Players;
using PikaGames.Games.Core.Scenes;

namespace PikaGames.Games.Core
{
    public class GameBase : Game
    {
        
        public Vector2 WindowSize = new Vector2(720*2, 480*2);
        public Vector2 VirtualSize = new Vector2(720*2, 480*2);

        public BoxingViewportAdapter ViewportAdapter;
        public ContentManager ContentManager;

        public bool RequestingExit = false;

        public SceneManager SceneManager;

        public readonly List<Player> Players = new List<Player>();

        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        public SpriteBatch SpriteBatch { get; private set; }

        public GameBase()
        {
            Content.RootDirectory = "Content";
            ContentManager = new ContentManager(Content.ServiceProvider, "Content");


            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            _graphicsDeviceManager.PreferredBackBufferWidth = (int) WindowSize.X;
            _graphicsDeviceManager.PreferredBackBufferHeight = (int)WindowSize.Y;
            _graphicsDeviceManager.ApplyChanges();

            Window.AllowUserResizing = false;
            IsMouseVisible = true;


        }

        public Player AddPlayer(Player player)
        {
            Players.Add(player);
            return player;
        }

        protected override void Initialize()
        {
            ViewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, (int)VirtualSize.X, (int)VirtualSize.Y);
            
            Resources.Init(ContentManager, GraphicsDevice);

            SceneManager = new SceneManager(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            SceneManager.LoadContent(Content);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {

            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (RequestingExit)
                Exit();

            SceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SceneManager.Draw(gameTime, SpriteBatch);

            base.Draw(gameTime);
        }
    }
}
