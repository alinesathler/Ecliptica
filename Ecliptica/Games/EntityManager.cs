using Ecliptica.Arts;
using Ecliptica.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Ecliptica.Games
{
	public class EntityManager
	{
		private static int _enemiesDestroyedTotal = 0;

		private static int _enemiesDestroyedLevel = 0;

		public static List<Entity> entities = new();

		public static int Count { get { return entities.Count; } }

		public static AnimatedSprite Explosion;
		private static Vector2 _animatedPosition;

		/// <summary>
		/// Method to add an entity to the list of entities
		/// </summary>
		/// <param name="entity"></param>
		public static void Add(Entity entity)
		{
			entities.Add(entity);
		}

		/// <summary>
		/// Method to update the entities
		/// </summary>
		/// <param name="gametime"></param>
		public static void Update(GameTime gametime, float volume)
		{
			HandleCollisions(volume);

			foreach (var entity in entities)
			{
				entity.Update(gametime);
			}

			if (Explosion != null && Explosion.isActive)
			{
				Explosion.Update(gametime);
			}


			entities.RemoveAll(x => x.IsExpired);
		}

		/// <summary>
		/// Method to clear the list of entities
		/// </summary>
		public static void Clear()
		{
			entities.Clear();

			if(ShipPlayer.Instance != null) entities.Add(ShipPlayer.Instance);

			_enemiesDestroyedLevel = 0;
		}

		/// <summary>
		/// Method to handle collisions between entities
		/// </summary>
		private static void HandleCollisions(float volume)
		{
			if (entities.Count == 0) return;

			for (int i = 0; i < entities.Count; i++)
			{
				// Check if the entity is active
				if (!entities[i].IsActive) continue;

				// Check for collisions between projectiles and other entities
				for (int j = 0; j < entities.Count; j++)
				{
					// Avoid self-collision (no need to check for a collision with itself)
					if (i == j) continue;

					// Aboid collision between projectiles and the ShipPlayer
					if (entities[i] is Projectile && entities[j] is ShipPlayer) continue;
					if (entities[j] is Projectile && entities[i] is ShipPlayer) continue;

					// Check if there's a collision between projectiles/shipPlayer and other entities
					if ((entities[i] is Projectile || entities[j] is Projectile) && IsColliding(entities[i], entities[j]) || (entities[i] is ShipPlayer || entities[j] is ShipPlayer) && IsColliding(entities[i], entities[j]))
					{
						// Decrease the life of the entities
						entities[i].Life--;
						entities[j].Life--;

						// If the entities have no more life, explode them
						if (entities[i].Life == 0)
						{
							ExplodeEntities(entities[i], volume);
						}

						if (entities[j].Life == 0)
						{
							ExplodeEntities(entities[j], volume);
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
		/// <returns></returns>
		private static bool IsColliding(Entity a, Entity b)
		{
			return !a.IsExpired && !b.IsExpired && a.BoundingBox.Intersects(b.BoundingBox);
		}

		/// <summary>
		/// Method to explode entities
		/// </summary>
		/// <param name="entity1"></param>
		/// <param name="entity2"></param>
		private static void ExplodeEntities(Entity entity, float volume)
		{
			// Explode entity
			entity.IsExpired = true;

			if (entity is Asteroid)
			{
				_enemiesDestroyedTotal++;
				_enemiesDestroyedLevel++;
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

			ScreenManager.ReplaceScreen(new GameOverScreen());
		}

		//public static IEnumerable<Entity> GetNearbyEntities(Vector2 position, float radius)
		//      {
		//          return entities.Where(x => Vector2.DistanceSquared(position, x.Position) < radius * radius);
		//      }

		/// <summary>
		/// Method to draw the entities
		/// </summary>
		/// <param name="spriteBatch"></param>
		public static void Draw(SpriteBatch spriteBatch)
		{
			foreach (var entity in entities)
			{
				if (entity.IsActive)
				{
					entity.Draw(spriteBatch);
				}
			}

			if (Explosion != null && Explosion.isActive)
			{
				Explosion.Draw(spriteBatch, _animatedPosition);
			}
			
			if (ShipPlayer.Instance.Life == 0 && Explosion != null && !Explosion.isActive)
			{
				KillPlayer();

				Explosion = null;
			}
		}

		/// <summary>
		/// Method to get the number of enemies destroyed total
		/// </summary>
		/// <returns></returns>
		public static int GetNumberOfEnemiesDestroyedTotal()
		{
			return _enemiesDestroyedTotal;
		}

		public static void ResetEnemiesDestroyedTotal()
		{
			_enemiesDestroyedTotal = 0;
		}

		/// <summary>
		/// Method to get the number of enemies destroyed in the level
		/// </summary>
		/// <returns></returns>
		public static int GetNumberOfEnemiesDestroyedLevel()
		{
			return _enemiesDestroyedLevel;
		}


	}
}
