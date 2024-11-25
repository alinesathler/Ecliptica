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
	internal class BonusLife : Entity
	{
		Random ramdon;
		public static BonusLife Instance { get; private set; }
		public BonusLife()
		{
			Instance = this;

			ramdon = new();

			image = Images.BonusLife;
			Position = ramdon.NextFloat(0.0f, 0.90f) * EclipticaGame.ScreenSize;
			Velocity = Vector2.Zero;
			MaxLife = 1;
			Life = MaxLife;
			Scale = 0.5f;
		}

		public override void Update(GameTime gameTime)
		{
			//// Check if the entity is out of the screen
			//if (Position.Y > EclipticaGame.ScreenSize.Y)
			//{
			//	IsExpired = true;
			//}
		}

		//public override void Draw(SpriteBatch spriteBatch)
		//{
		//	spriteBatch.Draw(image, Position, null, color, 0, Vector2.Zero, Scale, SpriteEffects.None, Layer);
		//}
	}
}
