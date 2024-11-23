/* EclipticaGame.cs
* Final Project
* Revision History
* Aline Sathler Delfino, 2024.11.18: Created, content, entities, and levels added.
* Aline Sathler Delfino, 2024.11.19: Added animated sprites, sound effects, background music, and Menu Screen.
* Aline Sathler Delfino, 2024.11.20: Added Buttons, Game Screen, Game Over Screen, Win Screen, and Level Transition Screen.
* Aline Sathler Delfino, 2024.11.21: Added Scores Screen, Title Screen, and About Screen. Fireworks added to the Win Screen. Fixed scrolling background.
* Aline Sathler Delfino, 2024.11.22: Changed spaceship life, added life bar for asteroids, and fixed the game over screen, generate asteroids randomly instead of hardcoding, pause screen added, game score added.
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Ecliptica.Games;
using Ecliptica.Levels;
using Ecliptica.Arts;
using Ecliptica.Screens;
using Microsoft.Xna.Framework.Media;

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
		private Texture2D cursorTexture;
		private Vector2 cursorOffset;

		private float _shootCooldown = 0.25f;
		private float _timeSinceLastShot = 0f;
		private float _soundVolume = 0.25f;
		public bool isPaused = false;

		private Platform _platform = Platform.Windows;

		public static string PlayerName { get; set; } = "";

		public EclipticaGame()
        {
            Instance = this;

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            //Screen size
            SetWindowSize();
        }

		public EclipticaGame(Platform platform)
		{
			Instance = this;

			_platform = platform;

			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			if (_platform == Platform.Windows || _platform == Platform.Linux)
			{
				SetWindowSize();
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
			Images.Load(Content, GraphicsDevice);
			Sounds.Load(Content);
			Fonts.Load(Content);

			//Change cursor
			if (_platform == Platform.Windows || _platform == Platform.Linux)
			{
				cursorTexture = Images.Cursor;
				cursorOffset = new Vector2(cursorTexture.Width / 2, 0);
			}

			ScreenManager.ReplaceScreen(new TitleScreen());

			Fonts.Load(Content);
		}       

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			//If game is paused, update the pause screen only
			if (PauseScreen.Instance !=null && PauseScreen.Instance.isPaused)
			{
				PauseScreen.Instance.Update(gameTime);

				MediaPlayer.Volume = 0.0f;

				return;
			}

			MediaPlayer.Volume = 1.0f;

			if (_platform == Platform.Android || _platform == Platform.iOS)
			{
				while (TouchPanel.IsGestureAvailable)
				{
					GestureSample gesture = TouchPanel.ReadGesture();
					switch (gesture.GestureType)
					{
						case GestureType.Tap:
							if(ShipPlayer.Instance != null)
							{
								LevelManager.FireProjectile();

								_timeSinceLastShot = 0f;
							}
							break;
						case GestureType.DoubleTap:
							break;
						case GestureType.Flick:
							break;
						case GestureType.FreeDrag:
							if (ShipPlayer.Instance != null)
								ShipPlayer.Instance.Position = gesture.Position;
							break;
						default:
							break;
					}
				}
			} else if (_platform == Platform.Windows)
			{
				if (ShipPlayer.Instance != null)
				{
					ShipPlayer.Instance.MoveSpaceShip(gameTime);
				}

				_keyboardState = Keyboard.GetState();
				
				// Spaceship shooting
				_timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

				if (_keyboardState.IsKeyDown(Keys.Space) && _timeSinceLastShot >= _shootCooldown && ShipPlayer.Instance != null)
				{
					LevelManager.FireProjectile();
					_timeSinceLastShot = 0f;
				}

				ScreenManager.Update(gameTime);
				Background.Update(gameTime);
				LevelTransition.UpdateTransition(gameTime);

				base.Update(gameTime);
			}
		}

		protected override void Draw(GameTime gameTime)
		{ 
            GraphicsDevice.Clear(Color.Transparent);

			// TODO: Add your drawing code here
			_spriteBatch.Begin();

			//If game is paused, draw the pause screen only
			if (PauseScreen.Instance != null && PauseScreen.Instance.isPaused)
			{
				PauseScreen.Instance.Draw(_spriteBatch);

				//Cursor update
				MouseState mouseState1 = Mouse.GetState();
				Vector2 cursorPosition1 = new Vector2(mouseState1.X, mouseState1.Y) - cursorOffset;
				_spriteBatch.Draw(cursorTexture, cursorPosition1, Color.White);

				_spriteBatch.End();

				return;
			}

			if (SaveScreen.Instance != null && SaveScreen.Instance.IsActive)
			{
				SaveScreen.Instance.Draw(_spriteBatch, GraphicsDevice);
			}

			Background.Draw(_spriteBatch, GraphicsDevice);

			ScreenManager.Draw(_spriteBatch);


			//Cursor update
			MouseState mouseState = Mouse.GetState();
			Vector2 cursorPosition = new Vector2(mouseState.X, mouseState.Y) - cursorOffset;
			_spriteBatch.Draw(cursorTexture, cursorPosition, Color.White);

			_spriteBatch.End();

			base.Draw(gameTime);
        }
	}
}
