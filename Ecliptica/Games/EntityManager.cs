using Ecliptica.Arts;
using Ecliptica.Levels;
using Ecliptica.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Ecliptica.Screens;

namespace Ecliptica.Games
{
	public class EntityManager
	{
		#region Fields
		private static int _levelScore = 0;
		private static int _totalScore = 0;

		private static AnimatedSprite _explosion;
		private static Vector2 _animatedPosition;

		private readonly static List<Entity> _entities = new();
		#endregion

		#region Properties
		public static int Count { get { return Entities.Count; } }

		public static AnimatedSprite Explosion { get { return _explosion; } set { _explosion = value; } }

		public static List<Entity> Entities { get { return _entities; } }
		#endregion

		#region Methods
		/// <summary>
		/// Method to add an entity to the list of entities
		/// </summary>
		/// <param name="entity"></param>
		public static void Add(Entity entity)
		{
			Entities.Add(entity);
		}

		/// <summary>
		/// Method to update the entities
		/// </summary>
		/// <param name="gametime"></param>
		public static void Update(GameTime gametime, float volume)
		{
			HandleCollisions(volume);

			foreach (var entity in Entities)
			{
				entity.Update(gametime);
			}

			if (Explosion != null && Explosion.IsActive)
			{
				Explosion.Update(gametime);
			}

			// Remove expired entities
			Entities.RemoveAll(x => x.IsExpired);
		}

		/// <summary>
		/// Method to clear the entity manager
		/// </summary>
		public static void Clear()
		{
			Entities.Clear();

			if (ShipPlayer.Instance != null) Entities.Add(ShipPlayer.Instance);

			ResetLevelScore();
		}

		/// <summary>
		/// Method to handle collisions between entities
		/// </summary>
		private static void HandleCollisions(float volume)
		{
			if (Entities.Count == 0) return;

			for (int i = 0; i < Entities.Count; i++)
			{
				// Check if the entity is active
				if (!Entities[i].IsActive) continue;

				for (int j = 0; j < Entities.Count; j++)
				{
					// Avoid self-collision (no need to check for a collision with itself)
					if (i == j) continue;

					// Avoid collision between projectiles and the ShipPlayer
					if (Entities[i] is Projectile && Entities[j] is ShipPlayer) continue;
					if (Entities[j] is Projectile && Entities[i] is ShipPlayer) continue;

					// Avoid collision between projectiles and bonus life
					if (Entities[i] is BonusLife && Entities[j] is Projectile) continue;
					if (Entities[j] is BonusLife && Entities[i] is Projectile) continue;

					// Avoid collision between projectiles and bonus time
					if (Entities[i] is BonusTime && Entities[j] is Projectile) continue;
					if (Entities[j] is BonusTime && Entities[i] is Projectile) continue;

					// Check if there's a collision between the ShipPlayer and a bonus life
					if ((Entities[i] is ShipPlayer && Entities[j] is BonusLife) || (Entities[j] is ShipPlayer && Entities[i] is BonusLife))
					{
						if (IsColliding(Entities[i], Entities[j]))
						{
							Entities[i].PlaySoundPicked();
							Entities[j].PlaySoundPicked();
							ShipPlayer.Instance.AddLife();
							BonusLife.Instance.IsExpired = true;
						}
					}

					// Check if there's a collision between the ShipPlayer and a bonus time
					if ((Entities[i] is ShipPlayer && Entities[j] is BonusTime) || (Entities[j] is ShipPlayer && Entities[i] is BonusTime))
					{
						if (IsColliding(Entities[i], Entities[j]))
						{
							Entities[i].PlaySoundPicked();
							Entities[j].PlaySoundPicked();
							LevelManager.CurrentLevel?.AddTime();
							BonusTime.Instance.IsExpired = true;
						}
					}

					// Check if there's a collision between projectiles/shipPlayer and other entities
					if ((Entities[i] is Projectile || Entities[j] is Projectile) && IsColliding(Entities[i], Entities[j]) || (Entities[i] is ShipPlayer || Entities[j] is ShipPlayer) && IsColliding(Entities[i], Entities[j]))
					{
						// Decrease the life of the entities
						Entities[i].Life--;
						Entities[j].Life--;

						// If the entities have no more life, explode them
						if (Entities[i].Life == 0)
						{
							ExplodeEntities(Entities[i], volume);
						}

						if (Entities[j].Life == 0)
						{
							ExplodeEntities(Entities[j], volume);
						}

						break;
					}
				}
			}
		}

