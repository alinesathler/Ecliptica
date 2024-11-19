using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Games
{
	public class Projectile : Entity
	{
		/// <summary>
		/// Constructor for a projectile
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="startPosition"></param>
		/// <param name="velocity"></param>
		public Projectile(Texture2D texture, Vector2 startPosition, Vector2 velocity)
		{
			this.image = texture;
			_position = startPosition;
			_velocity = velocity;
			life = 1;
		}

		/// <summary>
		/// Method to update the projectile
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			Position += _velocity;

			// Expire if it goes off-screen
			if (Position.Y < -image.Height)
				IsExpired = true;
		}
	}
}
