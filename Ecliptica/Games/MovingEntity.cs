using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecliptica.Games
{
	public class MovingEntity : Entity
	{
		private Random _random;

		public MovingEntity(Texture2D texture, Vector2 startPosition, Vector2 velocity, int life)
		{
			_random = new Random();

			this.image = texture;

			_position = new Vector2(_random.Next(0, (int)startPosition.X - (int)image.Width), 0);


			_velocity = velocity;
			//_position = new Vector2(_random.NextFloat(0.00f, 0.9f) * EclipticaGame.ScreenSize.X, _random.NextFloat(0.00f, 0.8f) * EclipticaGame.ScreenSize.Y);
			this.life = life;

			CalculateBoundingBox();
		}

		public override void Update(GameTime gametime)
		{
			_position += _velocity;
			CalculateBoundingBox();

			Orientation += 0.05f;

			// Expire the entity when it goes off the screen (bottom)
			if (_position.Y > EclipticaGame.ScreenSize.Y)
			{
					HasExpired();
			}

			//Bounce off the sides of the screen
			if (_position.X < image.Width || _position.X > EclipticaGame.ScreenSize.X - image.Width)
			{
				_velocity.X *= -1;
			}
		}

		/// <summary>
		/// Method to expire the entity
		/// </summary>
		public void HasExpired()
		{
			IsExpired = true;
			IsActive = false;
		}

		//public override void Draw(SpriteBatch spriteBatch)
		//{
		//	spriteBatch.Draw(image, _position, color);
		//}
	}
}
