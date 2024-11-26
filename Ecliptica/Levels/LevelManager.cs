using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Ecliptica.Games;
using Ecliptica.Arts;
using Ecliptica.Screens;

namespace Ecliptica.Levels
{
	public static class LevelManager
	{
		#region Fields
		private readonly static List<Level> _levels;
		private static int _currentLevelIndex;

		private readonly static double _initialLevelTime = 50;
		private readonly static double _levelTimeIncrement = 10;
		#endregion

		#region Properties
		public static Level CurrentLevel { get; private set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor to initialize the levels
		/// </summary>
		static LevelManager()
		{
			_levels = new();
			_currentLevelIndex = 0;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to check if the current level is the last level
		/// </summary>
		/// <returns>True if current level is the last level</returns>
		public static bool IsLastLevel()
		{
			return _currentLevelIndex == _levels.Count - 1;
		}

		/// <summary>
		/// Method to load the all levels
		/// </summary>
		public static void LoadLevels(int levelNumber)
		{
			Random random = new();

			// Create the levels
			for (int i = levelNumber; i < 6; i++)
			{
				double levelTime = _initialLevelTime + (i * _levelTimeIncrement);
				int levelStep = 10 - i;

				Level level = new(i, levelTime, levelStep);

				for (int j = 0; j < 100; j++)
				{
					level.AddAsteroid(new Asteroid(i, new Vector2(random.NextFloat(-1.00f * (i + 1), 1.00f * (i + 1)), random.NextFloat(0.10f * (i + 1), 1.00f * (i + 1)))));
				}

				level.MusicTrack = Sounds.MusicTheme;
				_levels.Add(level);
			}

			// Set the first level as the current level
			CurrentLevel = _levels[_currentLevelIndex];
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
			_currentLevelIndex++;

			// If there are more levels, load the next one
			if (_currentLevelIndex < _levels.Count)
			{
				CurrentLevel = _levels[_currentLevelIndex];

				CurrentLevel.LoadLevel();
			} else
			{
				CurrentLevel = null;
			}
		}

		/// <summary>
		///	Method to fire a projectile
		/// </summary>
		public static void FireProjectile()
		{
			CurrentLevel?.FireProjectile();
		}

		/// <summary>
		/// Method to clear the levels
		/// </summary>
		public static void Clear()
		{
			CurrentLevel = null;
			_levels.Clear();
			_currentLevelIndex = 0;
		}
		#endregion
	}
}
