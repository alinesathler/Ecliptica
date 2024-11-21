using Ecliptica.Arts;
using Ecliptica.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Ecliptica.Games
{
	public class MenuScreen : Screen
	{
		private Rectangle _startButtonRect;
		private Button _startButton;

		private Rectangle _loadButtonRect;
		private Button _loadButton;

		private Rectangle _scoresButtonRect;
		private Button _scoresButton;

		private Rectangle _tutorialButtonRect;
		private Button _tutorialButton;

		private Rectangle _aboutButtonRect;
		private Button _aboutButton;

		private Rectangle _exitButtonRect;
		private Button _exitButton;

		public MenuScreen()
		{
			Music = Sounds.MenuScreen;
			BackgroundSolid = Images.BackgroundScreens;
			BackgroundStars = Images.BackgroundStars1;
			Font = Fonts.FontGame;
			DefaultScale = 1.0f;
			HoverScale = 1.2f;
			DefaultColor = Color.White;
			HoverColor = Color.Yellow;
			ButtonWidth = 450;
			ButtonHeight = 50;
			AnimatedSprite _explosion;
			Vector2 _animatedPosition;


			// Start button
			_startButtonRect = new Rectangle(
				((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2,
				((int)EclipticaGame.ScreenSize.Y) / 4,
				ButtonWidth,
				ButtonHeight);

			_startButton = new Button(
				"Start Game",
				_startButtonRect,
				Font,
				DefaultScale,
				HoverScale,
				DefaultColor,
				HoverColor,
				() => ScreenManager.ReplaceScreen(new GameScreen()));

			Buttons.Add(_startButton);

			// Load button
			_loadButtonRect = new Rectangle(Buttons[0].Bounds.X, Buttons[0].Bounds.Y + (ButtonHeight + 10) * Buttons.Count, ButtonWidth, ButtonHeight);

			_loadButton = new Button(
				"Load Game",
				_loadButtonRect,
				Font,
				DefaultScale,
				HoverScale,
				DefaultColor,
				HoverColor,
				() => ScreenManager.PushScreen(new LoadScreen())
			);

			Buttons.Add(_loadButton);

			// Scores button
			_scoresButtonRect = new Rectangle(Buttons[0].Bounds.X, Buttons[0].Bounds.Y + (ButtonHeight + 10) * Buttons.Count, ButtonWidth, ButtonHeight);

			_scoresButton = new Button(
				"High Scores",
				_scoresButtonRect,
				Font,
				DefaultScale,
				HoverScale,
				DefaultColor,
				HoverColor,
				() => ScreenManager.PushScreen(new ScoresScreen())
			);

			Buttons.Add(_scoresButton);

			// Tutorial button
			_tutorialButtonRect = new Rectangle(Buttons[0].Bounds.X, Buttons[0].Bounds.Y + (ButtonHeight + 10) * Buttons.Count, ButtonWidth, ButtonHeight);

			_tutorialButton = new Button(
				"Tutorial",
				_tutorialButtonRect,
				Font,
				DefaultScale,
				HoverScale,
				DefaultColor,
				HoverColor,
				() => ScreenManager.PushScreen(new TutorialScreen())
			);

			Buttons.Add(_tutorialButton);

			// About button
			_aboutButtonRect = new Rectangle(Buttons[0].Bounds.X, Buttons[0].Bounds.Y + (ButtonHeight + 10) * Buttons.Count, ButtonWidth, ButtonHeight);

			_aboutButton = new Button(
				"About",
				_aboutButtonRect,
				Font,
				DefaultScale,
				HoverScale,
				DefaultColor,
				HoverColor,
				() => ScreenManager.PushScreen(new AboutScreen())
			);

			Buttons.Add(_aboutButton);

			// Exit button
			_exitButtonRect = new Rectangle(Buttons[0].Bounds.X, Buttons[0].Bounds.Y + (ButtonHeight + 10) * Buttons.Count, ButtonWidth, ButtonHeight);

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

			_explosion = new AnimatedSprite(Images.FireworksCrystalBlue, 1, 60, 0.05f);
			_animatedPosition = new Vector2(50, 50);
		}
	}
}
