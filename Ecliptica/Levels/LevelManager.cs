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

		private static double initialLevelTime = 10;
		private static double levelTimeIncrement = 5;

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

			for(int i = levelNumber; i < 5; i++)
			{
				double levelTime = initialLevelTime + (i * levelTimeIncrement);

				Level level = new(i, levelTime);

				for (int j = 0; j < 5; j++)
				{
					level.AddAsteroid(new Asteroid(new Vector2(random.NextFloat(-1.00f * (i + 1), 1.00f * (i + 1)), random.NextFloat(0.10f * (i + 1), 1.00f * (i + 1)))));
				}

				level.MusicTrack = Sounds.MusicTheme;
				levels.Add(level);
			}
			//// Level1
			//var level1 = new Level(1);
			//for (int i = 0; i < 2; i++)
			//{
			//	level1.AddAsteroid(new Asteroid(new Vector2(random.NextFloat(-1.00f, 1.00f), random.NextFloat(0.10f, 1.00f))));
			//}

			//level1.MusicTrack = Sounds.MusicTheme;
			//levels.Add(level1);

			//// Level2
			//var level2 = new Level(2);
			//for (int i = 0; i < 2; i++)
			//{
			//	level2.AddAsteroid(new Asteroid(new Vector2(random.NextFloat(-1.50f, 1.50f), random.NextFloat(0.10f, 1.50f))));
			//}
			//level2.MusicTrack = Sounds.MusicTheme;
			//levels.Add(level2);

			//// Level3
			//var level3 = new Level(3);
			//for (int i = 0; i < 2; i++)
			//{
			//	level3.AddAsteroid(new Asteroid(new Vector2(random.NextFloat(-2.00f, 2.00f), random.NextFloat(0.10f, 2.00f))));
			//}
			//level3.MusicTrack = Sounds.MusicTheme;
			//levels.Add(level3);

			//// Level4
			//var level4 = new Level(4);
			//for (int i = 0; i < 2; i++)
			//{
			//	level4.AddAsteroid(new Asteroid(new Vector2(random.NextFloat(-2.50f, 2.50f), random.NextFloat(0.10f, 2.50f))));
			//}
			//level4.MusicTrack = Sounds.MusicTheme;
			//levels.Add(level4);

			//// Level5
			//var level5 = new Level(5);
			//for (int i = 0; i < 2; i++)
			//{
			//	level5.AddAsteroid(new Asteroid(new Vector2(random.NextFloat(-3.00f, 3.00f), random.NextFloat(0.10f, 3.00f))));
			//}
			//level5.MusicTrack = Sounds.MusicTheme;
			//levels.Add(level5);


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
