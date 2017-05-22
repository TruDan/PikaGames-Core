using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PikaGames.Games.Core.Input;

namespace PikaGames.Games.Core.Entities
{
    public class Player : Entity
    {
        private readonly Texture2D _texture;

        public PlayerInputManager Input { get; }

        public virtual bool IsConnected => Input.UsesKeyboard || (Input.UsesGamePad && Input.IsGamePadConnected());

        public Player(Texture2D texture, PlayerIndex playerIndex)
        {
            _texture = texture;

            Input = new PlayerInputManager(playerIndex, InputType.GamePad);

            if(playerIndex == PlayerIndex.One)
                Input.UsesKeyboard = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Input.Update();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

    }
}
