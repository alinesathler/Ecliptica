using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Ecliptica.Games
{
	public class ScreenManager
	{
		private static Stack<Screen> _screenStack;
		private SpriteBatch _spriteBatch;

		static ScreenManager()
		{
			_screenStack = new Stack<Screen>();
		}

		public static void PushScreen(Screen screen)
		{
			_screenStack.Push(screen);

			screen.Load(false);
		}

		public static void PopScreen()
		{

			if (_screenStack.Count > 0)
			{
				Screen screen = _screenStack.Pop();

				if (_screenStack.Count > 0)
				{
					_screenStack.Peek().Load(false);
				}
			}

		}

		public static void ReplaceScreen(Screen screen)
		{
			if (_screenStack.Count > 0)
			{
				_screenStack.Pop();
			}

			_screenStack.Push(screen);

			screen.Load(true);
		}

		public static void Update(GameTime gameTime)
		{
			if (_screenStack.Count > 0)
			{
				_screenStack.Peek().Update(gameTime);
			}
		}

		public static void Draw(SpriteBatch spriteBatch)
		{
			if (_screenStack.Count > 0)
			{
				_screenStack.Peek().Draw(spriteBatch);
			}
		}

		public static Screen CurrentScreen() => _screenStack.Count > 0 ? _screenStack.Peek() : null;
	}
}
