using Ecliptica.Arts;
using Ecliptica.InputHandler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Diagnostics;

namespace Ecliptica.Screens
{
    internal class TitleScreen : Screen
    {
        #region Fields
        private string _message;
        private double _messageTime;
        private Rectangle _nameInputBox;
        private bool _isNameInputActive;
        private readonly IKeyboardHelper _keyboardHelper;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to initialize the title screen
        /// </summary>
        public TitleScreen()
        {
            if (EclipticaGame.Instance.platform == Games.Platform.Android)
            {
                _keyboardHelper = ServiceLocator.Get<IKeyboardHelper>();
            }

            _message = "";
            _messageTime = 0.0;
            _isNameInputActive = false;

            Music = Sounds.TitleScreen;
            BackgroundSolid = Images.BackgroundTitle;
            BackgroundStars = Images.BackgroundStars1;
            Font = Fonts.FontGame;
            DefaultScale = 1.0f;
            HoverScale = 1.2f;
            DefaultColor = Color.White;
            HoverColor = Color.Yellow;
            ButtonWidth = 450;
            ButtonHeight = 50;

            // Define the name input box
            if (EclipticaGame.Instance.platform == Games.Platform.Windows || EclipticaGame.Instance.platform == Games.Platform.Linux)
            {
                _nameInputBox = new Rectangle(
                    (int)((EclipticaGame.ScreenSize.X - 600) / 2),
                    (int)(EclipticaGame.ScreenSize.Y / 2),
                    600, 50
                );
            } else
            {
                _nameInputBox = new Rectangle(
                        (int)((EclipticaGame.ScreenSize.X - 800) / 2),
                        (int)(EclipticaGame.ScreenSize.Y / 2),
                        800, 75
                   );
            }

            // Buttons
            AddButton("Start Game", () => CheckName(), new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2,
            ((int)EclipticaGame.ScreenSize.Y - 2 * ButtonHeight - 30)));
            AddButton("Exit", () => EclipticaGame.Instance.Exit());
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to handle key presses on Windows
        /// </summary>
        /// <param name="key"></param>
        private static void HandleKeyPress(Keys key)
        {
            if (key == Keys.Back && EclipticaGame.PlayerName.Length > 0)
            {
                EclipticaGame.PlayerName = EclipticaGame.PlayerName[..^1]; // Remove the last character
            } else if (key == Keys.Space)
            {
                EclipticaGame.PlayerName += ' ';
            } else if (key.ToString().Length == 1) // Printable character
            {
                if (EclipticaGame.PlayerName.Length < 12) // Limit name length
                {
                    EclipticaGame.PlayerName += key.ToString();
                }
            }
        }

        /// <summary>
        /// Method to validate the player name
        /// </summary>
        private void CheckName()
        {
            string name = EclipticaGame.PlayerName.Trim();

            if (string.IsNullOrEmpty(name) || name.Length > 12)
            {
                _message = $"Please enter a name between 1 and 12 caracteres";
                _messageTime = 2.0;
            } else
            {
                ScreenManager.ReplaceScreen(new MenuScreen());
            }
        }

        /// <summary>
        /// Method to unload the title screen
        /// </summary>
        public void Unload()
        {
            if (EclipticaGame.Instance.platform == Games.Platform.Android)
            {
                _keyboardHelper?.HideKeyboard();
            }
        }

        /// <summary>
        /// Method to update the title screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            TouchCollection touchState = TouchPanel.GetState();
            // Show keyboard on Android
            foreach (var touch in touchState)
            {
                if ((touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved) && _nameInputBox.Contains(touch.Position.ToPoint()))
                {
                    _isNameInputActive = true;

                    // Show keyboard on Android
                    _keyboardHelper?.ShowKeyboard();
                }
            }

            // Hide keyboard on Android
            if (touchState.Count > 0 && !_nameInputBox.Contains(touchState[0].Position.ToPoint()))
            {
                _isNameInputActive = false;

                // Hide keyboard on Android
                _keyboardHelper?.HideKeyboard();
            }

            // Handle keyboard input
            KeyboardHandler.Update();
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (KeyboardHandler.IsKeyPressed(key))
                {
                    HandleKeyPress(key);
                }
            }

            if (_nameInputBox.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _isNameInputActive = true;
            } else if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _isNameInputActive = false;
            }

            // Start the game when enter is pressed
            if (KeyboardHandler.IsKeyPressed(Keys.Enter))
            {
                Buttons[0].OnClick();
            }

            // Update time for the message
            if (!string.IsNullOrEmpty(_message))
            {
                _messageTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (_messageTime <= 0) _message = null;
            }
        }

        /// <summary>
        /// Method to draw the title screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Draw logo
            spriteBatch.Draw(Images.Ecliptica, new Rectangle((int)EclipticaGame.ScreenSize.X / 4, 0, (int)EclipticaGame.ScreenSize.X / 2, (int)EclipticaGame.ScreenSize.Y / 5), Color.White);

            // Draw the prompt
            string prompt = "Enter your name:";
            Vector2 promptPosition = new(
                (EclipticaGame.ScreenSize.X - Font.MeasureString(prompt).X) / 2,
                EclipticaGame.ScreenSize.Y / 2 - 70
            );
            spriteBatch.DrawString(Font, prompt, promptPosition, DefaultColor);

            // Draw the name input box
            int borderWidth = 3;
            Color borderColor = _isNameInputActive ? HoverColor : DefaultColor;
            spriteBatch.Draw(Images.Pixel, new Rectangle(_nameInputBox.Left, _nameInputBox.Top, _nameInputBox.Width, borderWidth), borderColor);
            spriteBatch.Draw(Images.Pixel, new Rectangle(_nameInputBox.Left, _nameInputBox.Bottom - borderWidth, _nameInputBox.Width, borderWidth), borderColor);
            spriteBatch.Draw(Images.Pixel, new Rectangle(_nameInputBox.Left, _nameInputBox.Top, borderWidth, _nameInputBox.Height), borderColor);
            spriteBatch.Draw(Images.Pixel, new Rectangle(_nameInputBox.Right - borderWidth, _nameInputBox.Top, borderWidth, _nameInputBox.Height), borderColor);
            Vector2 namePosition = new(
                _nameInputBox.Center.X - Font.MeasureString(EclipticaGame.PlayerName).X / 2,
                _nameInputBox.Center.Y - Font.MeasureString(EclipticaGame.PlayerName).Y / 2
            );
            spriteBatch.DrawString(Font, EclipticaGame.PlayerName, namePosition, DefaultColor);

            // Draw the save message if it exists
            if (!string.IsNullOrEmpty(_message))
            {
                Vector2 messageSize = Fonts.FontGameSmall.MeasureString(_message);
                Vector2 messagePosition = new(
                    (EclipticaGame.ScreenSize.X - messageSize.X) / 2,
                    Buttons[0].Bounds.Y - messageSize.Y - 15
                );
                spriteBatch.DrawString(Fonts.FontGameSmall, _message, messagePosition, HoverColor);
            }
        }
        #endregion
    }
}
