using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecliptica.Games;
using Ecliptica.Arts;
using System.Reflection.Metadata;

namespace Ecliptica.Levels
{
    public static class LevelManager
	{
		public static Level CurrentLevel { get; private set; }
		private static List<Level> levels;
		private static int currentLevelIndex;

		private static int _smallLife = 1;
		private static int _mediumLife = 2;
		private static int _largeLife = 3;

		private static Vector2 _velocityLevel1 = new Vector2(0, 1);

		/// <summary>
		/// Constructor to initialize the levels
		/// </summary>
		static LevelManager()
		{
			Background.Load(Images.BackgroundBlue, Images.BackgroundStars);

			levels = new List<Level>();
			currentLevelIndex = 0;
		}

		/// <summary>
		/// Method to load the all levels
		/// </summary>
		public static void LoadLevels()
		{
			// Define and add levels here
			var level1 = new Level(1);
			level1.AddAsteroid(new Asteroid(Images.AsteroidRedSmall, EclipticaGame.ScreenSize, _velocityLevel1, _smallLife));
			level1.MusicTrack = Sounds.MusicTheme;
			levels.Add(level1);

			var level2 = new Level(2);
			level2.AddAsteroid(new Asteroid(Images.AsteroidRedMedium, EclipticaGame.ScreenSize, _velocityLevel1, _mediumLife));
			level2.AddAsteroid(new Asteroid(Images.AsteroidRedMedium, EclipticaGame.ScreenSize, _velocityLevel1, _mediumLife));
			level2.AddAsteroid(new Asteroid(Images.AsteroidRedBig, EclipticaGame.ScreenSize, _velocityLevel1, _largeLife));
			level2.AddAsteroid(new Asteroid(Images.AsteroidRedBig, EclipticaGame.ScreenSize, _velocityLevel1, _largeLife));
			level2.MusicTrack = Sounds.MusicTheme;
			levels.Add(level2);


			// Set the first level as the current level
			CurrentLevel = levels[currentLevelIndex];
			CurrentLevel.LoadLevel();
		}

		/// <summary>
		/// Method to update the current level
		/// </summary>
		/// <param name="gameTime"></param>
		public static void UpdateCurrentLevel(GameTime gameTime)
		{
			CurrentLevel?.Update(gameTime);
		}

		/// <summary>
		/// Method to draw the current level
		/// </summary>
		/// <param name="spriteBatch"></param>
		public static void DrawCurrentLevel(SpriteBatch spriteBatch)
		{
			CurrentLevel?.Draw(spriteBatch);
		}

		/// <summary>
		/// Method to move to the next level
		/// </summary>
		public static void NextLevel()
		{
			currentLevelIndex++;

			// If there are more levels, load the next one
			if (currentLevelIndex < levels.Count)
			{
				CurrentLevel = levels[currentLevelIndex];

				CurrentLevel.LoadLevel();
			} else
			{
				// End the game if all levels are completed
				CurrentLevel = null;
				// Handle game over here, if necessary
			}
		}

		public static void FireProjectile(ShipPlayer shipPlayer)
		{
			CurrentLevel?.FireProjectile(shipPlayer);
		}

		///// <summary>
		///// Method to reset the levels
		///// </summary>
		//public static void ResetLevels()
		//{
		//	CurrentLevelIndex = 0;
		//	LoadCurrentLevel();
		//}
	}

}
