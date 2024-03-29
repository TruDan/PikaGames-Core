﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.NuclexGui;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core.Entities;
using PikaGames.Games.Core.Gui;
using PikaGames.Games.Core.Gui.Pika;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.Scenes;
using PikaGames.Games.Core.Sound;
using PikaGames.Games.Core.UI;
using PikaGames.Games.Core.UI.Dialog;
using PikaGames.Games.Core.Utils;

namespace PikaGames.Games.Core
{
    public class GameBase : IDisposable
    {
        public static readonly Color[] PlayerColors = new Color[]
        {
            MaterialDesignColors.LightBlue.GetVariant(MaterialThemeVariant.Base),
            MaterialDesignColors.LightGreen.GetVariant(MaterialThemeVariant.Base),
            MaterialDesignColors.Purple.GetVariant(MaterialThemeVariant.Base),
            MaterialDesignColors.Orange.GetVariant(MaterialThemeVariant.Base)
        };

        public static GameBase Instance { get; private set; }

        public RootGame Game { get; private set; }

        public event Action<GameBase> Exiting;

        public ContentManager Content => Game.Content;
        public GameWindow Window => Game.Window;
        public GraphicsDevice GraphicsDevice => Game?.GraphicsDevice ?? RootGame.Instance.GraphicsDevice;

        public SoundManager SoundManager => Game.SoundManager;
        protected GameServiceContainer Services => Game.Services;

        public Vector2 WindowSize => Game.WindowSize;
        public Vector2 VirtualSize => Game.VirtualSize;

        public GuiManager GuiManager => Game.GuiManager;

        public BoxingViewportAdapter ViewportAdapter => Game.ViewportAdapter;
        public ContentManager ContentManager;

        public bool RequestingExit = false;
        private bool _exited = false;

        public SceneManager SceneManager;

        public readonly List<Player> Players = new List<Player>();

        internal UiDialogContainer DialogContainer { get; }

        public SpriteBatch SpriteBatch { get; private set; }

        public GameBase(RootGame rootGame)
        {
            if (rootGame == null)
            {
                Game = new RootGame();
            }
            else
            {
                Game = rootGame;
            }

            Game.Assign(this);
            
            Instance = this;

            Content.RootDirectory = "Content";
            ContentManager = new ContentManager(Content.ServiceProvider, "Content");

            DialogContainer = new UiDialogContainer((int) VirtualSize.X, (int)VirtualSize.Y);
        }

        public GameBase() : this(RootGame.Instance) { }
        
        public virtual Player CreatePlayer(PlayerIndex playerIndex)
        {
            return new Player(TextureUtils.CreateRectangle(64, 64, Color.White), playerIndex);
        }

        public Player AddPlayer(Player player)
        {
            Players.Add(player);
            return player;
        }

        public void InitialiseLocalMultiplayer(int max = 4)
        {
            AddPlayer(CreatePlayer(PlayerIndex.One));

            if (max > 1)
                AddPlayer(CreatePlayer(PlayerIndex.Two));
            if (max > 2)
                AddPlayer(CreatePlayer(PlayerIndex.Three));
            if (max > 3)
                AddPlayer(CreatePlayer(PlayerIndex.Four));
        }

        protected internal virtual void Initialize()
        {
            Resources.Init(ContentManager, GraphicsDevice);
            TextureUtils.Init(GraphicsDevice);

            SceneManager = new SceneManager(this);
        }

        protected internal virtual void LoadContent()
        {
            _exited = false;
            Instance = this;

            if(SpriteBatch == null)
                SpriteBatch = new SpriteBatch(GraphicsDevice);

            SceneManager.LoadContent(Content);
        }

        protected internal virtual void UnloadContent()
        {
            //SceneManager.UnloadContent();
        }

        protected internal virtual void Update(GameTime gameTime)
        {
            if (RequestingExit)
                Exit();

            foreach (var player in Players)
            {
                player.Update(gameTime);
            }

            SceneManager.Update(gameTime);

            DialogContainer.Update(gameTime);
        }

        protected internal virtual void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SceneManager.Draw(gameTime, SpriteBatch);


            SpriteBatch.Begin(transformMatrix: ViewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);
            DialogContainer.Draw(SpriteBatch);
            SpriteBatch.End();;
        }

        public void Exit()
        {
            if (_exited) return;
            _exited = true;

            RequestingExit = true;
            Exiting?.Invoke(this);
        }

        public void Dispose()
        {
            ContentManager?.Dispose();
            SpriteBatch?.Dispose();
        }
    }
}
