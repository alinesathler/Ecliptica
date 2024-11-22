using Ecliptica.Games;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Ecliptica.Levels;
using Microsoft.Xna.Framework.Input;
using System;

namespace Ecliptica.Screens
{
	internal class GameScreen : Screen
	{
		public static GameScreen Instance { get; private set; }

		private ShipPlayer _shipPlayer;

		private float _normalSpeed = 1.0f;
		private float _turboSpeed = 5.0f;

		public GameScreen()
		{
			Instance = this;

			LevelManager.Clear();

			LevelManager.LoadLevels();

			_shipPlayer = new ShipPlayer();
		}

		public override void Update(GameTime gameTime)
		{
			// Check if the level is completed
			if (!LevelTransition.IsTransitioning && LevelManager.CurrentLevel?.IsLevelComplete() == true)
			{
				if (!LevelManager.isLastLevel())
				{
					LevelTransition.StartTransition();
				} else
				{
					ScreenManager.ReplaceScreen(new WinScreen());
				}
			}

			LevelManager.UpdateCurrentLevel(gameTime);
			_shipPlayer.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			LevelManager.DrawCurrentLevel(spriteBatch);
			LevelTransition.DrawTransition(spriteBatch);
			_shipPlayer.Draw(spriteBatch);
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
			if (_keyboardState.IsKeyDown(Keys.Down) && ShipPlayer.Instance.Position.Y < EclipticaGame.ScreenSize.Y - ShipPlayer.Instance.Size.Y)
			{
				// Move faster if the left control key is pressed
				if ((_keyboardState.IsKeyDown(Keys.LeftControl) || _keyboardState.IsKeyDown(Keys.RightControl)) && ShipPlayer.Instance.Position.Y < EclipticaGame.ScreenSize.Y - ShipPlayer.Instance.Size.Y)
				{
					ShipPlayer.Instance.Position = new Vector2(ShipPlayer.Instance.Position.X, ShipPlayer.Instance.Position.Y + _turboSpeed);
				}

				ShipPlayer.Instance.Position = new Vector2(ShipPlayer.Instance.Position.X, ShipPlayer.Instance.Position.Y + _normalSpeed);
			}
		}
	}
}
