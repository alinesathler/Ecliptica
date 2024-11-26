using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecliptica.UI
{
	public class TextManipulation
	{
		#region Methods
		/// <summary>
		/// Method to wrap text
		/// </summary>
		/// <param name="font"></param>
		/// <param name="text"></param>
		/// <param name="maxLineWidth"></param>
		/// <returns></returns>
		public static string WrapText(SpriteFont font, string text, float maxLineWidth)
		{
			string[] words = text.Split(' ');
			StringBuilder wrappedText = new();
			float lineWidth = 0f;

			foreach (var word in words)
			{
				Vector2 wordSize = font.MeasureString(word + " ");
				if (lineWidth + wordSize.X > maxLineWidth)
				{
					wrappedText.AppendLine();
					lineWidth = 0f;
				}

				wrappedText.Append(word + " ");
				lineWidth += wordSize.X;
			}

			return wrappedText.ToString();
		}
		#endregion
	}
}
