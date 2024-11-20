using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ecliptica.Games
{
	public class GameOverScreen : Screen
	{
		private string _gameOverText = "Game Over!";
		private string _restartButtonText = "Restart";
		private Rectangle _restartButtonRect;
		private Vector2 _restartButtonTextPosition;

		public GameOverScreen(Texture2D backgroundSolid, Texture2D backgroundStars, SpriteFont font, Song backgroundMusic)
			: base(backgroundSolid, backgroundStars, backgroundMusic, font)
		{
			_restartButtonRect = new Rectangle(300, 400, 200, 50);
			_restartButtonTextPosition = new Vector2(
				_restartButtonRect.X + (_restartButtonRect.Width / 2) - (font.MeasureString(_restartButtonText).X / 2),
				_restartButtonRect.Y + (_restartButtonRect.Height / 2) - (font.MeasureString(_restartButtonText).Y / 2)
			);
		}

		public override void Update(GameTime gameTime)
		{
			MouseState mouseState = Mouse.GetState();

			if (mouseState.LeftButton == ButtonState.Pressed && _restartButtonRect.Contains(mouseState.Position))
			{
				// Restart the game (replace with the starting screen)
				// ScreenManager.Instance.ReplaceScreen(new MainMenuScreen(...));
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{ 
			//// Draw restart button
			//spriteBatch.DrawString(Font, _restartButtonText, _restartButtonTextPosition, Color.Black);
		}
	}
}
