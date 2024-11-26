using Ecliptica.Arts;
using Microsoft.Xna.Framework;
using System;

namespace Ecliptica.Games
{
	internal class BonusTime : Entity
	{
		#region Fields
		private readonly Random _ramdon;
		#endregion

		#region Properties
		public static BonusTime Instance { get; private set; }
		#endregion

		#region Constructors
		public BonusTime()
		{
			Instance = this;

			_ramdon = new();

			image = Images.BonusTime;
			Position = _ramdon.NextFloat(0.0f, 0.90f) * EclipticaGame.ScreenSize;
			Velocity = Vector2.Zero;
			MaxLife = 1;
			Life = MaxLife;
			Scale = 0.5f;

			SoundPicked = Sounds.BonusSound;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to update the bonus time
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{}
		#endregion
	}
}
