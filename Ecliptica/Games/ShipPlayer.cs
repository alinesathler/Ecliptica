using Ecliptica.Arts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ecliptica.Games
{
    public class ShipPlayer : Entity
	{
		#region Fields
		private readonly LifeShip _shipLife;

		private readonly float _normalSpeed = 1.0f;
		private readonly float _turboSpeed = 5.0f;
		#endregion

		#region Properties
		public static ShipPlayer Instance { get; private set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor to initialize the player's spaceship
		/// </summary>
		public ShipPlayer()
		{
			Instance = this;

			image = Images.ShipPlayer;
			Position = new Vector2(EclipticaGame.ScreenSize.X / 2, EclipticaGame.ScreenSize.Y * 9/10);
			MaxLife = 8;
			Life = MaxLife;

			_shipLife = new LifeShip(Images.LifeBarShip);

			CalculateBoundingBox();
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to update the player's spaceship
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			CalculateBoundingBox();
		}

		/// <summary>
		/// Method to draw the player's spaceship
		/// </summary>
		/// <param name="spriteBatch"></param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(image, Position, null, Color.White, Orientation, new Vector2(image.Width / 2, image.Height / 2), 1.0f, SpriteEffects.None, 0);

			_shipLife.Draw(spriteBatch, new Vector2(Position.X, Position.Y + image.Height / 2), Life);
		}

		/// <summary>
		/// Method to add life to the player's spaceship
		/// </summary>
		public void AddLife()
		{
			if (Life < MaxLife)
			{
				Life++;
			}
		}

		/// <summary>
		/// Method to Move the player's spaceship with the keyboard
		/// </summary>
		public void MoveSpaceShip()
		{
			KeyboardState _keyboardState = Keyboard.GetState();

			// Moving the spaceship with the keyboard
			if (_keyboardState.IsKeyDown(Keys.Left) && ShipPlayer.Instance.Position.X > ShipPlayer.Instance.Size.X)
			{
				// Move faster if the left control key is pressed
				if ((_keyboardState.IsKeyDown(Keys.LeftControl) || _keyboardState.IsKeyDown(Keys.RightControl)) && ShipPlayer.Instance.Position.X > ShipPlayer.Instance.Size.X)
				{
					ShipPlayer.Instance.Position = new Vector2(ShipPlayer.Instance.Position.X - _turboSpeed, ShipPlayer.Instance.Position.Y);
				}

				ShipPlayer.Instance.Position = new Vector2(ShipPlayer.Instance.Position.X - _normalSpeed, ShipPlayer.Instance.Position.Y);
			}
			if (_keyboardState.IsKeyDown(Keys.Right) && ShipPlayer.Instance.Position.X < EclipticaGame.ScreenSize.X - ShipPlayer.Instance.Size.X)
			{
				// Move faster if the left control key is pressed
				if ((_keyboardState.IsKeyDown(Keys.LeftControl) || _keyboardState.IsKeyDown(Keys.RightControl)) && ShipPlayer.Instance.Position.X < EclipticaGame.ScreenSize.X - ShipPlayer.Instance.Size.X)
				{
					ShipPlayer.Instance.Position = new Vector2(ShipPlayer.Instance.Position.X + _turboSpeed, ShipPlayer.Instance.Position.Y);
				}

				ShipPlayer.Instance.Position = new Vector2(ShipPlayer.Instance.Position.X + _normalSpeed, ShipPlayer.Instance.Position.Y);
			}
			if (_keyboardState.IsKeyDown(Keys.Up) && ShipPlayer.Instance.Position.Y > ShipPlayer.Instance.Size.Y)
			{
				// Move faster if the left control key is pressed
				if ((_keyboardState.IsKeyDown(Keys.LeftControl) || _keyboardState.IsKeyDown(Keys.RightControl)) && ShipPlayer.Instance.Position.Y > ShipPlayer.Instance.Size.Y)
				{
					ShipPlayer.Instance.Position = new Vector2(ShipPlayer.Instance.Position.X, ShipPlayer.Instance.Position.Y - _turboSpeed);
				}

				ShipPlayer.Instance.Position = new Vector2(ShipPlayer.Instance.Position.X, ShipPlayer.Instance.Position.Y - _normalSpeed);
			}
			if (_keyboardState.IsKeyDown(Keys.Down) && ShipPlayer.Instance.Position.Y < EclipticaGame.ScreenSize.Y * 9 / 10)
			{
				// Move faster if the left control key is pressed
				if ((_keyboardState.IsKeyDown(Keys.LeftControl) || _keyboardState.IsKeyDown(Keys.RightControl)) && ShipPlayer.Instance.Position.Y < EclipticaGame.ScreenSize.Y * 9 / 10)
				{
					ShipPlayer.Instance.Position = new Vector2(ShipPlayer.Instance.Position.X, ShipPlayer.Instance.Position.Y + _turboSpeed);
				}

				ShipPlayer.Instance.Position = new Vector2(ShipPlayer.Instance.Position.X, ShipPlayer.Instance.Position.Y + _normalSpeed);
			}
		}
		#endregion
	}
}
