using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Ecliptica.Games
{
	internal class LifeShip(List<Texture2D> lifeTextureList)
	{
		#region Fields
		private readonly List<Texture2D> _lifeTextureList = lifeTextureList;
		#endregion

		#region Methods
		/// <summary>
		/// Method to draw the ship life
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="position"></param>
		/// <param name="currentLife"></param>
		public void Draw(SpriteBatch spriteBatch, Vector2 position, int currentLife)
		{
			if (currentLife < 0)
			{
				currentLife = 0;
			}

			if (currentLife >= _lifeTextureList.Count)
			{
				currentLife = _lifeTextureList.Count - 1;
			}

			Texture2D lifebar = _lifeTextureList[currentLife];

			spriteBatch.Draw(lifebar, new Vector2(position.X - lifebar.Width / 2, position.Y), null, Color.White);
		}
		#endregion
	}
}
