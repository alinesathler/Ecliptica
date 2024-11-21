using Ecliptica.Arts;
using Ecliptica.Screens;
using Microsoft.Xna.Framework;

namespace Ecliptica.Games
{
	public class TutorialScreen : Screen
	{
		private Rectangle _returnButtonRect;
		private Button _returnButton;

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

			// Return button
			_returnButtonRect = new Rectangle(
				((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2,
				10,
				ButtonWidth,
				ButtonHeight);

			_returnButton = new Button(
				"Return",
				_returnButtonRect,
				Font,
				DefaultScale,
				HoverScale,
				DefaultColor,
				HoverColor,
				() => ScreenManager.PopScreen()
			);

			Buttons.Add(_returnButton);
		}
	}
}
