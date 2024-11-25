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
	internal class BonusTime : Entity
	{
		public static BonusTime Instance { get; private set; }

		Random ramdon;
		public BonusTime()
		{
			Instance = this;

			ramdon = new();

			image = Images.BonusTime;
			Position = ramdon.NextFloat(0.0f, 0.90f) * EclipticaGame.ScreenSize;
			Velocity = Vector2.Zero;
			MaxLife = 1;
			Life = MaxLife;
			Scale = 0.5f;
		}

		public override void Update(GameTime gameTime)
		{
		}
	}
}
