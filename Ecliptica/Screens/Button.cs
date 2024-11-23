using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Ecliptica.Arts;
using Ecliptica.Games;
using System;

namespace Ecliptica.Screens
{
	public class Button
	{
		public string text;
		public Rectangle Bounds { get; private set; }
		private SpriteFont _font;
		private float _scale;
		private float _defaultScale;
		private float _hoverScale;
		public Color defaultColor;
		private Color _hoverColor;
		public Action OnClick;
		public Color? CurrentColor { get; set; } = null;


		public Button(string text, Rectangle bounds, SpriteFont font, float defaultScale, float hoverScale, Color defaultColor, Color hoverColor, Action onClick)
		{
			this.text = text;
			Bounds = bounds;
			_font = font;
			_defaultScale = defaultScale;
			_hoverScale = hoverScale;
			this.defaultColor = defaultColor;
			_hoverColor = hoverColor;
			OnClick = onClick;
		}

		public void Update(MouseState mouseState)
		{
			if (Bounds.Contains(mouseState.Position))
			{
				_scale = _hoverScale;

				if (mouseState.LeftButton == ButtonState.Pressed)
				{
					OnClick?.Invoke();
				}
			} else
			{
				_scale = _defaultScale;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Vector2 textSize = _font.MeasureString(text);
			Vector2 origin = textSize / 2;
			Vector2 position = new Vector2(Bounds.X + Bounds.Width / 2, Bounds.Y + Bounds.Height / 2);

			defaultColor = CurrentColor ?? defaultColor;

			spriteBatch.DrawString(
				_font,
				text,
				position,
				Bounds.Contains(Mouse.GetState().Position) ? _hoverColor : defaultColor,
				0f,
				origin,
				_scale,
				SpriteEffects.None,
				0f);
		}
	}
}