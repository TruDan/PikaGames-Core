using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PikaGames.Games.Core;
using PikaGames.Games.Core.Gui;

namespace ClientLauncher.UI
{
    public class GamePadStatus
    {
        private bool _pad1Connected = false;
        private bool _pad2Connected = false;
        private bool _pad3Connected = false;
        private bool _pad4Connected = false;

        private readonly GameBase _game;

        public GamePadStatus(GameBase game)
        {
            _game = game;
        }

        public void Update(GameTime gameTime)
        {
            _pad1Connected = _game.Players[(int)PlayerIndex.One]?.Input.IsGamePadConnected() ?? false;
            _pad2Connected = _game.Players[(int)PlayerIndex.Two]?.Input.IsGamePadConnected() ?? false;
            _pad3Connected = _game.Players[(int)PlayerIndex.Three]?.Input.IsGamePadConnected() ?? false;
            _pad4Connected = _game.Players[(int)PlayerIndex.Four]?.Input.IsGamePadConnected() ?? false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var width = _game.VirtualSize.X / 4;

            width = 50;

            var x = 25f;
            var y = _game.VirtualSize.Y - 75f;

            spriteBatch.DrawIcon(IconsMap.Gamepad1, new Vector2(x, y), _pad1Connected ? Color.White : Color.White*0.25f);

            x += width + 25f;
            spriteBatch.DrawIcon(IconsMap.Gamepad2, new Vector2(x, y), _pad2Connected ? Color.White : Color.White * 0.25f);

            x += width + 25f;
            spriteBatch.DrawIcon(IconsMap.Gamepad3, new Vector2(x, y), _pad3Connected ? Color.White : Color.White * 0.25f);

            x += width + 25f;
            spriteBatch.DrawIcon(IconsMap.Gamepad4, new Vector2(x, y), _pad4Connected ? Color.White : Color.White * 0.25f);
        }

    }
}
