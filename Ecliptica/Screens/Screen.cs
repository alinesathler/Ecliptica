using Ecliptica.Arts;
using Ecliptica.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
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

		internal Vector2 Position;

		internal List<Button> Buttons = new();

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
			Texture2D texture = new (graphicsDevice, 1, 1);
			texture.SetData(new[] { color });
			return texture;
		}

		protected void AddButton(string text, Action onClick, Vector2? position = null)
		{
			Rectangle buttonRect;

			if (Buttons.Count == 0)
			{
				buttonRect = new Rectangle(
					(int)position.Value.X,
					(int)position.Value.Y,
					ButtonWidth,
					ButtonHeight
				);
			} else
			{
				buttonRect = new Rectangle(
				Buttons[0].Bounds.X, Buttons[0].Bounds.Y + (ButtonHeight + 10) * Buttons.Count,
				ButtonWidth,
				ButtonHeight);
			}

			Button button = new Button(
				text,
				buttonRect,
				Font,
				DefaultScale,
				HoverScale,
				DefaultColor,
				HoverColor,
				onClick
			);

			Buttons.Add(button);
		}
	}
}

