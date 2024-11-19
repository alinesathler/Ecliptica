using Ecliptica.Games;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecliptica.Levels
{
	public static class LevelTransition
	{
		public static bool IsTransitioning { get; private set; }
		public static float TransitionTime { get; private set; }
		public static float MaxTransitionTime { get; private set; } = 5.0f;

		/// <summary>
		/// Marks the start of a transition between levels.
		/// </summary>
		public static void StartTransition()
		{
			if (IsTransitioning)
				return;
			IsTransitioning = true;
			TransitionTime = 0f;
			Sound.StopMusic();
			Sound.PlayMusic(Sound.LevelComplete);
		}

		/// <summary>
		/// Method to update the transition between levels
		/// </summary>
		/// <param name="gameTime"></param>
		public static void UpdateTransition(GameTime gameTime)
		{
			if (IsTransitioning)
			{
				TransitionTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (TransitionTime >= MaxTransitionTime)
				{
					IsTransitioning = false;
					LevelManager.NextLevel();
				}
			}
		}

		/// <summary>
		/// Method to draw the transition between levels
		/// </summary>
		/// <param name="spriteBatch"></param>
		public static void DrawTransition(SpriteBatch spriteBatch)
		{
			if (IsTransitioning)
			{
				float alpha = MathHelper.Clamp(TransitionTime / MaxTransitionTime, 0f, 1f);
				//spriteBatch.Begin();
				//spriteBatch.Draw(Art.WhiteTexture, new Rectangle(0, 0, EclipticaGame.ScreenSize.X, EclipticaGame.ScreenSize.Y), Color.Black * alpha);
				//spriteBatch.End();
			}
		}
	}

}
