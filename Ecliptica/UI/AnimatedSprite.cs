using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.UI
{
    public class AnimatedSprite
    {
		#region Fields
		private int _currentFrame;
		private readonly int _totalFrames;
		private readonly float _timePerFrame;
		private float _timer;
		private bool _isActive;
		#endregion

		#region Properties
		public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
		public bool IsActive
		{
			get { return _isActive; }
			set { _isActive = value; }
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor to initialize the animated sprite
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="rows"></param>
		/// <param name="columns"></param>
		/// <param name="timePerFrame"></param>
		public AnimatedSprite(Texture2D texture, int rows, int columns, float timePerFrame)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            _currentFrame = 0;
            _totalFrames = Rows * Columns;
			_timePerFrame = timePerFrame;
            _timer = 0f;
			IsActive = true;
        }
		#endregion

		#region Methods
		/// <summary>
		/// Method to update the animated sprite
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
        {
            if (!IsActive) return;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (_timer >= _timePerFrame)
            {
                _timer -= _timePerFrame;
                _currentFrame++;

                // Deactivate the explosion when animation is complete
                if (_currentFrame >= _totalFrames)
                {
					IsActive = false;
                }
            }
        }

		/// <summary>
		/// Method to draw the animated sprite
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="location"></param>
		public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;

            int row = _currentFrame / (Texture.Width / width);
            int column = _currentFrame % (Texture.Width / width);
            Rectangle sourceRect = new(column * width, row * width, width, height);

            spriteBatch.Draw(Texture, location, sourceRect, Color.White);
        }
		#endregion
	}
}
