using Ecliptica.Games;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

namespace Ecliptica.Levels
{
	public class Level
	{
		public int LevelNumber { get; set; }
		public int EnemyCount { get; set; } = 0;
		public string BackgroundImage { get; set; }
		public Song MusicTrack { get; set; }
		public List<Entity> Enemies { get; set; }
		private float _soundVolume = 0.25f;


		public Level(int levelNumber)
		{
			LevelNumber = levelNumber;
			Enemies = new List<Entity>();
		}

		/// <summary>
		/// Method to load the level
		/// </summary>
		public void LoadLevel()
		{
			Sound.PlayMusic(MusicTrack);

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

		/// <summary>
		/// Method to update the level
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
		{
			EntityManager.Update(gameTime, _soundVolume);
		}

		/// <summary>
		/// Method to draw the level
		/// </summary>
		/// <param name="spriteBatch"></param>
		public void Draw(SpriteBatch spriteBatch)
		{
			EntityManager.Draw(spriteBatch);
		}

		/// <summary>
		/// Method to fire a projectile
		/// </summary>
		public void FireProjectile(ShipPlayer shipPlayer)
		{
			Vector2 projectileStartPosition = new Vector2(
				shipPlayer.Position.X,
				shipPlayer.Position.Y - shipPlayer.GetImageSize()[1]
			);

			Vector2 projectileVelocity = new Vector2(0, -10);

			var newProjectile = new Projectile(Art.LaserYellow, projectileStartPosition, projectileVelocity);

			EntityManager.Add(newProjectile);

			Sound.PlaySound(Sound.Shoot, _soundVolume);
		}
	}

}
