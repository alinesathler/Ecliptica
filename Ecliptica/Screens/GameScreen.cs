using Ecliptica.Games;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Ecliptica.Levels;
using Ecliptica.Arts;

namespace Ecliptica.Screens
{
    internal class GameScreen : Screen
	{
		#region Fields
		private readonly ShipPlayer _shipPlayer;

		private string _levelScore;
		private string _gameScore;
		#endregion

		#region Properties
		public static GameScreen Instance { get; private set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor to initialize the game screen
		/// </summary>
		/// <param name="isLoad"></param>
		/// <param name="shipLife"></param>
		/// <param name="levelNumber"></param>
		/// <param name="totalScore"></param>
		public GameScreen(bool isLoad = false, int shipLife = 0, int levelNumber = 1, int totalScore = 0)
		{
			Font = Fonts.FontGame;
			DefaultScale = 0.5f;
			HoverScale = 0.7f;
			DefaultColor = Color.White;
			HoverColor = Color.Yellow;
			ButtonWidth = 450;
			ButtonHeight = 50;

			// Buttons
			AddButton("Pause", () => ScreenManager.Pause(new PauseScreen()), new Vector2((int)EclipticaGame.ScreenSize.X - ButtonWidth - 10,
				(int)EclipticaGame.ScreenSize.Y - ButtonHeight - 8));

			// Initialize the ship player
			_shipPlayer = new ();

			// Clear level and entity managers
			LevelManager.Clear();
			EntityManager.ResetTotalScore();

			// Load the game if it is a load
			if (isLoad)
			{
				_shipPlayer.Life = shipLife;
				EntityManager.SetTotalScore(totalScore);
			}

			// Load the level
			LevelManager.LoadLevels(levelNumber);
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to update the game screen
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			// Check if the level is completed
			if (!LevelTransition.IsTransitioning && LevelManager.CurrentLevel?.IsLevelComplete() == true)
			{
				EntityManager.Clear();
				LevelManager.CurrentLevel?.Clear();

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

		/// <summary>
		/// Method to draw the game screen
		/// </summary>
		/// <param name="spriteBatch"></param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			LevelManager.DrawCurrentLevel(spriteBatch);

			base.Draw(spriteBatch);

			// Draw scores
			_gameScore = "Game Score: " + EntityManager.GetTotalScore() ?? "0";

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

			_levelScore = "Level Score: " + EntityManager.GetLevelScore() ?? "0";

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
		#endregion
	}
}
