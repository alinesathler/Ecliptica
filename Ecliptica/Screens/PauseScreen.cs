using Ecliptica.Arts;
using Ecliptica.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecliptica.Screens
{
	internal class PauseScreen : Screen
	{
		public bool isPaused;

		private static float _screenAlpha = 0.6f;

		public static PauseScreen Instance { get; private set; }

		public PauseScreen()
		{
			Instance = this;

			Music = Sounds.MusicTheme;
			BackgroundSolid = Images.BackgroundTitle;
			BackgroundStars = Images.BackgroundStars1;
			Font = Fonts.FontGame;
			DefaultScale = 1.0f;
			HoverScale = 1.2f;
			DefaultColor = Color.White;
			HoverColor = Color.Yellow;
			ButtonWidth = 450;
			ButtonHeight = 50;

			isPaused = true;

			// Buttons
			AddButton("Resume", () => { isPaused = false; ScreenManager.PopScreen(); }, new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2, ((int)EclipticaGame.ScreenSize.Y) * 2/3));
			AddButton("Restart", () => { isPaused = false; ScreenManager.ReplaceScreen(new GameScreen()); });
			AddButton("Main Menu", () => { isPaused = false; ScreenManager.ReplaceScreen(new MenuScreen()); });
			AddButton("Exit", () => { isPaused = false; EclipticaGame.Instance.Exit(); });
		}

		public override void Draw(SpriteBatch spriteBatch)
		{ 
			spriteBatch.Draw(BackgroundSolid, new Rectangle(0, 0, (int)EclipticaGame.ScreenSize.X, (int)EclipticaGame.ScreenSize.Y), Color.White);
			spriteBatch.Draw(BackgroundStars, new Rectangle(0, 0, (int)EclipticaGame.ScreenSize.X, (int)EclipticaGame.ScreenSize.Y), Color.White);

			// Draw the background with alpha
			spriteBatch.Draw(
				texture: CreateBlankTexture(spriteBatch.GraphicsDevice, Color.Black),
				destinationRectangle: new Rectangle(0, 0, (int)EclipticaGame.ScreenSize.X, (int)EclipticaGame.ScreenSize.Y),
				color: Color.Black * _screenAlpha
			);

			spriteBatch.Draw(Images.Ecliptica, new Rectangle((int)EclipticaGame.ScreenSize.X / 4, ((int)EclipticaGame.ScreenSize.Y - Images.Ecliptica.Height) / 2, (int)EclipticaGame.ScreenSize.X / 2, (int)EclipticaGame.ScreenSize.Y / 5), Color.White);

			base.Draw(spriteBatch);
		}
	}
}
