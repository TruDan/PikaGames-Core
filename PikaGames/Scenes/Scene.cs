using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace PikaGames.Games.Core.Scenes
{
    public class Scene
    {
        internal bool IsInitialised { get; set; }
        public bool IsActive { get; private set; }

        private FramesPerSecondCounter _fpsCounter;

        protected SceneManager SceneManager { get; private set; }
        protected GameBase Game { get; private set; }

        internal void Init(SceneManager sceneManager, GameBase game)
        {
            if (IsInitialised)
                return;

            IsInitialised = true;

            SceneManager = sceneManager;
            Game = game;

            Initialise();

        }

        protected virtual void Initialise() { }

        public virtual void LoadContent()
        {
            IsActive = true;
            _fpsCounter = new FramesPerSecondCounter();
        }

        public virtual void UnloadContent()
        {
            IsActive = false;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter) { }

    }
}
