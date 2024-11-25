using Ecliptica.Arts;
using Ecliptica.Screens;
using Ecliptica.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;

namespace Ecliptica.Games
{
	public class AboutScreen : Screen
	{
		private readonly string message1;
		private readonly string message2;
		private readonly Vector2 textPosition1;
		private readonly Vector2 textPosition2;

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
			message1 = "Ecliptica is a game developed in the context of the course 'PROG2370 - Game Programming with Data Structures' of the diploma in 'Computer Programming' from 'Conestoga Colege'." +
				" Ecliptica is a thrilling 2D space shooter adventure game where the player must destroy asteroids of different sizes and speed in the available time." +
				" Face epic battles, unravel mysteries, and conquer the stars!";

			message1 = TextManipulation.WrapText(Fonts.FontArial, message1, EclipticaGame.ScreenSize.X * 8 / 10);

			message2 = "Developed by: Aline Sathler\n" +
				"Tools Used: Microsoft XNA Framework, Visual Studio, Microsoft Designer\n" +
				"Contact: Email: sathler@ymail.com\n" +
				"Version: 1.0.0(November 2024)\n" +
				"Copyright 2024 Sathler Studios. All rights reserved.\n" +
				"Thanks for playing!";

			// Calculate Text Positions
			Vector2 textSize1 = Fonts.FontArial.MeasureString(message1);
			textPosition1 = new Vector2((EclipticaGame.ScreenSize.X - textSize1.X) / 2, EclipticaGame.ScreenSize.Y / 5 + 15);

			Vector2 textSize2 = Fonts.FontArial.MeasureString(message2);
			textPosition2 = new Vector2(textPosition1.X, textPosition1.Y + textSize1.Y + 15);

			// Buttons
			AddButton("Return", () => ScreenManager.PopScreen(), new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2, textPosition2.Y + textSize2.Y + 15));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			// Draw logo
			spriteBatch.Draw(Images.Ecliptica, new Rectangle((int)EclipticaGame.ScreenSize.X / 4, 0, (int)EclipticaGame.ScreenSize.X / 2, (int)EclipticaGame.ScreenSize.Y / 5), Color.White);

			// Draw text
			spriteBatch.DrawString(Fonts.FontArial, message1, textPosition1, DefaultColor);
			spriteBatch.DrawString(Fonts.FontArial, message2, textPosition2, DefaultColor);
		}
	}
}
