using Ecliptica.Arts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Ecliptica.Games
{
	public abstract class Screen
	{
		protected Song Music;
		protected SpriteFont Font;

		/// <summary>
		/// Contructor for a screen
		/// </summary>
		/// <param name="backgroundTexture"></param>
		/// <param name="backgroundMusic"></param>
		/// <param name="font"></param>
		protected Screen(Texture2D backgroundSolid, Texture2D backgroundStars, Song backgroundMusic, SpriteFont font)
		{
			Background.Load(backgroundSolid, backgroundStars);

			Music = backgroundMusic;
			Font = font;
		}

		/// <summary>
		/// Method to play the music
		/// </summary>
		public void PlayMusic()
		{
			if (Music != null)
			{
				Sounds.StopMusic();
				Sounds.PlayMusic(Music);
			}
		}

		/// <summary>
		/// Abstract method to update the screen
		/// </summary>
		/// <param name="gameTime"></param>
		public abstract void Update(GameTime gameTime);

		/// <summary>
		/// Abstract method to draw the screen
		/// </summary>
		/// <param name="spriteBatch"></param>
		public abstract void Draw(SpriteBatch spriteBatch);
	}
}
