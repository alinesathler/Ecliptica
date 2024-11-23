using Ecliptica.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecliptica.Games
{
	internal class LifeAsteroids
	{
		Texture2D _lifeSpriteSheet;
		int _rows;
		int _cols;

		public LifeAsteroids(Texture2D lifeSpriteSheet, int rows, int cols)
		{
			_lifeSpriteSheet = lifeSpriteSheet;
			_rows = rows;
			_cols = cols;
		}

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

			Rectangle sourceRect = new Rectangle(col * width, row * height, width, height);
			Vector2 position = new Vector2(location.X - width / 2, location.Y + 10);

			spriteBatch.Draw(_lifeSpriteSheet, position, sourceRect, Color.White);

		}
	}
}