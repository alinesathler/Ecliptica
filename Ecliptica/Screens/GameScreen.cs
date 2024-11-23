using Ecliptica.Games;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Ecliptica.Levels;
using Microsoft.Xna.Framework.Input;
using System;
using Ecliptica.Arts;
using static System.Net.Mime.MediaTypeNames;

namespace Ecliptica.Screens
{
	internal class GameScreen : Screen
	{
		public static GameScreen Instance { get; private set; }

		private ShipPlayer _shipPlayer;

		private Rectangle _pausenButtonRect;
		private Button _pauseButton;

		string _levelScore;
		string _gameScore;

		public GameScreen()
		{
			Font = Fonts.FontGame;
			DefaultScale = 0.5f;
			HoverScale = 0.7f;
			DefaultColor = Color.White;
			HoverColor = Color.Yellow;
			ButtonWidth = 450;
			ButtonHeight = 50;

			// Pause button
			_pausenButtonRect = new Rectangle(
				(int)EclipticaGame.ScreenSize.X - ButtonWidth - 10,
				(int)EclipticaGame.ScreenSize.Y - ButtonHeight - 8,
				ButtonWidth,
				ButtonHeight);

			_pauseButton = new Button(
				"Pause",
				_pausenButtonRect,
				Font,
				DefaultScale,
				HoverScale,
				DefaultColor,
				HoverColor,
				() => ScreenManager.Pause(new PauseScreen())
			);

			_shipPlayer = new ShipPlayer();

			LevelManager.Clear();

			EntityManager.ResetEnemiesDestroyedTotal()	;

			LevelManager.LoadLevels();

			Buttons.Add(_pauseButton);
		}

		public override void Update(GameTime gameTime)
		{
			// Check if the level is completed
			if (!LevelTransition.IsTransitioning && LevelManager.CurrentLevel?.IsLevelComplete() == true)
			{
				if (!LevelManager.IsLastLevel())
				{
					LevelTransition.StartTransition();
				} else
				{
					ScreenManager.ReplaceScreen(new WinScreen());

					EntityManager.Explosion = null;
				}
			}

			LevelManager.UpdateCurrentLevel(gameTime);

			base.Update(gameTime);

			_shipPlayer.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			LevelManager.DrawCurrentLevel(spriteBatch);

			base.Draw(spriteBatch);

			_gameScore = "Game Score: " + EntityManager.GetNumberOfEnemiesDestroyedTotal() ?? "0";

			Vector2 textSizeGameScore = Font.MeasureString(_gameScore);
			Vector2 originGameScore = new (-10, 0);
			Vector2 positionGameScore = new (0, EclipticaGame.ScreenSize.Y - textSizeGameScore.Y);

			spriteBatch.DrawString(
				Font,
				_gameScore,
				positionGameScore,
				DefaultColor,
				0f,
				originGameScore,
				DefaultScale,
				SpriteEffects.None,
				0f);

			_levelScore = "Level Score: " + EntityManager.GetNumberOfEnemiesDestroyedLevel() ?? "0";

			Vector2 textSizeLevelScore = Font.MeasureString(_levelScore);
			Vector2 originLevelScore = new (textSizeLevelScore.X / 2, 0);
			Vector2 positionLevelScore = new ((EclipticaGame.ScreenSize.X / 2), EclipticaGame.ScreenSize.Y - textSizeGameScore.Y);

			spriteBatch.DrawString(
				Font,
				_levelScore,
				positionLevelScore,
				DefaultColor,
				0f,
				originLevelScore,
				DefaultScale,
				SpriteEffects.None,
				0f);

			LevelTransition.DrawTransition(spriteBatch);
		}
	}
}
