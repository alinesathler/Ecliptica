using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Ecliptica.Screens
{
	public class ScreenManager
	{
		#region Fields
		private readonly static Stack<Screen> _screenStack;
		#endregion

		#region Events
		public static event Action OnScreenPopped;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor to initialize the screen manager
		/// </summary>
		static ScreenManager()
		{
			_screenStack = new Stack<Screen>();
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to push a screen to the stack
		/// </summary>
		/// <param name="screen"></param>
		public static void PushScreen(Screen screen)
		{
			_screenStack.Push(screen);

			screen.Load(false);
		}

		/// <summary>
		/// Method to pop a screen from the stack
		/// </summary>
		public static void PopScreen()
		{
			if (_screenStack.Count > 0)
			{
				OnScreenPopped?.Invoke();
				
				_screenStack.Pop();

				if (_screenStack.Count > 0)
				{
					_screenStack.Peek().Load(false);
				}
			}
		}

		/// <summary>
		/// Method to replace the current screen with a new screen
		/// </summary>
		/// <param name="screen"></param>
		public static void ReplaceScreen(Screen screen)
		{
			if (_screenStack.Count > 0)
			{
				_screenStack.Pop();
			}

			_screenStack.Push(screen);

			screen.Load(true);
		}

		/// <summary>
		/// Method to push pause screen
		/// </summary>
		/// <param name="screen"></param>
		public static void Pause(Screen screen)
		{
			_screenStack.Push(screen);
		}

		/// <summary>
		/// Method to update the screen manager
		/// </summary>
		/// <param name="gameTime"></param>
		public static void Update(GameTime gameTime)
		{
			if (_screenStack.Count > 0)
			{
				_screenStack.Peek().Update(gameTime);
			}
		}

		/// <summary>
		/// Method to draw the screen manager
		/// </summary>
		/// <param name="spriteBatch"></param>
		public static void Draw(SpriteBatch spriteBatch)
		{
			if (_screenStack.Count > 0)
			{
				_screenStack.Peek().Draw(spriteBatch);
			}
		}

		/// <summary>
		/// Method to get the current screen
		/// </summary>
		/// <returns>The current screen</returns>
		public static Screen CurrentScreen() => _screenStack.Count > 0 ? _screenStack.Peek() : null;
		#endregion
	}
}
