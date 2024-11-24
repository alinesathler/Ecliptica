using Ecliptica.Arts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ecliptica.Games
{
    public class ShipPlayer : Entity
	{
		public static ShipPlayer Instance { get; private set; }
		private LifeShip _shipLife;

		private float _normalSpeed = 1.0f;
		private float _turboSpeed = 5.0f;

		public ShipPlayer()
		{
			Instance = this;

			image = Images.ShipPlayer;
			_position = new Vector2(EclipticaGame.ScreenSize.X / 2, EclipticaGame.ScreenSize.Y * 9/10);
			Radius = 20;
			MaxLife = 8;
			Life = MaxLife;

			_shipLife = new LifeShip(Images.LifeBarShip);

			CalculateBoundingBox();
		}

		public override void Update(GameTime gameTime)
		{
			CalculateBoundingBox();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(image, _position, null, Color.White, Orientation, new Vector2(image.Width / 2, image.Height / 2), 1.0f, SpriteEffects.None, 0);

			_shipLife.Draw(spriteBatch, new Vector2(_position.X, _position.Y + image.Height / 2), Life);
		}

		public void AddLife()
		{
			if (Life < MaxLife)
			{
				Life++;
			}
		}

		public void RemoveLife()
		{
			Life--;

			if (Life <= 0)
			{
				IsExpired = true;
				IsActive = false;
			}
		}

		public void MoveSpaceShip(GameTime gameTime)
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
	}
}
