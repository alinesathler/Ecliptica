using Ecliptica.Games;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecliptica.Arts;
using static System.Net.Mime.MediaTypeNames;

namespace Ecliptica.Screens
{
	internal class LoadScreen : Screen
	{
		private Rectangle _returnButtonRect;
		private Button _returnButton;

		public LoadScreen()
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
