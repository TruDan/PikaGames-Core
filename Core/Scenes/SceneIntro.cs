﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Core.Managers;
using PikaGames.Games.PaperCast.Managers;

namespace PikaGames.Core.Scenes
{
    class SceneIntro : SceneBase
    {

        //--------------------------------------------------
        // Scene

        private const string PATH = "intro";

        //--------------------------------------------------
        // Textures

        private Texture2D _logoTexture;

        private Texture2D _monogameLogoTexture;

        private Texture2D _backgroundTexture;

        //--------------------------------------------------
        // Phase related

        private const float InitialInterval = 2000.0f;
        private float _currentTick;
        private int _phase;

        //--------------------------------------------------
        // Logo frames

        private const float LogoInterval = 2500.0f;
        private const int LogoFramesCount = 1;
        private const int LogoFrameInterval = 5;
        private const float FramesInterval = 70.0f;
        private int _currentFrame;
        private float _logoAlpha;

        //--------------------------------------------------
        // Monogame Image

        private const float MonogameInterval = 2500.0f;
        private float _monogameAlpha;

        //--------------------------------------------------
        // SEs

        private SoundEffect _logoSe;
        private SoundEffect _monogameSe;

        private bool _logoPlayed;
        private bool _monogamePlayed;

        //----------------------//------------------------//

        public override void LoadContent()
        {
            base.LoadContent();

            _backgroundTexture = new Texture2D(SceneManager.Instance.GraphicsDevice, 1, 1);
            _backgroundTexture.SetData(new Color[] { new Color(18, 18, 20) });

            // Load the logo sequence
            _logoTexture = ImageManager.loadScene(PATH, "Logo");

            // Load MonoGame logo
            _monogameLogoTexture = ImageManager.loadScene(PATH, "Monogame");

            // Load SEs
            _logoSe = SoundManager.LoadSe("Logo");
            _monogameSe = SoundManager.LoadSe("Monogame");

            // Load game settings
            //SettingsManager.Instance.LoadSettings();
        }

        public override void UnloadContent()
        {
            _backgroundTexture.Dispose();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            switch (_phase)
            {
                case 0:
                    HandleInitialInterval(deltaTime);
                    break;
                case 1:
                    HandleInitialLogoFrames(deltaTime);
                    break;
                case 2:
                    HandleLogoInterval(deltaTime);
                    break;
                case 3:
                    HandleLastLogoFrames(deltaTime);
                    break;
                case 4:
                    HandleAlphaIncrease(deltaTime);
                    break;
                case 5:
                    HandleFinalInterval(deltaTime);
                    break;
            }

            DebugValues["phase"] = _phase.ToString();
        }

        private void HandleInitialInterval(float deltaTime)
        {
            _currentTick += deltaTime;
            if (_currentTick >= InitialInterval)
            {
                _phase++;
                _currentTick = 0;
                _logoAlpha = 1.0f;
            }
        }

        private void HandleInitialLogoFrames(float deltaTime)
        {
            if (!_logoPlayed)
            {
                _logoSe.Play();
                _logoPlayed = true;
            }
            _currentTick += deltaTime;
            if (_currentTick >= FramesInterval)
            {
                _currentTick = 0;
                if (_currentFrame >= LogoFrameInterval)
                {
                    _phase++;
                    _currentTick = 0;
                }
                else
                {
                    _currentFrame++;
                }
            }
        }

        private void HandleLogoInterval(float deltaTime)
        {
            _currentTick += deltaTime;
            if (_currentTick >= LogoInterval)
            {
                _phase++;
                _currentTick = 0;
            }
        }

        private void HandleLastLogoFrames(float deltaTime)
        {
            if (!_monogamePlayed)
            {
                _monogameSe.Play();
                _monogamePlayed = true;
            }

            _currentTick += deltaTime;
            if (_currentTick >= FramesInterval)
            {
                _currentTick = 0;
                if (_currentFrame >= LogoFramesCount - 1)
                {
                    _phase++;
                    _currentTick = 0;
                    _logoAlpha = 0.0f;
                }
                else
                {
                    _currentFrame++;
                }
            }
        }

        private void HandleAlphaIncrease(float deltaTime)
        {
            _monogameAlpha += deltaTime / 1500;
            if (_monogameAlpha >= 1.0)
            {
                _phase++;
            }
        }

        private void HandleFinalInterval(float deltaTime)
        {
            _currentTick += deltaTime;
            if (_currentTick >= MonogameInterval && !SceneManager.Instance.IsTransitioning)
            {
                SceneManager.Instance.ChangeScene("SceneTitle");
            }
        }

        public override void Draw(SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            base.Draw(spriteBatch, viewportAdapter);

            var screenSize = SceneManager.Instance.VirtualSize;
            var screenRect = new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y);

            spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_backgroundTexture, screenRect, Color.White);
            spriteBatch.Draw(_logoTexture, Vector2.Zero, Color.White * _logoAlpha);
            spriteBatch.Draw(_monogameLogoTexture, Vector2.Zero, Color.White * _monogameAlpha);
            spriteBatch.End();
        }
    }
}
