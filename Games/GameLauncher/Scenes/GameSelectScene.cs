﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using PikaGames.Games.Core;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.Scenes;
using PikaGames.Games.Core.UI;
using PikaGames.Games.Core.UI.ButtonBar;
using PikaGames.Games.Core.UI.Menu;
using PikaGames.Games.Core.UI.Text;
using PikaGames.Games.Core.Utils;
using PikaGames.PaperCast;

namespace GameLauncher.Scenes
{
    public class GameSelectScene : Scene
    {

        private UiContainer _container;

        private UiText _title;
        private UiMenu _menu;
        private UiButtonBar _buttonBar;
        
        public override void LoadContent()
        {
            base.LoadContent();
            
            _container = new UiContainer();

            _title = new UiTitle(_container, 50, 50, "Game Select");

            _menu = new UiMenu(_container, 50, 50 + _title.Height + 50);
            
            _menu.AddMenuItem("PaperCast", () => LaunchGame(new PaperCastGame()));
            _menu.AddMenuItem("RacerCast", () => { });


            _buttonBar = new UiButtonBar(_container, (int)Game.VirtualSize.X - 25, (int)Game.VirtualSize.Y - 25);
            _buttonBar.AddButton(Buttons.A, "Select");
            _buttonBar.AddButton(Buttons.B, "Back");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            _container = null;
        }

        private void LaunchGame(GameBase game)
        {
            RootGame.Instance.LoadGame(game);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _container?.Update(gameTime);

            if (Game.Players.Any(p => p.Input.IsPressed(InputCommand.B)))
            {
                Game.SceneManager.Previous();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, ViewportAdapter viewportAdapter)
        {
            base.Draw(gameTime, spriteBatch, viewportAdapter);

            spriteBatch.Begin(transformMatrix: viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);

            _container?.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}