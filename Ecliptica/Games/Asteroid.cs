using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Ecliptica.Arts;

namespace Ecliptica.Games
{
	public class Asteroid : Entity
	{
		#region Fields
		private readonly Random _random;
		private readonly LifeAsteroids _asteroidLife;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor to initialize the asteroid
		/// </summary>
		/// <param name="levelNumber"></param>
		/// <param name="velocity"></param>
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

			Position = new Vector2(_random.Next(0, (int)EclipticaGame.ScreenSize.X - (int)image.Width), 0);

			Velocity = velocity;

			// Creta a new instance of the asteroid life
			_asteroidLife = new LifeAsteroids(Images.Life, 1, 4);

			CalculateBoundingBox();
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to update the asteroid
		/// </summary>
		/// <param name="gametime"></param>
		public override void Update(GameTime gametime)
		{
			Position += Velocity;
			CalculateBoundingBox();

			Orientation += 0.05f;

			if (Position.Y > EclipticaGame.ScreenSize.Y * 9/10)
			{
				HasExpired();
			}

			//Bounce off the sides of the screen
			if (Position.X < image.Width || Position.X > EclipticaGame.ScreenSize.X - image.Width)
			{
				float aux = Velocity.X;
				aux = -aux;
				Velocity = new Vector2(aux, Velocity.Y);
			}
		}

		/// <summary>
		/// Method to draw the asteroid
		/// </summary>
		/// <param name="spriteBatch"></param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			_asteroidLife.Draw(spriteBatch, new Vector2(Position.X, Position.Y - image.Height), 4, Life);
		}

		/// <summary>
		/// Method to expire the asteroid
		/// </summary>
		public void HasExpired()
		{
			IsExpired = true;
			IsActive = false;
		}
		#endregion
	}
}
