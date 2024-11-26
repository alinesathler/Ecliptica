using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Ecliptica.Arts;
using System;
using Ecliptica.InputHandler;

namespace Ecliptica.UI
{
    public class Button
	{
		#region Fields
		private string _text;
		private Rectangle _bounds;
		private readonly SpriteFont _font;
		private readonly float _defaultScale;
		private readonly float _hoverScale;
		private Color _defaultColor;
		private Color _hoverColor;

		private float _scale;
		#endregion

		#region Properties
		public string Text {
			get { return _text; }
			set { _text = value; }
		}
		public Rectangle Bounds { get { return _bounds; } }
		public Color? CurrentColor { get; set; } = null;
		#endregion

		#region Events
		public Action OnClick;
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor to initialize the button
		/// </summary>
		/// <param name="text"></param>
		/// <param name="bounds"></param>
		/// <param name="font"></param>
		/// <param name="defaultScale"></param>
		/// <param name="hoverScale"></param>
		/// <param name="defaultColor"></param>
		/// <param name="hoverColor"></param>
		/// <param name="onClick"></param>
		public Button(string text, Rectangle bounds, SpriteFont font, float defaultScale, float hoverScale, Color defaultColor, Color hoverColor, Action onClick)
		{
			_text = text;
			_bounds = bounds;
			_font = font;
			_defaultScale = defaultScale;
			_hoverScale = hoverScale;
			_defaultColor = defaultColor;
			_hoverColor = hoverColor;
			OnClick = onClick;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to update the button
		/// </summary>
		/// <param name="mouseState"></param>
		public void Update(MouseState mouseState)
        {
            if (_bounds.Contains(mouseState.Position))
            {
                _scale = _hoverScale;

                if (MouseHandler.IsLeftClick(_bounds))
                {
                    Sounds.PlaySound(Sounds.ButtonSound, 1.0f);
					OnClick?.Invoke();
                }
            }
            else
            {
                _scale = _defaultScale;
            }
        }

		/// <summary>
		/// Method to draw the button
		/// </summary>
		/// <param name="spriteBatch"></param>
		public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = _font.MeasureString(Text);
            Vector2 origin = textSize / 2;
            Vector2 position = new(_bounds.X + _bounds.Width / 2, _bounds.Y + _bounds.Height / 2);

			// Only slot buttons use the currentColor property, it has has 3 colors
			_defaultColor = CurrentColor ?? _defaultColor;

            spriteBatch.DrawString(
                _font,
                Text,
                position,
				_bounds.Contains(Mouse.GetState().Position) ? _hoverColor : _defaultColor,
                0f,
                origin,
                _scale,
                SpriteEffects.None,
                0f);
        }
		#endregion
	}
}