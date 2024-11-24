using Ecliptica.Arts;
using Ecliptica.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Games
{
	public class MenuScreen : Screen
	{
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
			ButtonHeight = 50;

			// Buttons
			AddButton("Start Game", () => ScreenManager.ReplaceScreen(new GameScreen()), new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2, (int)EclipticaGame.ScreenSize.Y / 3));
			AddButton("Load Game", () => ScreenManager.PushScreen(new LoadScreen()));
			AddButton("High Scores", () => ScreenManager.PushScreen(new ScoresScreen()));
			AddButton("Tutorial", () => ScreenManager.PushScreen(new TutorialScreen()));
			AddButton("About", () => ScreenManager.PushScreen(new AboutScreen()));
			AddButton("Exit", () => EclipticaGame.Instance.Exit());
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			spriteBatch.Draw(Images.Ecliptica, new Rectangle((int)EclipticaGame.ScreenSize.X / 4, 0, (int)EclipticaGame.ScreenSize.X / 2, (int)EclipticaGame.ScreenSize.Y / 5), Color.White);
		}
	}
}
