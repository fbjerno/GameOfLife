using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class InputManager
    {
        public KeyboardState KeyboardState { get; private set; }
        public KeyboardState LastKeyboardState { get; private set; }

        public MouseState MouseState { get; private set; }
        public MouseState LastMouseState { get; private set; }

        public bool LeftMousePressed => MouseState.LeftButton == ButtonState.Pressed;
        public bool LeftMouseReleased => MouseState.LeftButton == ButtonState.Released;
        public bool LeftMouseClicked => MouseState.LeftButton == ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Released;
        public bool RightMousePressed => MouseState.RightButton == ButtonState.Pressed;
        public bool RightMouseReleased => MouseState.RightButton == ButtonState.Released;
        public bool RightMouseClicked => MouseState.RightButton == ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Released;
        public int MouseX => MouseState.X;
        public int MouseY => MouseState.Y;
        public Point MousePosition => MouseState.Position;

        public void Update()
        {
            LastKeyboardState = KeyboardState;
            LastMouseState = MouseState;

            KeyboardState = Keyboard.GetState();
            MouseState = Mouse.GetState();
        }

        public bool KeyPressed(Keys key, bool lastFrame = false)
        {
            if (lastFrame)
                return LastKeyboardState.IsKeyDown(key);
            return KeyboardState.IsKeyDown(key);
        }

        public bool KeyReleased(Keys key, bool lastFrame = false)
        {
            if (lastFrame)
                return LastKeyboardState.IsKeyUp(key);
            return KeyboardState.IsKeyUp(key);
        }

        public bool KeyClicked(Keys key)
        {
            if (KeyboardState.IsKeyDown(key) && LastKeyboardState.IsKeyUp(key))
                return true;
            return false;
        }
    }
}
