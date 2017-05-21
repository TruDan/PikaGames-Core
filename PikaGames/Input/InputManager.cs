using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace PikaGames.Games.Core.Input
{
    public class InputManager
    {
        private KeyboardState _currentState;
        private KeyboardState _prevState;


        public void Update()
        {
            _prevState = _currentState;
            _currentState = Keyboard.GetState();
        }

        public bool IsPressed(Keys key)
        {
            if (_currentState.IsKeyDown(key) && _prevState.IsKeyUp(key))
                return true;
            
            return false;
        }

    }
}
