//using Ecliptica.Arts;
//using Ecliptica.Screens;
//using Microsoft.Xna.Framework;

//namespace Ecliptica.Games
//{
//	public class TutorialScreen : Screen
//	{
//		public TutorialScreen()
//		{
//			Music = Sounds.MenuScreen;
//			BackgroundSolid = Images.BackgroundScreens;
//			BackgroundStars = Images.BackgroundStars1;
//			Font = Fonts.FontGame;
//			DefaultScale = 1.0f;
//			HoverScale = 1.2f;
//			DefaultColor = Color.White;
//			HoverColor = Color.Yellow;
//			ButtonWidth = 450;
//			ButtonHeight = 50;

//			// Buttons
//			AddButton("Return", () => ScreenManager.PopScreen(), new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2, 10));
//		}
//	}
//}

using Ecliptica.Arts;
using Ecliptica.Screens;
using Ecliptica.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Games
{
	public class TutorialScreen : Screen
	{
		private readonly string tutorialMessage;
		private readonly Vector2 messagePosition;

		public TutorialScreen()
		{
			// Screen settings
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

			// Tutorial Message
			tutorialMessage = "Welcome to Ecliptica!\n\n" +
				"Instructions:\n" +
				"- Use the arrow keys to move your spaceship.\n" +
				"- Press SPACEBAR to shoot lasers at incoming asteroids.\n" +
				"- Destroy asteroids to gain points and avoid collisions.\n" +
				"- Collect power-ups to improve your abilities.\n\n" +
				"Goal:\n" +
				"- Destroy as many asteroids as possible within the time limit.\n" +
				"- Stay alive and achieve a high score!\n\n" +
				"Good luck, Captain!";

			//tutorialMessage = TextManipulation.WrapText(Fonts.FontArial, tutorialMessage, EclipticaGame.ScreenSize.X * 95 / 100);

			// Calculate text position
			Vector2 textSize = Fonts.FontArial.MeasureString(tutorialMessage);
			messagePosition = new Vector2((EclipticaGame.ScreenSize.X - textSize.X) / 2, EclipticaGame.ScreenSize.Y / 5 + 15);

			// Buttons
			AddButton("Return", () => ScreenManager.PopScreen(), new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2, messagePosition.Y + textSize.Y + 15));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			// Draw logo
			spriteBatch.Draw(Images.Ecliptica, new Rectangle((int)EclipticaGame.ScreenSize.X / 4, 0, (int)EclipticaGame.ScreenSize.X / 2, (int)EclipticaGame.ScreenSize.Y / 5), Color.White);

			// Draw tutorial message
			spriteBatch.DrawString(Fonts.FontArial, tutorialMessage, messagePosition, DefaultColor);
		}
	}
}

