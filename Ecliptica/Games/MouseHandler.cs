using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ecliptica.Games
{
	public class MouseHandler
	{
		private MouseState _currentMouseState;
		private MouseState _previousMouseState;

		public void Update()
		{
			_previousMouseState = _currentMouseState;
			_currentMouseState = Mouse.GetState();
		}

		public bool IsLeftButtonClicked()
		{
			return _currentMouseState.LeftButton == ButtonState.Pressed &&
				   _previousMouseState.LeftButton == ButtonState.Released;
		}

		public bool IsLeftButtonReleased()
		{
			return _currentMouseState.LeftButton == ButtonState.Released &&
				   _previousMouseState.LeftButton == ButtonState.Pressed;
		}

		public bool IsMouseOver(Rectangle bounds)
		{
			Point mousePosition = _currentMouseState.Position;
			return bounds.Contains(mousePosition);
		}

		public Point GetMousePosition()
		{
			return _currentMouseState.Position;
		}

		public MouseState CurrentMouseState => _currentMouseState;
		public MouseState PreviousMouseState => _previousMouseState;
	}
}
