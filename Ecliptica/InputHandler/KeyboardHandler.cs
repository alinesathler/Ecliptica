using Microsoft.Xna.Framework.Input;

namespace Ecliptica.InputHandler
{
    public static class KeyboardHandler
    {
		#region Fields
		private static KeyboardState _keyboardState;
        private static KeyboardState _previousKeyState;
		#endregion

		#region Methods
		/// <summary>
		/// Method to update the keyboard state
		/// </summary>
		public static void Update()
        {
			_previousKeyState = _keyboardState;
			_keyboardState = Keyboard.GetState();
        }

		/// <summary>
		/// Method to check if a key is pressed once
		/// </summary>
		/// <param name="key"></param>
		/// <returns>Returntrue if key has just been pressed</returns>
		public static bool IsKeyPressed(Keys key)
        {
            return _keyboardState.IsKeyDown(key) && !_previousKeyState.IsKeyDown(key);
        }
		#endregion
	}
}
