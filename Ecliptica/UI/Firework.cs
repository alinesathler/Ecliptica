using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.UI
{
    public class Firework
    {
		#region Fields
		private Vector2 _position;
        private Vector2 _velocity;
        private bool _isExploding;

        private readonly AnimatedSprite _trailAnimation;
        private readonly AnimatedSprite _explosionAnimation;

        private readonly float _trailDuration;
        private float _elapsedTrailTime;
		#endregion

		#region Properties
		public bool IsFinished { get; private set; }
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor to initialize the firework
		/// </summary>
		/// <param name="trailSheet"></param>
		/// <param name="trailsRows"></param>
		/// <param name="trailCols"></param>
		/// <param name="explosionSheet"></param>
		/// <param name="explosionRows"></param>
		/// <param name="explosionCols"></param>
		/// <param name="startPosition"></param>
		/// <param name="startVelocity"></param>
		/// <param name="trailDuration"></param>
		public Firework(Texture2D trailSheet, int trailsRows, int trailCols, Texture2D explosionSheet, int explosionRows, int explosionCols, Vector2 startPosition, Vector2 startVelocity, float trailDuration)
        {
			_position = startPosition;
			_velocity = startVelocity;
			_trailDuration = trailDuration;
			_elapsedTrailTime = 0f;
			_isExploding = false;
            IsFinished = false;

			// Initialize animations
			_trailAnimation = new AnimatedSprite(trailSheet, trailsRows, trailCols, 0.1f);
			_explosionAnimation = new AnimatedSprite(explosionSheet, explosionRows, explosionCols, 0.05f);
        }
		#endregion

		#region Methods
		/// <summary>
		/// Method to clone a firework
		/// </summary>
		/// <param name="firework"></param>
		/// <returns></returns>
		public static Firework Clone(Firework firework)
        {
            return new Firework(firework._trailAnimation.Texture, firework._trailAnimation.Rows, firework._trailAnimation.Columns,
                                firework._explosionAnimation.Texture, firework._explosionAnimation.Rows, firework._explosionAnimation.Columns,
                                firework._position, firework._velocity, firework._trailDuration
			);
        }

		/// <summary>
		/// Method to update the firework
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
        {
            if (IsFinished) return;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!_isExploding)
            {
				// Update trail phase
				_position += _velocity * deltaTime;
				_elapsedTrailTime += deltaTime;
				_trailAnimation.Update(gameTime);

                if (_elapsedTrailTime >= _trailDuration)
                {
					_isExploding = true;
					_elapsedTrailTime = 0f;
                }
            }
            else
            {
				// Update explosion phase
				_explosionAnimation.Update(gameTime);
                if (!_explosionAnimation.IsActive)
                {
                    IsFinished = true;
                }
            }
        }

		/// <summary>
		/// Method to draw the firework
		/// </summary>
		/// <param name="spriteBatch"></param>
		public void Draw(SpriteBatch spriteBatch)
        {
            if (IsFinished) return;

            if (!_isExploding)
            {
				// Draw trail
				_trailAnimation.Draw(spriteBatch, _position);
            }
            else
            {
				// Draw explosion
				_explosionAnimation.Draw(spriteBatch, _position);
            }
        }
		#endregion
	}
}
