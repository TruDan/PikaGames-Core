using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.UI.Menu;
using PikaGames.Games.Core.Utils;

namespace PikaGames.Games.Core.UI.Controls
{
    public class UiSliderControl : UiControl
    {
        public float Step { get; set; } = 0.01f;

        public float Value { get; set; } = 0.5f;

        public override int Width { get; set; } = 400;

        private readonly Texture2D _background;
        private readonly Texture2D _backgroundActive;
        private readonly Texture2D _backgroundShadow;
        private readonly Texture2D _backgroundShadowActive;

        private readonly Texture2D _knob;
        private readonly Texture2D _knobActive;
        private readonly Texture2D _knobShadow;
        private readonly Texture2D _knobShadowActive;

        private Action<float> _onChangeAction;

        private Vector2 _knobPosition = Vector2.Zero;

        public UiSliderControl(UiMenu menu, int x, int y, string label, Action<float> onChangeAction = null) : base(menu, x, y, label)
        {
            _onChangeAction = onChangeAction;

            _background = TextureUtils.CreateRectangle(1, 1, UiTheme.ControlBackgroundColor);
            _backgroundActive = TextureUtils.CreateRectangle(1, 1, UiTheme.ControlActiveBackgroundColor);

            _backgroundShadow = TextureUtils.CreateRectangle(1, 1, UiTheme.ControlBackgroundShadowColor);
            _backgroundShadowActive = TextureUtils.CreateRectangle(1, 1, UiTheme.ControlActiveBackgroundShadowColor);

            _knob = TextureUtils.CreateRectangle(1, 1, UiTheme.ControlForegroundColor);
            _knobActive = TextureUtils.CreateRectangle(1, 1, UiTheme.ControlActiveForegroundColor);

            _knobShadow = TextureUtils.CreateRectangle(1, 1, UiTheme.ControlForegroundShadowColor);
            _knobShadowActive = TextureUtils.CreateRectangle(1, 1, UiTheme.ControlActiveForegroundShadowColor);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _knobPosition = Position + new Vector2(Width / 2f + (Width / 2f) * Value, 0);

            if (!IsSelected)
                return;

            if (GameBase.Instance.Players.Any(p => p.Input.IsDown(InputCommand.Left)))
            {
                Value -= Step;
                if (Value < 0)
                    Value = 0;

                _onChangeAction?.Invoke(Value);
            }
            else if (GameBase.Instance.Players.Any(p => p.Input.IsDown(InputCommand.Right)))
            {
                Value += Step;
                if (Value > 1)
                    Value = 1;

                _onChangeAction?.Invoke(Value);
            }

            _knobPosition = Position + new Vector2(Width / 2f + (Width / 2f) * Value, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Background
            
            for (int i = 0; i < (IsSelected ? UiTheme.ControlActiveBackgroundShadowSize : UiTheme.ControlBackgroundShadowSize); i++)
            {
                spriteBatch.Draw(IsSelected ? _backgroundShadowActive : _backgroundShadow, new Rectangle(Bounds.X + Width / 2 + i, Bounds.Y + i, Bounds.Width / 2, Bounds.Height), Color.White);
            }

            spriteBatch.Draw(IsSelected ? _backgroundActive : _background, new Rectangle(Bounds.X + Width/2, Bounds.Y, Bounds.Width/2, Bounds.Height), Color.White);
            

            // Slider Track



            // Slider Knob
            var shadowSize = (int)Math.Min(Bounds.X + Bounds.Width - _knobPosition.X + Bounds.Height,
                IsSelected ? UiTheme.ControlActiveForegroundShadowSize : UiTheme.ControlForegroundShadowSize);
            if (_knobPosition.X + Bounds.Height < Bounds.X + Bounds.Width)
            {

                for (int i = 0; i < shadowSize; i++)
                {
                    spriteBatch.Draw(IsSelected ? _knobShadowActive : _knobShadow, new Rectangle((int)_knobPosition.X - Bounds.Height / 2 + i, (int)_knobPosition.Y - shadowSize/2 + i, Bounds.Height, Bounds.Height), Color.White);
                }
            }

            spriteBatch.Draw(IsSelected ? _knobActive : _knob, new Rectangle((int)_knobPosition.X - Bounds.Height/2, (int)_knobPosition.Y - shadowSize/2, Bounds.Height, Bounds.Height), Color.White);

        }
    }
}
