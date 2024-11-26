using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Games
{
	internal class LifeAsteroids (Texture2D lifeSpriteSheet, int rows, int cols)
	{
		#region Fields
		private readonly Texture2D _lifeSpriteSheet = lifeSpriteSheet;
		private readonly int _rows = rows;
		private readonly int _cols = cols;
		#endregion

		#region Methods
		/// <summary>
		/// Method to draw the astecoid life
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="location"></param>
		/// <param name="maxLifes"></param>
		/// <param name="currentLives"></param>
		public void Draw(SpriteBatch spriteBatch, Vector2 location, int maxLifes, int currentLives)
		{
			int width = _lifeSpriteSheet.Width / _cols;
			int height = _lifeSpriteSheet.Height / _rows;

			int frameIndex = maxLifes - currentLives;

			if (frameIndex < 0)
			{
				frameIndex = 0;
			}

			int row = frameIndex / _cols;
			int col = frameIndex % _cols;

			Rectangle sourceRect = new(col * width, row * height, width, height);
			Vector2 position = new(location.X - width / 2, location.Y + 10);

			spriteBatch.Draw(_lifeSpriteSheet, position, sourceRect, Color.White);
		}
		#endregion
	}
}