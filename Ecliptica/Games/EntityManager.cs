using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ecliptica.Games
{
	public class EntityManager
	{
		private static int _enemiesDestroyedTotal = 0;

		private static int _enemiesDestroyedLevel = 0;

		public static List<Entity> entities = new List<Entity>();

		public static int Count { get { return entities.Count; } }

		private static AnimatedSprite _explosion;
		private static Vector2 _animatedPosition;
		private static bool _isExploding = false;

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

			if (_isExploding)
			{
				_explosion.Update();
			}


			entities.RemoveAll(x => x.IsExpired);
		}

		/// <summary>
		/// Method to clear the list of entities
		/// </summary>
		public static void Clear()
		{
			entities.Clear();
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
				if (!entities[i].IsActivee) continue;

				// Check collisions between ShipPlayer and entities
				if (IsColliding(ShipPlayer.Instance, entities[i]))
				{
					// If it's a projectile, ignore the collision (no action)
					if (entities[i] is Projectile)
					{
						continue;
					}

					// If it's not a projectile, decrease the life of the ShipPlayer
					ShipPlayer.Instance.Life--;

					// Explode the entity if it collides with the ShipPlayer
					ExplodeEntities(entities[i], volume);

					// If the ShipPlayer has no more life, explode the ShipPlayer
					if (ShipPlayer.Instance.Life == 0)
					{
						ExplodeEntities(ShipPlayer.Instance, volume);

						break;
					}
				}

				// Check for collisions between projectiles and other entities
				for (int j = 0; j < entities.Count; j++)
				{
					// Avoid self-collision (no need to check for a collision with itself)
					if (i == j) continue;

					// Check if there's a collision between projectiles and other entities
					if ((entities[i] is Projectile || entities[j] is Projectile) && IsColliding(entities[i], entities[j]))
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
				Sound.PlaySound(Sound.PlayerKilled, volume * 4);

				KillPlayer();
			} else
			{
				Sound.PlaySound(Sound.Explosion, volume * 4);
			}

			_explosion = new AnimatedSprite(Art.Explosion, 6, 6);
			_animatedPosition = entity.Position;
			_isExploding = true;

			// If the entity is an asteroid, add a new asteroid
			//if (entity is Asteroid)
			//{
			//	EntityManager.Add(new Asteroid(Assets.AsteroidTexture, new Vector2(0, 0), new Vector2(0, 1), 1));
			//}

		}

	/// <summary>
	/// Method to kill the player
	/// </summary>
	private static void KillPlayer() {
			ShipPlayer.Instance.IsActivee = false;
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
                if (entity.IsActivee)
                {
                    entity.Draw(spriteBatch);
                }
            }

			if (_isExploding)
			{
				_explosion.Draw(spriteBatch, _animatedPosition);
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
