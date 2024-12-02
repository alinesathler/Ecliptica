/* EclipticaGame.cs
* Final Project
* Revision History
* Aline Sathler Delfino, 2024.11.18: Created, content, entities, and levels added.
* Aline Sathler Delfino, 2024.11.19: Added animated sprites, sound effects, background music, and Menu Screen.
* Aline Sathler Delfino, 2024.11.20: Added Buttons, Game Screen, Game Over Screen, Win Screen, and Level Transition Screen.
* Aline Sathler Delfino, 2024.11.21: Added Scores Screen, Title Screen, and About Screen. Fireworks added to the Win Screen. Fixed scrolling background.
* Aline Sathler Delfino, 2024.11.22: Changed spaceship life, added life bar for asteroids, and fixed the game over screen, generate asteroids randomly instead of hardcoding, pause screen added, game score added.
* Aline Sathler Delfino, 2024.11.23: Added Save Screen to save the game state, write to file, refactroing screen class to not hadcording buttons.
* Aline Satheer Delfino, 2024.11.24: Added Load Screen to load the game state, refactored game screen to use the load game method, added the ability to save the game state, refactored game screen to use the save game method.
* Aline Sathler Delfino, 2024.11.24: Added the ability to read and write high scores to a file, added the Scores Screen to display the high scores, changed the game logic to finish the level based on time.
* Aline Sathler Delfino, 2024.11.24: Levels are generated randomly with increased speed and enemy level, added mouse handler, added bonus life and time, levels design.
* Aline Sathler Delfino, 2024.11.25: Added About and Tutorial screens.
* Aline Sathler Delfino, 2024.11.25: Installer, taskbar title and debug.
* Aline Sathler Delfino, 2024.12.02: Android vbersion and debuging.	
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
using Ecliptica.UI;            

namespace Ecliptica
{
    public class EclipticaGame : Game
    {
		#region Fields
		private readonly GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private static KeyboardState _keyboardState;
		private Texture2D _cursorTexture;
		private Vector2 _cursorOffset;

		private readonly float _shootCooldown = 0.25f;
		private float _timeSinceLastShot = 0f;
		public bool isPaused = false;

		public readonly Platform platform = Platform.Windows;
		#endregion

		#region Properties
		public static EclipticaGame Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static GameTime GameTime { get; private set; }
        public static GraphicsDevice GameGraphicDevice { get { return Instance.GraphicsDevice; } }

		public static string PlayerName { get; set; } = "";
		#endregion

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

            this.platform = platform;

			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			if (platform == Platform.Windows || platform == Platform.Linux)
			{
				SetWindowSize();
			}
		}

		/// <summary>
		/// Method to set the window size
		/// </summary>
		private void SetWindowSize()
        {
            int screenWidth = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width);
            int screenHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.90);

            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
		}

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            if(platform == Platform.Android || platform == Platform.iOS)
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
			if (platform == Platform.Windows || platform == Platform.Linux)
			{
				_cursorTexture = Images.Cursor;
				_cursorOffset = new Vector2(_cursorTexture.Width / 2, 0);
			}

			ScreenManager.ReplaceScreen(new TitleScreen());
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

			if (platform == Platform.Android || platform == Platform.iOS)
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
			} else if (platform == Platform.Windows)
			{
				if (ShipPlayer.Instance != null)
				{
					ShipPlayer.Instance.MoveSpaceShip();
				}

				_keyboardState = Keyboard.GetState();

                // Spaceship shooting
                _timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

				if (_keyboardState.IsKeyDown(Keys.Space) && _timeSinceLastShot >= _shootCooldown && ShipPlayer.Instance != null)
				{
					LevelManager.FireProjectile();
					_timeSinceLastShot = 0f;
				}
			}

            ScreenManager.Update(gameTime);
            Background.Update(gameTime);
            LevelTransition.UpdateTransition(gameTime);

            base.Update(gameTime);
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


				if (platform == Platform.Windows || platform == Platform.Linux)
				{
					//Cursor update
					MouseState mouseState1 = Mouse.GetState();
					Vector2 cursorPosition1 = new Vector2(mouseState1.X, mouseState1.Y) - _cursorOffset;
					_spriteBatch.Draw(_cursorTexture, cursorPosition1, Color.White);
				}

				_spriteBatch.End();

				return;
			}

			if (SaveScreen.Instance != null && SaveScreen.Instance.IsActive)
			{
				SaveScreen.Instance.Draw(_spriteBatch);
			}

			Background.Draw(_spriteBatch, GraphicsDevice);
			ScreenManager.Draw(_spriteBatch);

			if(platform == Platform.Windows || platform == Platform.Linux)
			{
				//Cursor update
				MouseState mouseState = Mouse.GetState();
				Vector2 cursorPosition = new Vector2(mouseState.X, mouseState.Y) - _cursorOffset;
				_spriteBatch.Draw(_cursorTexture, cursorPosition, Color.White);
			}

			_spriteBatch.End();

			base.Draw(gameTime);
        }
	}
}
