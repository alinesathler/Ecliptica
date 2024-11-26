using Ecliptica.Arts;
using Ecliptica.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Screens
{
	public class AboutScreen : Screen
	{
		#region Fields
		private readonly string _message1;
		private readonly string _message2;
		private readonly Vector2 _textPosition1;
		private readonly Vector2 _textPosition2;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor to initialize the about screen
		/// </summary>
		public AboutScreen()
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

			// Text
			_message1 = "Ecliptica is a game developed in the context of the course 'PROG2370 - Game Programming with Data Structures' of the diploma in 'Computer Programming' from 'Conestoga Colege'." +
				" Ecliptica is a thrilling 2D space shooter adventure game where the player must destroy asteroids of different sizes and speed in the available time." +
				" Face epic battles, unravel mysteries, and conquer the stars!";
			_message1 = TextManipulation.WrapText(Fonts.FontArial, _message1, EclipticaGame.ScreenSize.X * 8 / 10);

			_message2 = "Developed by: Aline Sathler\n" +
				"Tools Used: Microsoft XNA Framework, Visual Studio, Microsoft Designer\n" +
				"Contact: Email: sathler@ymail.com\n" +
				"Version: 1.0.0(November 2024)\n" +
				"Copyright 2024 Sathler Studios. All rights reserved.\n" +
				"Thanks for playing!";

			// Calculate Text Positions
			Vector2 textSize1 = Fonts.FontArial.MeasureString(_message1);
			_textPosition1 = new Vector2((EclipticaGame.ScreenSize.X - textSize1.X) / 2, EclipticaGame.ScreenSize.Y / 5 + 15);

			Vector2 textSize2 = Fonts.FontArial.MeasureString(_message2);
			_textPosition2 = new Vector2(_textPosition1.X, _textPosition1.Y + textSize1.Y + 15);

			// Buttons
			AddButton("Return", () => ScreenManager.PopScreen(), new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2, _textPosition2.Y + textSize2.Y + 15));
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to draw the about screen
		/// </summary>
		/// <param name="spriteBatch"></param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			// Draw logo
			spriteBatch.Draw(Images.Ecliptica, new Rectangle((int)EclipticaGame.ScreenSize.X / 4, 0, (int)EclipticaGame.ScreenSize.X / 2, (int)EclipticaGame.ScreenSize.Y / 5), Color.White);

			// Draw text
			spriteBatch.DrawString(Fonts.FontArial, _message1, _textPosition1, DefaultColor);
			spriteBatch.DrawString(Fonts.FontArial, _message2, _textPosition2, DefaultColor);
		}
		#endregion
	}
}
