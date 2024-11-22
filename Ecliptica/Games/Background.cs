using Ecliptica.Arts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Games
{
    internal class Background
	{
		private static Texture2D BackgroundSolid;
		private static Texture2D BackgroundStars;

		// Scrolling star background
		private static float _starOffset = 0f;
		private static float _starSpeed = -50f;

		public static void Load(Texture2D backgroundSolid, Texture2D backgroundStars)
		{
			BackgroundSolid = backgroundSolid;
			BackgroundStars = backgroundStars;
		}

		/// <summary>
		/// Method to update the background
		/// </summary>
		/// <param name="gameTime"></param>
		public static void Update(GameTime gameTime)
		{
			// Update star offset for scrolling effect
			_starOffset += (float)gameTime.ElapsedGameTime.TotalSeconds * _starSpeed;
			if (_starOffset > BackgroundStars.Height)
			{
				_starOffset -= BackgroundStars.Height;
			} else
			{
				if (_starOffset < -BackgroundStars.Height)
				{
					_starOffset += BackgroundStars.Height;
				}

			}
		}

		/// <summary>
		/// Method to draw the background
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="graphicsDevice"></param>
		public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
		{
			graphicsDevice.Clear(Color.Black);

			// Draw the static blue background
			spriteBatch.Draw(
				BackgroundSolid,
				new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height),
				Color.White
			);

			// Draw scrolling stars
			spriteBatch.Draw(
				BackgroundStars,
				new Rectangle(0, (int)-_starOffset, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height),
				Color.White
			);

			// Draw second star layer
			spriteBatch.Draw(
				BackgroundStars,
				new Rectangle(0, (int)(-BackgroundStars.Height - _starOffset), graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height),
				Color.White
			);
		}
	}
}
