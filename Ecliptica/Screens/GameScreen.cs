using Ecliptica.Games;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Ecliptica.Levels;

namespace Ecliptica.Screens
{
	internal class GameScreen : Screen
	{
		public GameScreen(Texture2D backgroundSolid, Texture2D backgroundStars, SpriteFont font, Song backgroundMusic)
			: base(backgroundSolid, backgroundStars, backgroundMusic, font)
		{
			LevelManager.LoadLevels();
		}

		public override void Update(GameTime gameTime)
		{
			// Check if the level is completed
			if (!LevelTransition.IsTransitioning && LevelManager.CurrentLevel.EnemyCount == EntityManager.GetNumberOfEnemiesDestroyedLevel())
			{
				LevelTransition.StartTransition();
			}

			LevelManager.UpdateCurrentLevel(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			LevelManager.DrawCurrentLevel(spriteBatch);
			LevelTransition.DrawTransition(spriteBatch);
		}
	}
}
