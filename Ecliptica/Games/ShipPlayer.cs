using Microsoft.Xna.Framework;
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
		static private int maxLife = 4;

		public ShipPlayer()
		{
			Instance = this;

			image = Art.ShipPlayer;
			_position = new Vector2(EclipticaGame.ScreenSize.X / 2, EclipticaGame.ScreenSize.Y - image.Height - 5);
			Radius = 20;
			Life = 4;

			CalculateBoundingBox();
		}

		public override void Update(GameTime gameTime)
		{
			CalculateBoundingBox();
		}

		public int[] GetImageSize()
		{
			return new int[] { image.Width, image.Height };
		}
	}
}
