using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Games
{
	internal class Background
	{
		public static Texture2D BackgroundBlue { get; private set; }
		public static Texture2D BackgroundStars { get; private set; }

		// Scrolling star background
		private static float _starOffset = 0f;
		private static float _starSpeed = -50f;

		/// <summary>
		/// Method to load the background images
		/// </summary>
		/// <param name="content"></param>
		public static void Load(ContentManager content)
		{
			BackgroundBlue = content.Load<Texture2D>("Images/background-blue");
			BackgroundStars = content.Load<Texture2D>("Images/background-stars");
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
				_starOffset -= BackgroundStars.Height;
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
				BackgroundBlue,
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
