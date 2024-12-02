using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;


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

            // Bigger lifebar for mobile platforms
            if (EclipticaGame.Instance.platform == Platform.Android || EclipticaGame.Instance.platform == Platform.iOS)
            {
				Vector2 _position = new ((int)position.X, (int)position.Y + (int)ShipPlayer.Instance.Size.Y / 2 + 15);	
                spriteBatch.Draw(lifebar, _position, null, Color.White, 0.0f, new Vector2(lifebar.Width / 2, lifebar.Height / 2), 2.0f, SpriteEffects.None, 0);
            }
        }
        #endregion
	}
}
