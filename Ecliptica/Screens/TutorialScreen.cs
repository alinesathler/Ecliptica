using Ecliptica.Arts;
using Ecliptica.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Screens
{
	public class TutorialScreen : Screen
	{
		#region Fields
		private readonly string _tutorialMessage;
		private readonly Vector2 _messagePosition;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor to initialize the tutorial screen
		/// </summary>
		public TutorialScreen()
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

			// Tutorial Message
			_tutorialMessage = "Welcome to Ecliptica!\n\n" +
				"Instructions:\n";
			if(EclipticaGame.Instance.platform == Platform.Android || EclipticaGame.Instance.platform == Platform.iOS)
			{
				_tutorialMessage += "- Drag the spaceship to move it.\n" +
					"- Tap the screen to shoot lasers at incoming asteroids.\n";

            } else
			{
				_tutorialMessage += "- Use the arrow keys to move your spaceship.\n" +
				"- Press SPACEBAR to shoot lasers at incoming asteroids.\n" +
                "- Press Ctrl to turn on the turbo speed and move faster.\n";
            }

            _tutorialMessage +=  "- Destroy asteroids to gain points and avoid collisions.\n" +
				"- Collect power-ups to improve your abilities.\n\n" +
				"Goal:\n" +
				"- Destroy as many asteroids as possible within the time limit.\n" +
				"- Stay alive and achieve a high score!\n\n" +
				"Good luck, Captain!";
			Vector2 textSize = Fonts.FontArial.MeasureString(_tutorialMessage);
			_messagePosition = new Vector2((EclipticaGame.ScreenSize.X - textSize.X) / 2, EclipticaGame.ScreenSize.Y / 5 + 15);

			// Buttons
			AddButton("Return", () => ScreenManager.PopScreen(), new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2, _messagePosition.Y + textSize.Y + 15));
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to draw the tutorial screen
		/// </summary>
		/// <param name="spriteBatch"></param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			// Draw logo
			spriteBatch.Draw(Images.Ecliptica, new Rectangle((int)EclipticaGame.ScreenSize.X / 4, 0, (int)EclipticaGame.ScreenSize.X / 2, (int)EclipticaGame.ScreenSize.Y / 5), Color.White);

			// Draw tutorial message
			spriteBatch.DrawString(Fonts.FontArial, _tutorialMessage, _messagePosition, DefaultColor);
		}
		#endregion
	}
}

