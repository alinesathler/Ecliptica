using Ecliptica.Arts;
using Ecliptica.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecliptica.Screens
{
	internal class PauseScreen : Screen
	{
		public bool isPaused;

		private Rectangle _resumeButtonRect;
		private Button _resumeButton;

		private Rectangle _restartButtonRect;
		private Button _restartButton;

		private Rectangle _mainMenuButtonRect;
		private Button _mainMenuButton;

		private Rectangle _exitButtonRect;
		private Button _exitButton;

		private static float _screenAlpha = 0.6f;

		public static PauseScreen Instance { get; private set; }

		public PauseScreen()
		{
			Instance = this;

			Music = Sounds.MusicTheme;
			BackgroundSolid = Images.BackgroundTitle;
			BackgroundStars = Images.BackgroundStars1;
			Font = Fonts.FontGame;
			DefaultScale = 1.0f;
			HoverScale = 1.2f;
			DefaultColor = Color.White;
			HoverColor = Color.Yellow;
			ButtonWidth = 450;
			ButtonHeight = 50;

			isPaused = true;

			// Resume button
			_resumeButtonRect = new Rectangle(
				((int)EclipticaGame.ScreenSize.X - ButtonWidth)/ 2,
				((int)EclipticaGame.ScreenSize.Y - ButtonHeight)/ 2,
				ButtonWidth,
				ButtonHeight);

			_resumeButton = new Button(
			"Resume",
			_resumeButtonRect,
			Font,
			DefaultScale,
			HoverScale,
			DefaultColor,
			HoverColor,
			() => {
				isPaused = false;

				ScreenManager.PopScreen();
			});

			Buttons.Add(_resumeButton);

			// Restart button
			_restartButtonRect = new Rectangle(
				Buttons[0].Bounds.X, Buttons[0].Bounds.Y + (ButtonHeight + 10) * Buttons.Count,
				ButtonWidth,
				ButtonHeight);

			_restartButton = new Button(
				"Restart",
				_restartButtonRect,
				Font,
				DefaultScale,
				HoverScale,
				DefaultColor,
				HoverColor,
				() =>
				{
					isPaused = false;

					ScreenManager.ReplaceScreen(new GameScreen());
				}
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
				() =>
				{
					isPaused = false;

					ScreenManager.ReplaceScreen(new MenuScreen());
				}
			);

			Buttons.Add(_mainMenuButton);

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
				() =>
				{
					isPaused = false;

					EclipticaGame.Instance.Exit();
				}
			);

			Buttons.Add(_exitButton);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{ 
			spriteBatch.Draw(BackgroundSolid, new Rectangle(0, 0, (int)EclipticaGame.ScreenSize.X, (int)EclipticaGame.ScreenSize.Y), Color.White);
			spriteBatch.Draw(BackgroundStars, new Rectangle(0, 0, (int)EclipticaGame.ScreenSize.X, (int)EclipticaGame.ScreenSize.Y), Color.White);

			// Draw the background with alpha
			spriteBatch.Draw(
				texture: CreateBlankTexture(spriteBatch.GraphicsDevice, Color.Black),
				destinationRectangle: new Rectangle(0, 0, (int)EclipticaGame.ScreenSize.X, (int)EclipticaGame.ScreenSize.Y),
				color: Color.Black * _screenAlpha
			);

			base.Draw(spriteBatch);
		}
	}
}
