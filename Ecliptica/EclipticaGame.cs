using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Ecliptica.Games;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Collections.Generic;
using System.Linq;
using Ecliptica.Levels;

namespace Ecliptica
{
    public class EclipticaGame : Microsoft.Xna.Framework.Game
    {
        public static EclipticaGame Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static GameTime GameTime { get; private set; }
        public static GraphicsDevice GameGraphicDevice { get { return Instance.GraphicsDevice; } }


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
		private static KeyboardState _keyboardState;

		//private Vector2 _velocity = new Vector2(1, 1);
        private ShipPlayer _shipPlayer;
		private float _normalSpeed = 1.0f;
		private float _turboSpeed = 5.0f;

		private float _shootCooldown = 0.25f;
		private float _timeSinceLastShot = 0f;
		private float _soundVolume = 0.25f;

		//private Asteroid _asteroidRedSmall;
		//private Asteroid _asteroidRedMedium;

		private Platform _platform = Platform.Windows;

		private SpriteFont _myFont;

        public EclipticaGame()
        {
            Instance = this;

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //Screen size
            SetWindowSize();
        }

		public EclipticaGame(Platform platform)
		{
			Instance = this;

			_platform = platform;

			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			if (_platform == Platform.Android || _platform == Platform.iOS)
			{
				IsMouseVisible = false;
			} else
			{
				SetWindowSize();

				IsMouseVisible = true;
			}
		}

		private void SetWindowSize()
        {
            int screenWidth = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.9);
            int screenHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.9);

            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
		}

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            if(_platform == Platform.Android || _platform == Platform.iOS)
			{
				//Enable touch input
                TouchPanel.EnabledGestures = GestureType.Tap | GestureType.DoubleTap | GestureType.Flick | GestureType.FreeDrag;
			}

			base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Art.Load(Content);
			Sound.Load(Content);
			Background.Load(Content);
			LevelManager.LoadLevels();
			Fonts.Load(Content);

			_shipPlayer = new ShipPlayer();
			_myFont = Fonts.MyFont;
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			if (_platform == Platform.Android || _platform == Platform.iOS)
			{
				while (TouchPanel.IsGestureAvailable)
				{
					GestureSample gesture = TouchPanel.ReadGesture();
					switch (gesture.GestureType)
					{
						case GestureType.Tap:
							LevelManager.FireProjectile(_shipPlayer);
							_timeSinceLastShot = 0f;
							break;
						case GestureType.DoubleTap:
							break;
						case GestureType.Flick:
							break;
						case GestureType.FreeDrag:
							_shipPlayer.Position = gesture.Position;
							break;
						default:
							break;
					}
				}
			} else if (_platform == Platform.Windows)
			{
				_keyboardState = Keyboard.GetState();
				_timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

				// Moving the spaceship with the keyboard
				if (_keyboardState.IsKeyDown(Keys.Left) && _shipPlayer.Position.X > _shipPlayer.GetImageSize()[0])
				{
					// Move faster if the left control key is pressed
					if ((_keyboardState.IsKeyDown(Keys.LeftControl) || _keyboardState.IsKeyDown(Keys.RightControl)) && _shipPlayer.Position.X > _shipPlayer.GetImageSize()[0])
					{
						_shipPlayer.Position = new Vector2(_shipPlayer.Position.X - _turboSpeed, _shipPlayer.Position.Y);
					}

					_shipPlayer.Position = new Vector2(_shipPlayer.Position.X - _normalSpeed, _shipPlayer.Position.Y);
				}
				if (_keyboardState.IsKeyDown(Keys.Right) && _shipPlayer.Position.X < ScreenSize.X - _shipPlayer.GetImageSize()[0])
				{
					// Move faster if the left control key is pressed
					if ((_keyboardState.IsKeyDown(Keys.LeftControl) || _keyboardState.IsKeyDown(Keys.RightControl)) && _shipPlayer.Position.X < ScreenSize.X - _shipPlayer.GetImageSize()[0])
					{
						_shipPlayer.Position = new Vector2(_shipPlayer.Position.X + _turboSpeed, _shipPlayer.Position.Y);
					}

					_shipPlayer.Position = new Vector2(_shipPlayer.Position.X + _normalSpeed, _shipPlayer.Position.Y);
				}
				if (_keyboardState.IsKeyDown(Keys.Up) && _shipPlayer.Position.Y > _shipPlayer.GetImageSize()[1])
				{
					// Move faster if the left control key is pressed
					if ((_keyboardState.IsKeyDown(Keys.LeftControl) || _keyboardState.IsKeyDown(Keys.RightControl)) && _shipPlayer.Position.Y > _shipPlayer.GetImageSize()[1])
					{
						_shipPlayer.Position = new Vector2(_shipPlayer.Position.X, _shipPlayer.Position.Y - _turboSpeed);
					}

					_shipPlayer.Position = new Vector2(_shipPlayer.Position.X, _shipPlayer.Position.Y - _normalSpeed);
				}
				if (_keyboardState.IsKeyDown(Keys.Down) && _shipPlayer.Position.Y < ScreenSize.Y - _shipPlayer.GetImageSize()[1])
				{
					// Move faster if the left control key is pressed
					if ((_keyboardState.IsKeyDown(Keys.LeftControl) || _keyboardState.IsKeyDown(Keys.RightControl)) && _shipPlayer.Position.Y < ScreenSize.Y - _shipPlayer.GetImageSize()[1])
					{
						_shipPlayer.Position = new Vector2(_shipPlayer.Position.X, _shipPlayer.Position.Y + _turboSpeed);
					}

					_shipPlayer.Position = new Vector2(_shipPlayer.Position.X, _shipPlayer.Position.Y + _normalSpeed);
				}

				// Spaceship shooting
				if (_keyboardState.IsKeyDown(Keys.Space) && _timeSinceLastShot >= _shootCooldown)
				{
					LevelManager.FireProjectile(_shipPlayer);
					_timeSinceLastShot = 0f;
				}

				// Check if the level is completed
				if (!LevelTransition.IsTransitioning && LevelManager.CurrentLevel.EnemyCount == EntityManager.GetNumberOfEnemiesDestroyedLevel())
				{
					LevelTransition.StartTransition();
				}

				LevelManager.UpdateCurrentLevel(gameTime);

				_shipPlayer.Update(gameTime);
				Background.Update(gameTime);
				LevelTransition.UpdateTransition(gameTime);

				base.Update(gameTime);
			}
		}

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
			Background.Draw(_spriteBatch, GraphicsDevice);
			LevelManager.DrawCurrentLevel(_spriteBatch);
			LevelTransition.DrawTransition(_spriteBatch);
			_shipPlayer.Draw(_spriteBatch);
			_spriteBatch.End();

            base.Draw(gameTime);
        }

        private void AddGiftBoxes()
		{
			//_redGiftBox = new MovingEntity(Art.RedGiftBox, Vector2.Zero, _velocity);

			//EntityManager.Add(_asteroidRedSmall);
		}
	}
}
