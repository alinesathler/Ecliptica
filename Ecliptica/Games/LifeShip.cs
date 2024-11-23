using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecliptica.Games
{
	internal class LifeShip
	{
		private List<Texture2D> _lifeTextureList;

		public LifeShip(List<Texture2D> lifeTextureList)
		{
			_lifeTextureList = lifeTextureList;
		}

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
	}
}
