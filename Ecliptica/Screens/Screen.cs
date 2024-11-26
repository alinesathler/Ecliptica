using Ecliptica.Arts;
using Ecliptica.InputHandler;
using Ecliptica.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Ecliptica.Screens
{
    public abstract class Screen
	{
		#region Fields
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

		internal List<Button> Buttons = new();
		#endregion

		#region Methods
		/// <summary>
		/// Method to load the screen
		/// </summary>
		/// <param name="isLoadMusic"></param>
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

		/// <summary>
		/// Method to update the screen
		/// </summary>
		/// <param name="gameTime"></param>
		public virtual void Update(GameTime gameTime)
		{
			MouseState mouseState = Mouse.GetState();

			MouseHandler.Update();

			foreach (var button in Buttons)
			{
				button.Update(mouseState);
			}
		}

		/// <summary>
		/// Method to draw the screen
		/// </summary>
		/// <param name="spriteBatch"></param>
		public virtual void Draw(SpriteBatch spriteBatch)
		{
			foreach (var button in Buttons)
			{
				button.Draw(spriteBatch);
			}
		}

		/// <summary>
		/// Method to create a blank texture
		/// </summary>
		/// <param name="graphicsDevice"></param>
		/// <param name="color"></param>
		/// <returns>A Texture2D in the size of the screen in the color selected</returns>
		public static Texture2D CreateBlankTexture(GraphicsDevice graphicsDevice, Color color)
		{
			Texture2D texture = new (graphicsDevice, 1, 1);
			texture.SetData(new[] { color });
			return texture;
		}

		/// <summary>
		/// Method to add a button to the list of buttons
		/// </summary>
		/// <param name="text"></param>
		/// <param name="onClick"></param>
		/// <param name="position"></param>
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

			Button button = new(
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
		#endregion
	}
}

