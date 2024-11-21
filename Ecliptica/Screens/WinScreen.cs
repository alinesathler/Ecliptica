using Ecliptica.Arts;
using Ecliptica.Games;
using Microsoft.Xna.Framework;

namespace Ecliptica.Screens
{
	internal class WinScreen : Screen
	{
		private Rectangle _restartButtonRect;
		private Button _restartButton;

		private Rectangle _mainMenuButtonRect;
		private Button _mainMenuButton;

		private Rectangle _scoreButtonRect;
		private Button _scoreButton;

		private Rectangle _exitButtonRect;
		private Button _exitButton;

		public WinScreen()
		{
			Music = Sounds.GameEnd;
			BackgroundSolid = Images.BackgroundYouWin;
			BackgroundStars = Images.BackgroundStars1;
			Font = Fonts.FontGame;
			DefaultScale = 1.0f;
			HoverScale = 1.2f;
			DefaultColor = Color.White;
			HoverColor = Color.Yellow;
			ButtonWidth = 450;
			ButtonHeight = 50;

			// Restart button
			_restartButtonRect = new Rectangle(
				((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2,
				(int)EclipticaGame.ScreenSize.Y / 2 + ButtonHeight,
				ButtonWidth,
				ButtonHeight);

			_restartButton = new Button(
				"Play Again",
				_restartButtonRect,
				Font,
				DefaultScale,
				HoverScale,
				DefaultColor,
				HoverColor,
				() => ScreenManager.ReplaceScreen(new GameScreen())
			);

			Buttons.Add(_restartButton);

			// Main Menu button
			_mainMenuButtonRect = new Rectangle
				(Buttons[0].Bounds.X, Buttons[0].Bounds.Y + (ButtonHeight + 10) * Buttons.Count,
				ButtonWidth,
				ButtonHeight);

			_mainMenuButton = new Button(
				"Main Menu",
				_mainMenuButtonRect,
				Font,
				DefaultScale,
				HoverScale,
				DefaultColor,
				HoverColor,
				() => ScreenManager.ReplaceScreen(new MenuScreen())
			);

			Buttons.Add(_mainMenuButton);

			// Scores button
			_scoreButtonRect = new Rectangle(Buttons[0].Bounds.X, Buttons[0].Bounds.Y + (ButtonHeight + 10) * Buttons.Count, ButtonWidth,
				ButtonHeight);

			_scoreButton = new Button(
				"High Scores",
				_scoreButtonRect,
				Font,
				DefaultScale,
				HoverScale,
				DefaultColor,
				HoverColor,
				() => ScreenManager.PushScreen(new ScoresScreen())
			);

			Buttons.Add(_scoreButton);

			// Exit button
			_exitButtonRect = new Rectangle(Buttons[0].Bounds.X, Buttons[0].Bounds.Y + (ButtonHeight + 10) * Buttons.Count, ButtonWidth,
				ButtonHeight);

			_exitButton = new Button(
				"Exit",
				_exitButtonRect,
				Font,
				DefaultScale,
				HoverScale,
				DefaultColor,
				HoverColor,
				() => EclipticaGame.Instance.Exit()
			);

			Buttons.Add(_exitButton);
		}
	}
}
