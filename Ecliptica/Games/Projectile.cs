using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Games
{
	public class Projectile : Entity
	{
		#region Constructors
		/// <summary>
		/// Constructor for a projectile
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="startPosition"></param>
		/// <param name="velocity"></param>
		public Projectile(Texture2D texture, Vector2 startPosition, Vector2 velocity)
		{
			this.image = texture;
			Position = startPosition;
			Velocity = velocity;
			Life = 1;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to update the projectile
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			Position += Velocity;

			// Expire if it goes off-screen
			if (Position.Y < -image.Height)
				IsExpired = true;
		}
		#endregion
	}
}
