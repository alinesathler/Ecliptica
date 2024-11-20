using Ecliptica.Arts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecliptica.Games
{
    public class ShipPlayer : Entity
	{
		public static ShipPlayer Instance { get; private set; }
		static private int _maxLife = 4;
		private Life _shipLife;

		public ShipPlayer()
		{
			Instance = this;

			image = Images.ShipPlayer;
			_position = new Vector2(EclipticaGame.ScreenSize.X / 2, EclipticaGame.ScreenSize.Y - image.Height - 5);
			Radius = 20;
			Life = 4;

			_shipLife = new Life(Images.ShipLife, 1, 4);

			CalculateBoundingBox();
		}

		//public void Update(GameTime gameTime, SpriteBatch spriteBatch)
		//{
		//	CalculateBoundingBox();

			
		//}

		public override void Update(GameTime gameTime)
		{
			CalculateBoundingBox();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(image, _position, null, Color.White, Orientation, new Vector2(image.Width / 2, image.Height / 2), 1.0f, SpriteEffects.None, 0);

			_shipLife.Draw(spriteBatch, _position, _maxLife, Life);
		}

		public void AddLife()
		{
			if (Life < _maxLife)
			{
				Life++;
			}
		}

		public void RemoveLife()
		{
			Life--;
		}

		public int GetMaxLife()
		{
			return _maxLife;
		}
	}
}
