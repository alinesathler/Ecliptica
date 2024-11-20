using Ecliptica.Arts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
			//_spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		public static void PushScreen(Screen screen)
		{
			if (_screenStack.Count > 0)
			{
				Sounds.StopMusic();
			}

			_screenStack.Push(screen);
			screen.PlayMusic();
		}

		public static void PopScreen()
		{
			if (_screenStack.Count > 0)
			{
				Screen screen = _screenStack.Pop();
				Sounds.StopMusic();

				if (_screenStack.Count > 0)
				{
					_screenStack.Peek().PlayMusic();
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

			screen.PlayMusic();
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
	}
}
