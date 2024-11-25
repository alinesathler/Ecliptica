using Ecliptica.Games;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Ecliptica.Arts;
using Ecliptica.UI;
using System;

namespace Ecliptica.Levels
{
    public class Level
	{
		public int LevelNumber { get; set; }
		public int EnemyCount { get; set; } = 0;
		public Texture2D BackgroundSolid { get; set; }
		public Texture2D BackgroundStars { get; set; }
		public Song MusicTrack { get; set; }
		public List<Entity> Enemies { get; set; }
		private float _soundVolume = 0.25f;
		private string _levelName;
		private double _levelInitialTime;
		private double _levelRemaningTime;

		private int _emenyIndex;
		private int _step;
		private int _stepCounter;
		
		private bool _isBonusLifeCreated = false;
		private bool _isBonusTimeCreated = false;


		public Level(int levelNumber, double levelTime, int timeStep)
		{
			LevelNumber = levelNumber;
			_levelInitialTime = levelTime;
			_levelRemaningTime = _levelInitialTime;
			_levelName = "Level " + LevelNumber;
			_step = timeStep;
			_stepCounter = 1;

			Enemies = new ();
			BackgroundSolid = Images.BackgroundBlue;
			BackgroundStars = Images.BackgroundStars;
		}

		/// <summary>
		/// Method to load the level
		/// </summary>
		public void LoadLevel()
		{
			Sounds.PlayMusic(MusicTrack);

			Background.Load(BackgroundSolid, BackgroundStars);

			EntityManager.Clear();

			// Add the first 5 enemies to the level
			for (int i = 0; i < 5; i++)
			{
				EntityManager.Add(Enemies[i]);

				_emenyIndex = i;
			}

			//foreach (var enemy in Enemies)
			//{
			//	EntityManager.Add(enemy);
			//}
		}

		/// <summary>
		/// Method to add an asteroid to the level
		/// </summary>
		/// <param name="asteroid"></param>
		public void AddAsteroid(Entity asteroid)
		{
			Enemies.Add(asteroid);

			EnemyCount++;
		}

		public bool IsLevelComplete()
		{
			return _levelRemaningTime <= 0;
		}

		/// <summary>
		/// Method to update the level
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
		{
			EntityManager.Update(gameTime, _soundVolume);

			_levelRemaningTime -= gameTime.ElapsedGameTime.TotalSeconds;
		}

		/// <summary>
		/// Method to draw the level
		/// </summary>
		/// <param name="spriteBatch"></param>
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(Fonts.FontGame, _levelName, new Vector2((EclipticaGame.ScreenSize.X - Fonts.FontGame.MeasureString(_levelName).X) / 2, 10), Color.White);

			TimeSpan remainingTime = TimeSpan.FromSeconds(_levelRemaningTime);

			string levelTimeText;

			if (remainingTime.TotalSeconds > 0)
			{
				levelTimeText = $"{remainingTime.Minutes:00}:{remainingTime.Seconds:00}";
			}
            else
            {
				levelTimeText = "00:00";
			}

			if(_levelRemaningTime <= _levelInitialTime / 2 && !_isBonusLifeCreated)
			{
				EntityManager.Add(new BonusLife());
				_isBonusLifeCreated = true;
			}

			if (_levelRemaningTime <= _levelInitialTime / 3 && !_isBonusTimeCreated)
			{
				EntityManager.Add(new BonusTime());
				_isBonusTimeCreated = true;
			}

				spriteBatch.DrawString(Fonts.FontGame, levelTimeText, new Vector2((EclipticaGame.ScreenSize.X - Fonts.FontGame.MeasureString(levelTimeText).X) - 10, 10), Color.White);

			int aux = _emenyIndex;

			// Add the next 5 enemies to the level each _step seconds
			if (_levelRemaningTime < _levelInitialTime - _step * _stepCounter)
			{
				for (int i = _emenyIndex; i < aux + 5; i++)
				{
					if (i < Enemies.Count)
					{
						EntityManager.Add(Enemies[i]);
					}

					_emenyIndex = i;
				}

				 _stepCounter++;
			}

			EntityManager.Draw(spriteBatch);
		}

		/// <summary>
		/// Method to fire a projectile
		/// </summary>
		public void FireProjectile()
		{
			Vector2 projectileStartPosition = new (
				ShipPlayer.Instance.Position.X,
				ShipPlayer.Instance.Position.Y - ShipPlayer.Instance.Size.Y
			);

			Vector2 projectileVelocity = new (0, -10);

			var newProjectile = new Projectile(Images.LaserYellow, projectileStartPosition, projectileVelocity);

			EntityManager.Add(newProjectile);

			Sounds.PlaySound(Sounds.Shoot, _soundVolume);
		}

		public void Clear()
		{
			Enemies.Clear();
		}

		public void AddTime()
		{
			_levelRemaningTime += 10;
		}
	}

}
