using Ecliptica.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Games
{
	public class Firework
	{
		private Vector2 position;
		private Vector2 velocity;
		private bool isExploding;

		private AnimatedSprite trailAnimation;
		private AnimatedSprite explosionAnimation;

		private float trailDuration;
		private float elapsedTrailTime;

		public bool IsFinished { get; private set; }

		public Firework(Texture2D trailSheet, int trailsRows, int trailCols, Texture2D explosionSheet, int explosionRows, int explosionCols,  Vector2 startPosition, Vector2 startVelocity, float trailDuration)
		{
			this.position = startPosition;
			this.velocity = startVelocity;
			this.trailDuration = trailDuration;
			this.elapsedTrailTime = 0f;
			this.isExploding = false;
			this.IsFinished = false;

			// Initialize animations
			trailAnimation = new AnimatedSprite(trailSheet, trailsRows, trailCols, 0.1f);
			explosionAnimation = new AnimatedSprite(explosionSheet, explosionRows, explosionCols, 0.05f);
		}

		public static Firework Clone(Firework firework)
		{
			return new Firework(firework.trailAnimation.Texture, firework.trailAnimation.Rows, firework.trailAnimation.Columns,
								firework.explosionAnimation.Texture, firework.explosionAnimation.Rows, firework.explosionAnimation.Columns,
								firework.position, firework.velocity, firework.trailDuration
			);
		}

		public void Update(GameTime gameTime)
		{
			if (IsFinished) return;

			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (!isExploding)
			{
				// Update trail phase
				position += velocity * deltaTime;
				elapsedTrailTime += deltaTime;
				trailAnimation.Update(gameTime);

				if (elapsedTrailTime >= trailDuration)
				{
					isExploding = true;
					elapsedTrailTime = 0f;
				}
			} else
			{
				// Update explosion phase
				explosionAnimation.Update(gameTime);
				if (!explosionAnimation.isActive)
				{
					IsFinished = true;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (IsFinished) return;

			if (!isExploding)
			{
				// Draw trail
				trailAnimation.Draw(spriteBatch, position);
			} else
			{
				// Draw explosion
				explosionAnimation.Draw(spriteBatch, position);
			}
		}
	}
}
