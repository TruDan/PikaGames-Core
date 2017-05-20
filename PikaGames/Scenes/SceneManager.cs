using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.ViewportAdapters;

namespace PikaGames.Games.Core.Scenes
{
    public class SceneManager
    {
        public readonly GameBase Game;

        public bool DebugMode = true;
        
        public bool IsTransitioning { get { return _isTransitioning; } }

        public SplashScene SplashScene { get; }
        public Scene DefaultScene { get; set; }

        private Texture2D _background;

        private Scene _currentScene, _newScene;
        private Sprite _transitionImage;

        private bool _isTransitioning = false;
        private bool _beginTransitionFade = false;

        public SceneManager(GameBase game)
        {
            Game = game;
            SplashScene = new SplashScene();

            _currentScene = SplashScene;
            _currentScene.Init(this, Game);

            _background = Resources.Images.Background;
        }

        public void LoadContent(ContentManager content)
        {
            var transitionTexture = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            transitionTexture.SetData(new Color[] { Color.Black });
            _transitionImage = new Sprite(transitionTexture);
            _transitionImage.Scale = new Vector2(Game.VirtualSize.X, Game.VirtualSize.Y);
            _transitionImage.Alpha = 0.0f;
            _transitionImage.IsVisible = false;
            _currentScene.LoadContent();
        }

        public void ChangeScene(Scene newScene)
        {
            newScene.Init(this, Game);
            if (_isTransitioning) return;
            _newScene = newScene;
            _transitionImage.Alpha = 0;
            _transitionImage.IsVisible = true;
            _isTransitioning = true;
            _beginTransitionFade = true;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: Game.ViewportAdapter.GetScaleMatrix(), samplerState: SamplerState.LinearWrap);
            spriteBatch.Draw(_background, Vector2.Zero, Game.ViewportAdapter.BoundingRectangle, Color.White);
            spriteBatch.End();

            _currentScene.Draw(gameTime, spriteBatch, Game.ViewportAdapter);

            spriteBatch.Begin();
            spriteBatch.Draw(_transitionImage.TextureRegion.Texture, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), Color.White * _transitionImage.Alpha);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            if (_isTransitioning)
            {
                UpdateTransition(gameTime);
            }

            _currentScene.Update(gameTime);
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
