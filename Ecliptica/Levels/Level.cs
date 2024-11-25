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
		private double _levelRemaningTime;


		public Level(int levelNumber, double levelTime)
		{
			LevelNumber = levelNumber;
			_levelRemaningTime = levelTime;
			_levelName = "Level " + LevelNumber;

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

			foreach (var enemy in Enemies)
			{
				EntityManager.Add(enemy);
			}
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

            spriteBatch.DrawString(Fonts.FontGame, levelTimeText, new Vector2((EclipticaGame.ScreenSize.X - Fonts.FontGame.MeasureString(levelTimeText).X) - 10, 10), Color.White);
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

		public double GetLevelRemaingTime()
		{
			return _levelRemaningTime;
		}
	}

}
