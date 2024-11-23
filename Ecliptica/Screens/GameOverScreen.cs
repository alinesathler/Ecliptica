using Ecliptica.Arts;
using Ecliptica.Screens;
using Microsoft.Xna.Framework;

namespace Ecliptica.Games
{
	public class GameOverScreen : Screen
	{
		public GameOverScreen()
		{
			Music = Sounds.GameOver;
			BackgroundSolid = Images.BackgroundGameOver;
			BackgroundStars = Images.BackgroundStars;
			Font = Fonts.FontGame;
			DefaultScale = 1.0f;
			HoverScale = 1.2f;
			DefaultColor = Color.White;
			HoverColor = Color.Yellow;
			ButtonWidth = 450;
			ButtonHeight = 50;

			//Buttons
			AddButton("Play Again", () => ScreenManager.ReplaceScreen(new GameScreen()), new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2,
				(int)EclipticaGame.ScreenSize.Y / 2 + ButtonHeight));
			AddButton("Main Menu", () => ScreenManager.ReplaceScreen(new MenuScreen()));
			AddButton("High Scores", () => ScreenManager.PushScreen(new ScoresScreen()));
			AddButton("Exit", () => EclipticaGame.Instance.Exit());
		}
	}
}
