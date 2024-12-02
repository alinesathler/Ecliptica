using Ecliptica.Arts;
using Ecliptica.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Screens
{
	internal class PauseScreen : Screen
	{
		#region Fields
		public bool isPaused;

		private readonly static float _screenAlpha = 0.6f;
		#endregion

		#region Properties
		public static PauseScreen Instance { get; private set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor to initialize the pause screen
		/// </summary>
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
            AddButton("Load Game", () => { isPaused = false; ScreenManager.PushScreen(new LoadScreen()); });
            AddButton("Main Menu", () => { isPaused = false; ScreenManager.ReplaceScreen(new MenuScreen()); });
			AddButton("Exit", () => { isPaused = false; EclipticaGame.Instance.Exit(); });
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to draw the pause screen
		/// </summary>
		/// <param name="spriteBatch"></param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			// Draw the background
			spriteBatch.Draw(BackgroundSolid, new Rectangle(0, 0, (int)EclipticaGame.ScreenSize.X, (int)EclipticaGame.ScreenSize.Y), DefaultColor);
			spriteBatch.Draw(BackgroundStars, new Rectangle(0, 0, (int)EclipticaGame.ScreenSize.X, (int)EclipticaGame.ScreenSize.Y), DefaultColor);

			// Draw the background with alpha
			spriteBatch.Draw(
				texture: CreateBlankTexture(spriteBatch.GraphicsDevice, Color.Black),
				destinationRectangle: new Rectangle(0, 0, (int)EclipticaGame.ScreenSize.X, (int)EclipticaGame.ScreenSize.Y),
				color: Color.Black * _screenAlpha
			);

			// Draw logo
			spriteBatch.Draw(Images.Ecliptica, new Rectangle((int)EclipticaGame.ScreenSize.X / 4, ((int)EclipticaGame.ScreenSize.Y - Images.Ecliptica.Height) / 2, (int)EclipticaGame.ScreenSize.X / 2, (int)EclipticaGame.ScreenSize.Y / 5), Color.White);

			base.Draw(spriteBatch);
		}
		#endregion
	}
}
