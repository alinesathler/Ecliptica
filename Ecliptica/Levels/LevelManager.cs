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
		private static double elapsedLevelTime;

		//private static double initialLevelTime = 50;
		//private static double levelTimeIncrement = 10;

		private static double initialLevelTime = 50;
		private static double levelTimeIncrement = 10;

		/// <summary>
		/// Constructor to initialize the levels
		/// </summary>
		static LevelManager()
		{
			levels = new();
			currentLevelIndex = 0;
			elapsedLevelTime = 0;
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
		public static void LoadLevels(int levelNumber)
		{
			Random random = new();

			for(int i = levelNumber; i < 6; i++)
			{
				double levelTime = initialLevelTime + (i * levelTimeIncrement);
				int levelStep = 10 - i;

				Level level = new(i, levelTime, levelStep);

				for (int j = 0; j < 100; j++)
				{
					level.AddAsteroid(new Asteroid(i, new Vector2(random.NextFloat(-1.00f * (i + 1), 1.00f * (i + 1)), random.NextFloat(0.10f * (i + 1), 1.00f * (i + 1)))));
				}

				level.MusicTrack = Sounds.MusicTheme;
				levels.Add(level);
			}

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

			ScoresScreen.UpdateHighScores();

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
