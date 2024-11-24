using Ecliptica.Arts;
using Ecliptica.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Ecliptica.Screens
{
	internal class TitleScreen : Screen
	{
		public TitleScreen()
		{
			Music = Sounds.TitleScreen;
			BackgroundSolid = Images.BackgroundTitle;
			BackgroundStars = Images.BackgroundStars1;
			Font = Fonts.FontGame;
			DefaultScale = 1.0f;
			HoverScale = 1.2f;
			DefaultColor = Color.White;
			HoverColor = Color.Yellow;
			ButtonWidth = 450;
			ButtonHeight = 50;

			// Buttons
			AddButton("Start Game", () => ScreenManager.ReplaceScreen(new MenuScreen()), new Vector2 (((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2,
				((int)EclipticaGame.ScreenSize.Y - 2 * ButtonHeight)));
		}

		private void HandleKeyPress(Keys key)
		{
			if (key == Keys.Back && EclipticaGame.PlayerName.Length > 0)
			{
				EclipticaGame.PlayerName = EclipticaGame.PlayerName[..^1]; // Remove the last character
			} else if (key == Keys.Space)
			{
				EclipticaGame.PlayerName += ' ';
			} else if (key.ToString().Length == 1) // Printable character
			{
				if (EclipticaGame.PlayerName.Length < 12) // Limit name length
				{
					EclipticaGame.PlayerName += key.ToString();
				}
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			KeyboardHandler.Update(); // Update the keyboard state

			// Handle key presses
			foreach (Keys key in Enum.GetValues(typeof(Keys)))
			{
				if (KeyboardHandler.IsKeyPressed(key))
				{
					HandleKeyPress(key);
				}
			}

			// Start the game when Enter is pressed
			if (KeyboardHandler.IsKeyPressed(Keys.Enter))
			{
				Buttons[0].OnClick();
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			//string title = "Ecliptica";

			//spriteBatch.DrawString(Fonts.FontTitle, title, new Vector2((EclipticaGame.ScreenSize.X - Fonts.FontTitle.MeasureString(title).X) / 2, EclipticaGame.ScreenSize.Y / 4), DefaultColor);

			spriteBatch.Draw(Images.Ecliptica, new Rectangle((int)EclipticaGame.ScreenSize.X / 4, 0, (int)EclipticaGame.ScreenSize.X/2, (int)EclipticaGame.ScreenSize.Y/5), Color.White);

			string prompt = "Enter your name:";

			Vector2 promptPosition = new Vector2(
				(EclipticaGame.ScreenSize.X - Font.MeasureString(prompt).X) / 2,
				EclipticaGame.ScreenSize.Y / 2 - 50
			);

			Vector2 namePosition = new Vector2(
				(EclipticaGame.ScreenSize.X - Font.MeasureString(EclipticaGame.PlayerName).X) / 2,
				EclipticaGame.ScreenSize.Y / 2
			);

			spriteBatch.DrawString(Font, prompt, promptPosition, DefaultColor);
			spriteBatch.DrawString(Font, EclipticaGame.PlayerName, namePosition, DefaultColor);
		}
	}
}
