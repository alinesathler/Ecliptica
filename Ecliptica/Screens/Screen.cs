using Ecliptica.Arts;
using Ecliptica.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Ecliptica.Games
{
	public abstract class Screen
	{
		internal Song Music;
		internal SpriteFont Font;
		internal Texture2D BackgroundSolid;
		internal Texture2D BackgroundStars;
		internal float DefaultScale;
		internal float HoverScale;
		internal Color DefaultColor;
		internal Color HoverColor;
		internal int ButtonWidth;
		internal int ButtonHeight;

		internal List<Button> Buttons = new List<Button>();

		public void Load(bool isLoadMusic)
		{
			// Choose to load the music
			if (isLoadMusic)
			{
				if (Music != null)
				{
					Sounds.StopMusic();
					Sounds.PlayMusic(Music);
				}
			}

			if (BackgroundSolid != null && BackgroundStars != null)
			{
				Background.Load(BackgroundSolid, BackgroundStars);
			}
		}

		public virtual void Update(GameTime gameTime)
		{
			MouseState mouseState = Mouse.GetState();

			foreach (var button in Buttons)
			{
				button.Update(mouseState);
			}
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			foreach (var button in Buttons)
			{
				button.Draw(spriteBatch);
			}
		}

		public static Texture2D CreateBlankTexture(GraphicsDevice graphicsDevice, Color color)
		{
			Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
			texture.SetData(new[] { color });
			return texture;
		}
	}
}

