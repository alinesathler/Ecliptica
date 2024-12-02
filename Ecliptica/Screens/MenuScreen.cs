using Ecliptica.Arts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Screens
{
	public class MenuScreen : Screen
	{
		#region Constructors
		/// <summary>
		/// Constructor to initialize the menu screen
		/// </summary>
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
			ButtonHeight = 75;

			// Buttons
			AddButton("Start Game", () => ScreenManager.ReplaceScreen(new GameScreen()), new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2, (int)EclipticaGame.ScreenSize.Y / 3));
			AddButton("Load Game", () => ScreenManager.PushScreen(new LoadScreen()));
			AddButton("High Scores", () => ScreenManager.PushScreen(new ScoresScreen()));
			AddButton("Tutorial", () => ScreenManager.PushScreen(new TutorialScreen()));
			AddButton("About", () => ScreenManager.PushScreen(new AboutScreen()));
			AddButton("Exit", () => EclipticaGame.Instance.Exit());
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to draw the menu screen
		/// </summary>
		/// <param name="spriteBatch"></param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			// Draw logo
			spriteBatch.Draw(Images.Ecliptica, new Rectangle((int)EclipticaGame.ScreenSize.X / 4, 0, (int)EclipticaGame.ScreenSize.X / 2, (int)EclipticaGame.ScreenSize.Y / 5), Color.White);
		}
		#endregion
	}
}
