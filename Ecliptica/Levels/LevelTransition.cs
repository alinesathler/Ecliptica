﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Ecliptica.Arts;
using Ecliptica.Screens;

namespace Ecliptica.Levels
{
	public static class LevelTransition
	{
		#region Properties
		public static bool IsTransitioning { get; private set; }
		public static float TransitionTime { get; private set; }
		public static float MaxTransitionTime { get; private set; } = 5.0f;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor to initialize the level transition
		/// </summary>
		static LevelTransition()
		{
			ScreenManager.OnScreenPopped += HandleScreenPopped;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Marks the start of a transition between levels.
		/// </summary>
		public static void StartTransition()
		{
			if (IsTransitioning)
				return;
			IsTransitioning = true;
			TransitionTime = 0f;
			Sounds.StopMusic();
			Sounds.PlayMusic(Sounds.LevelComplete);
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

					ScreenManager.PushScreen(new SaveScreen());
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

				spriteBatch.Draw(Images.BackgroundLevelWin, new Rectangle(0, 0, (int)EclipticaGame.ScreenSize.X, (int)EclipticaGame.ScreenSize.Y), Color.White * alpha);
			}
		}

		/// <summary>
		/// Method to handle the screen popped event of the Save Screen
		/// </summary>
		private static void HandleScreenPopped()
		{
			if (!IsTransitioning && ScreenManager.CurrentScreen() is SaveScreen)
			{
				LevelManager.NextLevel();
			}
		}
		#endregion
	}
}
