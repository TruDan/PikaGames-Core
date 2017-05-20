using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PikaGames.Games.PaperCast
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class PaperCastGame : Game
	{
		GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;

	    private Texture2D _background;

        public PaperCastGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.IsFullScreen = true;
            _graphics.SupportedOrientations = DisplayOrientation.Portrait;
        }
        
	    protected override void Initialize()
	    {
	        base.Initialize();
	    }

        protected override void LoadContent()
	    {
	        _spriteBatch = new SpriteBatch(GraphicsDevice);

	        _background = Content.Load<Texture2D>("background");
	    }

	    protected override void Draw(GameTime gameTime)
	    {
	        _graphics.GraphicsDevice.Clear(Color.SaddleBrown);

	        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap,
	            DepthStencilState.Default, RasterizerState.CullNone);

	        _spriteBatch.Draw(_background, Vector2.Zero, _graphics.GraphicsDevice.Viewport.Bounds, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

	        _spriteBatch.End();

	        base.Draw(gameTime);
	    }
    }
}