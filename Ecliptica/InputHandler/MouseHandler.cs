using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace Ecliptica.InputHandler
{
	public static class MouseHandler
	{
		private static MouseState _currentState;
		private static MouseState _previousState;

		public static Point Position => _currentState.Position;
		public static bool IsLeftButtonPressed => _currentState.LeftButton == ButtonState.Pressed;
		public static bool IsLeftButtonReleased => _previousState.LeftButton == ButtonState.Pressed && _currentState.LeftButton == ButtonState.Released;
		public static bool IsLeftButtonDown => _currentState.LeftButton == ButtonState.Pressed;
		public static bool IsRightButtonPressed => _currentState.RightButton == ButtonState.Pressed;
		public static bool IsRightButtonReleased => _previousState.RightButton == ButtonState.Pressed && _currentState.RightButton == ButtonState.Released;

		/// <summary>
		/// Method to update the mouse state
		/// </summary>
		public static void Update()
		{
			_previousState = _currentState;
			_currentState = Mouse.GetState();
		}

		/// <summary>
		/// Method to check if the mouse is hovering over a rectangle
		/// </summary>
		public static bool IsHovering(Rectangle bounds)
		{
			return bounds.Contains(Position);
		}

		/// <summary>
		/// Method to check if the left mouse button is clicked
		/// </summary>
		public static bool IsLeftClick(Rectangle bounds)
		{
			return IsHovering(bounds) && IsLeftButtonReleased;
		}

		//public static int ScrollDelta => _currentState.ScrollWheelValue - _previousState.ScrollWheelValue;

		//public static void Reset()
		//{
		//	_previousState = _currentState = Mouse.GetState();
		//}
	}
}
