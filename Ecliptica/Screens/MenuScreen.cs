using Ecliptica.Arts;
using Ecliptica.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ecliptica.Games
{
	public class MenuScreen : Screen
	{
		private string _startButtonText = "Start Game";
		private Rectangle _startButtonRect;
		private Vector2 _startButtonCenter;
		float _fontScale;

		public MenuScreen(Texture2D backgroundSolid, Texture2D backgroundStars, SpriteFont font, Song backgroundMusic)
			: base(backgroundSolid, backgroundStars, backgroundMusic, font)
		{
			int buttonWidth = 450;
			int buttonHeight = 75;

			_fontScale = 1.0f;

			_startButtonRect = new Rectangle(
				((int)EclipticaGame.ScreenSize.X - buttonWidth) / 2,
				((int)EclipticaGame.ScreenSize.Y - buttonHeight) / 2,
				buttonWidth,
				buttonHeight);

			_startButtonCenter = new Vector2(
				_startButtonRect.X + _startButtonRect.Width / 2,
				_startButtonRect.Y + _startButtonRect.Height / 2);
		}

		public override void Update(GameTime gameTime)
		{
			MouseState mouseState = Mouse.GetState();
			Point mousePosition = mouseState.Position;

			if (_startButtonRect.Contains(mousePosition))
			{

				if (mouseState.LeftButton == ButtonState.Pressed)
				{
					ScreenManager.ReplaceScreen(new GameScreen(
						Images.BackgroundBlue,
						Images.BackgroundStars,
						Fonts.FontGame,
						Sounds.MusicTheme));
				}

				_fontScale = 1.2f;
			} else
			{
				_fontScale = 1.0f;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Vector2 textSize = Font.MeasureString(_startButtonText);
			Vector2 origin = textSize / 2;

			spriteBatch.DrawString(
			Font,
			_startButtonText,
			_startButtonCenter,
			Color.White,
			0f,
			origin,
			_fontScale,
			SpriteEffects.None,
			0f);
		}
	}
}
