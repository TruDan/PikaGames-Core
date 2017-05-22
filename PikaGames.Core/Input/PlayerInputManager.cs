using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PikaGames.Games.Core.Input
{
    public class PlayerInputManager
    {
        public bool AnalogDirection = true;

        // Keyboard
        // -------------------------------------------
        public bool UsesKeyboard = false;

        private KeyboardState _currentKeyState, _prevKeyState;

        private readonly IDictionary<InputCommand, Keys[]> _keyMap = new Dictionary<InputCommand, Keys[]>()
        {
            {InputCommand.Up, new []{Keys.W, Keys.Up}},
            {InputCommand.Down, new []{Keys.S, Keys.Down}},
            {InputCommand.Left, new []{Keys.A, Keys.Left}},
            {InputCommand.Right, new []{Keys.D, Keys.Right}},

            {InputCommand.A, new []{ Keys.Q, Keys.Enter }},
            {InputCommand.B, new []{ Keys.E, Keys.Back }},
            {InputCommand.X, new []{ Keys.F }},
            {InputCommand.Y, new []{ Keys.G }},
            {InputCommand.Start, new []{ Keys.Escape }},
        };

        // GamePad
        // -------------------------------------------
        private const float ThumbstickTolerance = 0.5f;

        public bool UsesGamePad = false;
        public PlayerIndex PlayerIndex { get; private set; }

        private GamePadState _currentPadState, _prevPadState;
        private GamePadCapabilities _padCapabilities;

        private readonly IDictionary<InputCommand, Buttons[]> _buttonMap = new Dictionary<InputCommand, Buttons[]>()
        {
            {InputCommand.Up, new []{Buttons.LeftThumbstickUp, Buttons.DPadUp}},
            {InputCommand.Down, new []{Buttons.LeftThumbstickDown, Buttons.DPadDown}},
            {InputCommand.Left, new []{Buttons.LeftThumbstickLeft, Buttons.DPadLeft}},
            {InputCommand.Right, new []{Buttons.LeftThumbstickRight, Buttons.DPadRight}},

            {InputCommand.A, new []{ Buttons.A }},
            {InputCommand.B, new []{ Buttons.B }},
            {InputCommand.X, new []{ Buttons.X }},
            {InputCommand.Y, new []{ Buttons.Y }},
            {InputCommand.Start, new []{ Buttons.Start }},
        };

        public PlayerInputManager(PlayerIndex playerIndex, InputType inputType)
        {
            PlayerIndex = playerIndex;

            if (inputType == InputType.Keyboard)
                UsesKeyboard = true;
            else if (inputType == InputType.GamePad)
                UsesGamePad = true;
        }


        public void Update()
        {
            if (UsesKeyboard)
            {
                _prevKeyState = _currentKeyState;
                _currentKeyState = Keyboard.GetState();
            }

            if (UsesGamePad)
            {
                _prevPadState = _currentPadState;
                _padCapabilities = GamePad.GetCapabilities(PlayerIndex);
                _currentPadState = GamePad.GetState(PlayerIndex);
            }
        }

        public Vector2 GetDirectionVector()
        {
            Vector2 direction = Vector2.Zero;

            if (UsesKeyboard)
            {
                var upDown = IsKeyDown(GetKeyBinds(InputCommand.Up));
                var downDown = IsKeyDown(GetKeyBinds(InputCommand.Down));
                var leftDown = IsKeyDown(GetKeyBinds(InputCommand.Left));
                var rightDown = IsKeyDown(GetKeyBinds(InputCommand.Right));

                if (upDown && !downDown)
                {
                    direction.X = 1;
                }
                else if (!upDown && downDown)
                {
                    direction.X = -1;
                }
                else if (upDown && downDown)
                {
                    direction.X = 0;
                }

                if (leftDown && !rightDown)
                {
                    direction.Y = 1;
                }
                else if (!leftDown && rightDown)
                {
                    direction.Y = -1;
                }
                else if (leftDown && rightDown)
                {
                    direction.Y = 0;
                }
            }

            if (UsesGamePad)
            {
                var padDirection = GetPadDirectionVector();
                if (Math.Abs(padDirection.X) > ThumbstickTolerance || Math.Abs(padDirection.Y) > ThumbstickTolerance)
                {
                    direction = padDirection;
                }
            }

            if (AnalogDirection)
            {
                direction.X = (float)Math.Round(direction.X);
                direction.Y = (float)Math.Round(direction.Y);
            }

            return direction;
        }

        public bool IsPressed(params InputCommand[] commands)
        {
            foreach (var command in commands)
            {
                if (UsesKeyboard)
                {
                    if (IsKeyPressed(GetKeyBinds(command)))
                        return true;
                }

                if (UsesGamePad)
                {
                    if (IsButtonPressed(GetButtonBinds(command)))
                        return true;
                }
            }

            return false;
        }

        public bool IsReleased(params InputCommand[] commands)
        {
            foreach (var command in commands)
            {
                if (UsesKeyboard)
                {
                    if (IsKeyReleased(GetKeyBinds(command)))
                        return true;
                }

                if (UsesGamePad)
                {
                    if (IsButtonReleased(GetButtonBinds(command)))
                        return true;
                }
            }

            return false;
        }

        public bool IsDown(params InputCommand[] commands)
        {
            foreach (var command in commands)
            {
                if (UsesKeyboard)
                {
                    if (IsKeyDown(GetKeyBinds(command)))
                        return true;
                }

                if (UsesGamePad)
                {
                    if (IsButtonDown(GetButtonBinds(command)))
                        return true;
                }
            }

            return false;
        }

        public bool IsUp(params InputCommand[] commands)
        {
            foreach (var command in commands)
            {
                if (UsesKeyboard)
                {
                    if (IsKeyUp(GetKeyBinds(command)))
                        return true;
                }

                if (UsesGamePad)
                {
                    if (IsButtonUp(GetButtonBinds(command)))
                        return true;
                }
            }
            return false;
        }


        // Keyboard
        // -------------------------------------------

        public Keys[] GetKeyBinds(InputCommand command)
        {
            Keys[] keys;
            if (_keyMap.TryGetValue(command, out keys))
            {
                return keys;
            }
            return new Keys[0];
        }

        protected bool IsKeyPressed(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (_currentKeyState.IsKeyDown(key) && _prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        protected bool IsKeyReleased(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (_currentKeyState.IsKeyUp(key) && _prevKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        protected bool IsKeyDown(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (_currentKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        protected bool IsKeyUp(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (_currentKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }


        // GamePad
        // -------------------------------------------

        public Buttons[] GetButtonBinds(InputCommand command)
        {
            Buttons[] buttons;
            if (_buttonMap.TryGetValue(command, out buttons))
            {
                return buttons;
            }
            return new Buttons[0];
        }

        public bool IsGamePadConnected()
        {
            return _padCapabilities.IsConnected;
        }

        protected bool IsButtonPressed(params Buttons[] buttons)
        {
            if (!IsGamePadConnected()) return false;

            foreach (var button in buttons)
            {
                if (IsThumbstickButton(button))
                {
                    var prevDirection = GetPadDirection(_prevPadState);
                    var currentDirection = GetPadDirection(_currentPadState);

                    switch (button)
                    {
                        case Buttons.LeftThumbstickUp:
                        case Buttons.RightThumbstickUp:
                            return currentDirection == Buttons.DPadUp && prevDirection != Buttons.DPadUp;

                        case Buttons.LeftThumbstickDown:
                        case Buttons.RightThumbstickDown:
                            return currentDirection == Buttons.DPadDown && prevDirection != Buttons.DPadDown;

                        case Buttons.LeftThumbstickLeft:
                        case Buttons.RightThumbstickLeft:
                            return currentDirection == Buttons.DPadLeft && prevDirection != Buttons.DPadLeft;

                        case Buttons.LeftThumbstickRight:
                        case Buttons.RightThumbstickRight:
                            return currentDirection == Buttons.DPadRight && prevDirection != Buttons.DPadRight;
                    }
                }
                else if (_currentPadState.IsButtonDown(button) && _prevPadState.IsButtonUp(button))
                    return true;
            }

            return false;
        }

        protected bool IsButtonReleased(params Buttons[] buttons)
        {
            if (!IsGamePadConnected()) return false;

            foreach (var button in buttons)
            {
                if (IsThumbstickButton(button))
                {
                    var prevDirection = GetPadDirection(_prevPadState);
                    var currentDirection = GetPadDirection(_currentPadState);

                    switch (button)
                    {
                        case Buttons.LeftThumbstickUp:
                        case Buttons.RightThumbstickUp:
                            return currentDirection != Buttons.DPadUp && prevDirection == Buttons.DPadUp;

                        case Buttons.LeftThumbstickDown:
                        case Buttons.RightThumbstickDown:
                            return currentDirection != Buttons.DPadDown && prevDirection == Buttons.DPadDown;

                        case Buttons.LeftThumbstickLeft:
                        case Buttons.RightThumbstickLeft:
                            return currentDirection != Buttons.DPadLeft && prevDirection == Buttons.DPadLeft;

                        case Buttons.LeftThumbstickRight:
                        case Buttons.RightThumbstickRight:
                            return currentDirection != Buttons.DPadRight && prevDirection == Buttons.DPadRight;
                    }
                }
                else if (_currentPadState.IsButtonUp(button) && _prevPadState.IsButtonDown(button))
                    return true;
            }
            return false;
        }

        protected bool IsButtonDown(params Buttons[] buttons)
        {
            if (!IsGamePadConnected()) return false;

            foreach (var button in buttons)
            {
                if (IsThumbstickButton(button))
                {
                    var currentDirection = GetPadDirection(_currentPadState);

                    switch (button)
                    {
                        case Buttons.LeftThumbstickUp:
                        case Buttons.RightThumbstickUp:
                            return currentDirection == Buttons.DPadUp;

                        case Buttons.LeftThumbstickDown:
                        case Buttons.RightThumbstickDown:
                            return currentDirection == Buttons.DPadDown;

                        case Buttons.LeftThumbstickLeft:
                        case Buttons.RightThumbstickLeft:
                            return currentDirection == Buttons.DPadLeft;

                        case Buttons.LeftThumbstickRight:
                        case Buttons.RightThumbstickRight:
                            return currentDirection == Buttons.DPadRight;
                    }
                }
                else if (_currentPadState.IsButtonDown(button))
                    return true;
            }
            return false;
        }

        protected bool IsButtonUp(params Buttons[] buttons)
        {
            if (!IsGamePadConnected()) return false;

            foreach (var button in buttons)
            {

                if (IsThumbstickButton(button))
                {
                    var currentDirection = GetPadDirection(_currentPadState);

                    switch (button)
                    {
                        case Buttons.LeftThumbstickUp:
                        case Buttons.RightThumbstickUp:
                            return currentDirection != Buttons.DPadUp;

                        case Buttons.LeftThumbstickDown:
                        case Buttons.RightThumbstickDown:
                            return currentDirection != Buttons.DPadDown;

                        case Buttons.LeftThumbstickLeft:
                        case Buttons.RightThumbstickLeft:
                            return currentDirection != Buttons.DPadLeft;

                        case Buttons.LeftThumbstickRight:
                        case Buttons.RightThumbstickRight:
                            return currentDirection != Buttons.DPadRight;
                    }
                }
                else if (_currentPadState.IsButtonUp(button))
                    return true;
            }
            return false;
        }
        
        protected Vector2 GetPadDirectionVector()
        {
            return _currentPadState.ThumbSticks.Left;
        }

        private Buttons GetPadDirection(GamePadState state)
        {
            Vector2 direction = state.ThumbSticks.Left;

            float absX = Math.Abs(direction.X);
            float absY = Math.Abs(direction.Y);

            if (absX > absY && absX > ThumbstickTolerance)
            {
                return direction.X > 0 ? Buttons.DPadRight : Buttons.DPadLeft;
            }
            else if (absX < absY && absY > ThumbstickTolerance)
            {
                return direction.Y > 0 ? Buttons.DPadUp : Buttons.DPadDown;
            }
            return 0;
        }

        private bool IsThumbstickButton(Buttons button)
        {
            return button == Buttons.LeftThumbstickUp || button == Buttons.LeftThumbstickDown ||
                   button == Buttons.LeftThumbstickLeft || button == Buttons.LeftThumbstickRight ||
                   button == Buttons.RightThumbstickUp || button == Buttons.RightThumbstickDown ||
                   button == Buttons.RightThumbstickLeft || button == Buttons.RightThumbstickRight;
        }


        public bool IsDirectionalCommand(InputCommand command)
        {
            return command == InputCommand.Up || command == InputCommand.Down ||
                   command == InputCommand.Left || command == InputCommand.Right;
        }
    }
}
