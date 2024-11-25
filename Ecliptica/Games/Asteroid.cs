using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Ecliptica.Arts;

namespace Ecliptica.Games
{
	public class Asteroid : Entity
	{
		private Random _random;
		private LifeAsteroids _asteroidLife;

		public Asteroid(int levelNumber, Vector2 velocity)
		{
			_random = new Random();

			int maxLevelOfAsteroid;

			// Set the maximum level of the asteroid based on the level number
			switch (levelNumber) { 
				case 1:
					maxLevelOfAsteroid = 2;
					break;
				case 2:
					maxLevelOfAsteroid = 3;
					break;
				case 3:
					maxLevelOfAsteroid = 4;
					break;
				case 4:
					maxLevelOfAsteroid = 5;
					break;
				case 5:
					maxLevelOfAsteroid = 6;
					break;
				default:
					maxLevelOfAsteroid = 1;
					break;
			}

			// Set the asteroid based on the maximum level of the asteroid
			switch
				(_random.Next(0, maxLevelOfAsteroid))
			{
				case 0:
					image = Images.AsteroidRedSmall;
					MaxLife = 1;
					Life = MaxLife;
					break;
				case 1:
					image = Images.AsteroidRedMedium;
					MaxLife = 2;
					Life = MaxLife;
					break;
				case 2:
					image = Images.AsteroidRedBig;
					MaxLife = 3;
					Life = MaxLife;
					break;
				case 3:
					image = Images.AsteroidBlueSmall;
					MaxLife = 2;
					Life = MaxLife;
					break;
				case 4:
					image = Images.AsteroidBlueMedium;
					MaxLife = 3;
					Life = MaxLife;
					break;
				case 5:
					image = Images.AsteroidBlueBig;
					MaxLife = 4;
					Life = MaxLife;
					break;
			}

			_position = new Vector2(_random.Next(0, (int)EclipticaGame.ScreenSize.X - (int)image.Width), 0);

			_velocity = velocity;

			_asteroidLife = new LifeAsteroids(Images.Life, 1, 4);

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

			if (_position.Y > EclipticaGame.ScreenSize.Y * 9/10)
			{
				HasExpired();
			}

			//Bounce off the sides of the screen
			if (_position.X < image.Width || _position.X > EclipticaGame.ScreenSize.X - image.Width)
			{
				_velocity.X *= -1;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			_asteroidLife.Draw(spriteBatch, new Vector2(_position.X, _position.Y - image.Height), 4, life);
		}

		/// <summary>
		/// Method to expire the asteroid
		/// </summary>
		public void HasExpired()
		{
			IsExpired = true;
			IsActive = false;
		}
	}
}
