using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    public class CustomKeyboard
    {
        private KeyboardState _prevKeyboardState;
        private KeyboardState _keyboardState;

        private MouseState _mouseState;
        private MouseState _prevMouseState;

        public bool IsLeftMouseDown()
        {
            return _mouseState.LeftButton == ButtonState.Pressed;
        }
        public bool JustLeftMouseDown()
        {
            return _mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton != ButtonState.Pressed;
        }
        public bool IsKeyDown(Keys key)
        {
            return _keyboardState.IsKeyDown(key);
        }
        public bool JustPressed(Keys key)
        {
            return _keyboardState.IsKeyDown(key) && !_prevKeyboardState.IsKeyDown(key);
        }
        public void GetKeyboardState()
        {
            _prevMouseState = _mouseState;
            _mouseState = Mouse.GetState();

            _prevKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();
        }
        public bool IsOver(Rectangle r)
        {
            return r.Contains(new Vector2(_mouseState.X, _mouseState.Y));
        }
    }
}
