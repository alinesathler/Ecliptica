using Ecliptica.Arts;
using Ecliptica.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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

		List<Firework> fireworks = new List<Firework>();

		List<Firework> fireworksStore = new List<Firework>();

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

			fireworksStore.Add(new Firework(Images.FireworksRocketBlue, 1, 50, Images.FireworksLongBlue, 1, 57, startPosition: EclipticaGame.ScreenSize / 2,
					   startVelocity: new Vector2(0, -100),
					   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketOrange, 1, 50, Images.FireworksLongOrange, 1, 57, startPosition: new Vector2(EclipticaGame.ScreenSize.X * 8 / 10, EclipticaGame.ScreenSize.Y),
					   startVelocity: new Vector2(0, -200),
					   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketBlue, 1, 50, Images.FireworksLongGreen, 1, 54,startPosition: EclipticaGame.ScreenSize/3,
					   startVelocity: new Vector2(0, -150),
					   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketOrange, 1, 50, Images.FireworksCrystalBlue, 1, 82, startPosition: new Vector2(EclipticaGame.ScreenSize.X * 9/10, EclipticaGame.ScreenSize.Y * 9/10),
					   startVelocity: new Vector2(0, -300),
					   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketBlue, 1, 50, Images.FireworksLongBlue, 1, 57, startPosition: EclipticaGame.ScreenSize * 6/10,
		   startVelocity: new Vector2(0, -50),
		   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketOrange, 1, 50, Images.FireworksLongOrange, 1, 57, startPosition: new Vector2(EclipticaGame.ScreenSize.X * 4/10, EclipticaGame.ScreenSize.Y * 8/10),
					   startVelocity: new Vector2(0, -100),
					   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketBlue, 1, 50, Images.FireworksLongGreen, 1, 54, startPosition: EclipticaGame.ScreenSize * 7 / 10,
					   startVelocity: new Vector2(0, -50),
					   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketOrange, 1, 50, Images.FireworksCrystalBlue, 1, 82, startPosition: new Vector2(EclipticaGame.ScreenSize.X /10, EclipticaGame.ScreenSize.Y - 100),
					   startVelocity: new Vector2(0, -400),
					   trailDuration: 2.0f));

			foreach (var firework in fireworksStore)
			{
				fireworks.Add(Firework.Clone(firework));
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			// Update fireworks
			foreach (var firework in fireworks)
			{
				firework.Update(gameTime);
			}

			// Remove finished fireworks
			for (int i = fireworks.Count - 1; i >= 0; i--)
			{
				if (fireworks[i].IsFinished)
				{
					var index = i;
					fireworks[i] = Firework.Clone(fireworksStore[i]);
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			foreach (var firework in fireworks)
				firework.Draw(spriteBatch);
				
		}
	}
}