		/// <summary>
		/// Method to check if two entities are colliding
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns>True is entities ae colliding, false if they ar not.</returns>
		private static bool IsColliding(Entity a, Entity b)
		{
			return !a.IsExpired && !b.IsExpired && a.BoundingBox.Intersects(b.BoundingBox);
		}

		/// <summary>
		/// Method to explode entities
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="volume"></param>
		private static void ExplodeEntities(Entity entity, float volume)
		{
			// Expire entity
			entity.IsExpired = true;

			// Increase the score based and asteroid speed and life
			if (entity is Asteroid)
			{
				_levelScore += entity.MaxLife * (int)(100 * (Math.Abs(entity.Velocity.X) + entity.Velocity.Y));
				_totalScore += entity.MaxLife * (int)(100 * (Math.Abs(entity.Velocity.X) + entity.Velocity.Y));
			}

			if (entity is ShipPlayer)
			{
				Sounds.PlaySound(Sounds.PlayerKilled, volume * 4);
			} else
			{
				Sounds.PlaySound(Sounds.Explosion, volume * 4);
			}

			Explosion = new AnimatedSprite(Images.Explosion, 5, 5, 0.05f);
			_animatedPosition = entity.Position - entity.Size;
		}

		/// <summary>
		/// Method to kill the player
		/// </summary>
		private static void KillPlayer()
		{
			Explosion = new AnimatedSprite(Images.Explosion, 5, 5, 0.05f);
			_animatedPosition = new Vector2(ShipPlayer.Instance.Position.X, ShipPlayer.Instance.Position.Y);
			_animatedPosition = ShipPlayer.Instance.Position - ShipPlayer.Instance.Size;

			ShipPlayer.Instance.IsActive = false;
			ShipPlayer.Instance.IsExpired = true;

			// Update high scores
			ScoresScreen.UpdateHighScores();

			ScreenManager.ReplaceScreen(new GameOverScreen());
		}

		/// <summary>
		/// Method to draw the entities
		/// </summary>
		/// <param name="spriteBatch"></param>
		public static void Draw(SpriteBatch spriteBatch)
		{
			foreach (var entity in Entities)
			{
				if (entity.IsActive)
				{
					entity.Draw(spriteBatch);
				}
			}

			if (Explosion != null && Explosion.IsActive)
			{
				Explosion.Draw(spriteBatch, _animatedPosition);
			}

			if (ShipPlayer.Instance.Life == 0 && Explosion != null && !Explosion.IsActive)
			{
				KillPlayer();

				Explosion = null;
			}
		}

		/// <summary>
		/// Method to get the level score
		/// </summary>
		/// <returns>An int with the gae score</returns>
		public static int GetLevelScore()
		{
			return _levelScore;
		}

		/// <summary>
		/// Method to get the total score
		/// </summary>
		/// <returns>An int wwith game score</returns>
		public static int GetTotalScore()
		{
			return _totalScore;
		}

		/// <summary>
		/// Method to reset the level score
		/// </summary>
		public static void ResetLevelScore()
		{
			_levelScore = 0;
		}

		/// <summary>
		/// Method to reset the total score
		/// </summary>
		public static void ResetTotalScore()
		{
			_totalScore = 0;
		}

		/// <summary>
		/// Method to set the total score
		/// </summary>
		/// <param name="score"></param>
		public static void SetTotalScore(int score)
		{
			_totalScore = score;
		}
		#endregion
	}
}
