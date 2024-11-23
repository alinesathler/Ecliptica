using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Games
{
	public class AnimatedSprite
	{
		public Texture2D Texture { get; set; }
		public int Rows { get; set; }
		public int Columns { get; set; }
		private int _currentFrame;
		private int _totalFrames;
		public float TimePerFrame;
		private float _timer;
		public bool isActive;

		public AnimatedSprite(Texture2D texture, int rows, int columns, float timePerFrame)
		{
			Texture = texture;
			Rows = rows;
			Columns = columns;
			_currentFrame = 0;
			_totalFrames = Rows * Columns;
			TimePerFrame = timePerFrame;
			_timer = 0f;
			isActive = true;
		}

		public void Update(GameTime gameTime)
		{
			if (!isActive) return;

			_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;


			if (_timer >= TimePerFrame)
			{
				_timer -= TimePerFrame;
				_currentFrame++;

				// Deactivate the explosion when animation is complete
				if (_currentFrame >= _totalFrames)
				{
					isActive = false;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location)
		{
			int width = Texture.Width / Columns;
			int height = Texture.Height / Rows;

			int row = _currentFrame / (Texture.Width / width);
			int column = _currentFrame % (Texture.Width / width);
			Rectangle sourceRect = new Rectangle(column * width, row * width, width, height);

			spriteBatch.Draw(Texture, location, sourceRect, Color.White);
		}
	}
}
