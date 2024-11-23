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
using Ecliptica.Screens;

namespace Ecliptica.Levels
{
    public static class LevelManager
	{
		public static Level CurrentLevel { get; private set; }
		private static List<Level> levels;
		private static int currentLevelIndex;

		private static Vector2 _velocityLevel1 = new (0, 1);
		private static Vector2 _velocityLevel2 = new (0, 1.5f);
		private static Vector2 _velocityLevel3 = new (0, 2.0f);
		private static Vector2 _velocityLevel4 = new (0, 2.5f);
		private static Vector2 _velocityLevel5 = new (0, 3.0f);

		/// <summary>
		/// Constructor to initialize the levels
		/// </summary>
		static LevelManager()
		{
			levels = new ();
			currentLevelIndex = 0;
		}

		/// <summary>
		/// Method to check if the current level is the last level
		/// </summary>
		/// <returns></returns>
		public static bool IsLastLevel()
		{
			return currentLevelIndex == levels.Count - 1;
		}

		/// <summary>
		/// Method to load the all levels
		/// </summary>
		public static void LoadLevels()
		{
			// Define and add levels here
			var level1 = new Level(1);

			for (int i = 0; i < 1; i++)
			{
				level1.AddAsteroid(new Asteroid(_velocityLevel1));
			}

			level1.MusicTrack = Sounds.MusicTheme;
			levels.Add(level1);

			var level2 = new Level(2);

			for (int i = 0; i < 2; i++)
			{
				level2.AddAsteroid(new Asteroid(_velocityLevel2));
			}
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
				CurrentLevel = null;
			}
		}

		public static void FireProjectile()
		{
			CurrentLevel?.FireProjectile();
		}

		public static void Clear()
		{
			CurrentLevel = null;
			levels.Clear();
			currentLevelIndex = 0;
		}
	}

}
