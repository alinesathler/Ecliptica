using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Ecliptica.Games
{
	public class Asteroid : Entity
	{
		private Random _random;

		public Asteroid(Texture2D texture, Vector2 startPosition, Vector2 velocity, int life)
		{
			_random = new Random();

			this.image = texture;

			_position = new Vector2(_random.Next(0, (int)startPosition.X - (int)image.Width), 0);


			_velocity = velocity;
			this.life = life;

			CalculateBoundingBox();
		}

		/// <summary>
		/// Method to update the asteroid
		/// </summary>
		/// <param name="gametime"></param>
		public override void Update(GameTime gametime)
		{
			_position += _velocity;
			CalculateBoundingBox();

			Orientation += 0.05f;

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
		/// Method to expire the asteroid
		/// </summary>
		public void HasExpired()
		{
			IsExpired = true;
			IsActivee = false;
		}
	}
}
