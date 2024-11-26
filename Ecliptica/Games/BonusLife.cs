using Ecliptica.Arts;
using Microsoft.Xna.Framework;
using System;

namespace Ecliptica.Games
{
	internal class BonusLife : Entity
	{
		#region Fields
		private readonly Random _ramdon;
		#endregion

		#region Properties
		public static BonusLife Instance { get; private set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor to initialize the bonus life
		/// </summary>
		public BonusLife()
		{
			Instance = this;

			_ramdon = new();

			image = Images.BonusLife;
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
		/// Method to update the bonus life
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{}
		#endregion
	}
}
