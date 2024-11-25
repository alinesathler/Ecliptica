using Ecliptica.Arts;
using Ecliptica.Games;
using Ecliptica.InputHandler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Ecliptica.Screens
{
    internal class TitleScreen : Screen
	{
		private string _message;
		private double _messageTime;

		public TitleScreen()
		{
			_message = "";
			_messageTime = 0.0;

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
			AddButton("Start Game", () => CheckName(), new Vector2 (((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2,
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

		private void CheckName()
		{
			string name = EclipticaGame.PlayerName.Trim();

			if (string.IsNullOrEmpty(name) || name.Length > 12)
			{
				_message = $"Please enter a name between 1 and 12 caracteres";
				_messageTime = 2.0;
			} else
			{
				ScreenManager.ReplaceScreen(new MenuScreen());
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			KeyboardHandler.Update();

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

			// Update time for the message
			if (!string.IsNullOrEmpty(_message))
			{
				_messageTime -= gameTime.ElapsedGameTime.TotalSeconds;
				if (_messageTime <= 0) _message = null;
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

			// Draw the save message if it exists
			if (!string.IsNullOrEmpty(_message))
			{
				Vector2 messageSize = Fonts.FontGameSmall.MeasureString(_message);
				Vector2 messagePosition = new(
					(EclipticaGame.ScreenSize.X - messageSize.X) / 2,
					Buttons[0].Bounds.Y - messageSize.Y - 15
				);
				spriteBatch.DrawString(Fonts.FontGameSmall, _message, messagePosition, HoverColor);
			}
		}
	}
}
