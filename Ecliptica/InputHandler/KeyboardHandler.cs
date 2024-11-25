using Microsoft.Xna.Framework.Input;

namespace Ecliptica.InputHandler
{
    public static class KeyboardHandler
    {
        private static KeyboardState keyboardState;
        private static KeyboardState previousKeyState;

        public static void Update()
        {
            previousKeyState = keyboardState;
            keyboardState = Keyboard.GetState();
        }

        public static bool IsKeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }
    }
}
