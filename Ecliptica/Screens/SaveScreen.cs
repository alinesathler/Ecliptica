using Ecliptica.Arts;
using Ecliptica.Files;
using Ecliptica.Games;
using Ecliptica.InputHandler;
using Ecliptica.Levels;
using Ecliptica.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.IO;

namespace Ecliptica.Screens
{
    internal class SaveScreen : Screen
	{
		#region Fields
		private static int _selectedSlot = 1;
		private readonly static int _totalSlots = 3;

		private static readonly float _screenAlpha = 0.6f;

		private string _saveMessage;
		private double _saveMessageTime;

		private readonly List<Button> _slotButtons;
		#endregion

		#region Properties
		public bool IsActive { get; private set; }
		public static SaveScreen Instance { get; private set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor to initialize the save screen
		/// </summary>
		public SaveScreen()
		{
			Instance = this;
			IsActive = true;

			Music = Sounds.MusicTheme;
			BackgroundSolid = Images.BackgroundBlue;
			BackgroundStars = Images.BackgroundStars;
			Font = Fonts.FontGame;
			DefaultScale = 1.0f;
			HoverScale = 1.2f;
			DefaultColor = Color.White;
			HoverColor = Color.Yellow;
			ButtonWidth = 450;
			ButtonHeight = 50;

			// Buttons
			AddButton("Save", () => { SaveGame(_selectedSlot); }, new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2, ((int)EclipticaGame.ScreenSize.Y * 2 / 3)));
			AddButton("Continue", () => { IsActive = false; ScreenManager.PopScreen(); });
			AddButton("Main Menu", () => { IsActive = false; ScreenManager.ReplaceScreen(new MenuScreen()); });
			AddButton("Exit", () => EclipticaGame.Instance.Exit());

			// Slot buttons
			_slotButtons = new();

			for (int i = 0; i < _totalSlots; i++)
			{
				int slotIndex = i;

				string slotStatus = "";

				try
				{
					slotStatus = SaveManager.IsSlotOccupied(slotIndex + 1) ? "Occupied" : "Empty";
				} catch
				{
					Instance._saveMessage = $"Failed to get slot status.";
					Instance._saveMessageTime = 2.0;
				}

				Button slotButton = new (
					 $"Slot {slotIndex + 1} - {slotStatus}",
					new Rectangle(
						((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2,
						200 + i * 50,
						ButtonWidth,
						ButtonHeight
					),
					Font,
					DefaultScale,
					HoverScale,
					DefaultColor,
					HoverColor,
					() => {
						SelectSlot(slotIndex);
					}
				);
				_slotButtons.Add(slotButton);
			}

			// Select the first slot by default
			SelectSlot(0);
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to update the save screen
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (!string.IsNullOrEmpty(_saveMessage))
			{
				_saveMessageTime -= gameTime.ElapsedGameTime.TotalSeconds;
				if (_saveMessageTime <= 0) _saveMessage = null;
			}

            // Windows or Linux
            MouseState mouseState = Mouse.GetState();

            // Touch screen
            TouchCollection touchState = TouchPanel.GetState();

            foreach (var slotButton in _slotButtons)
            {
				slotButton.Update(mouseState, touchState);
            }
        }

		/// <summary>
		/// Method to draw the save screen
		/// </summary>
		/// <param name="spriteBatch"></param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			// Draw the background with alpha
			spriteBatch.Draw(
				texture: CreateBlankTexture(spriteBatch.GraphicsDevice, Color.Black),
				destinationRectangle: new Rectangle(0, 0, (int)EclipticaGame.ScreenSize.X, (int)EclipticaGame.ScreenSize.Y),
				color: Color.Black * _screenAlpha
			);

			base.Draw(spriteBatch);

			// Draw the title
			string title = "Save Your Game";
			Vector2 titleSize = Font.MeasureString(title);
			Vector2 titlePosition = new (
				(EclipticaGame.ScreenSize.X - titleSize.X) / 2,
				100
			);
			spriteBatch.DrawString(Font, title, titlePosition, DefaultColor);

			// Draw the slot buttons
			for (int i = 0; i < _slotButtons.Count; i++)
			{
				_slotButtons[i].Draw(spriteBatch);
			}

			// Draw the instructions
			string instructions = "Select a slot and save your game";
			Vector2 instructionsSize = Font.MeasureString(instructions);
			Vector2 instructionsPosition = new (
				(spriteBatch.GraphicsDevice.Viewport.Width - instructionsSize.X) / 2,
				spriteBatch.GraphicsDevice.Viewport.Height - instructionsSize.Y
			);
			spriteBatch.DrawString(Font, instructions, instructionsPosition, Color.White);

			// Draw the save message if it exists
			if (!string.IsNullOrEmpty(_saveMessage))
			{
				Vector2 messageSize = Font.MeasureString(_saveMessage);
				Vector2 messagePosition = new (
					(EclipticaGame.ScreenSize.X - messageSize.X) / 2,
					EclipticaGame.ScreenSize.Y - instructionsSize.Y - messageSize.Y - 10
				);
				spriteBatch.DrawString(Font, _saveMessage, messagePosition, HoverColor);
			}
		}

		/// <summary>
		/// Method to save the game to a slot
		/// </summary>
		/// <param name="slot"></param>
		private static void SaveGame(int slot)
		{
			string saveData = $"Slot: {slot}\nPlayer Name: {EclipticaGame.PlayerName}\nLevel: {LevelManager.CurrentLevel.LevelNumber}\nGame Score: {EntityManager.GetTotalScore()}\nShip Lifes: {ShipPlayer.Instance.Life}";

			// Write save data to the slot file
			try
			{
				string savePath = SaveManager.GetSaveSlotPath(slot);
				File.WriteAllText(savePath, saveData);
				Console.WriteLine($"Game saved to {savePath}.");
			} catch (Exception ex)
			{
				Console.WriteLine($"Failed to save game: {ex.Message}");
				Instance._saveMessage = $"Failed to save Slot {slot}: {ex.Message}";
			}

			Instance._saveMessage = $"Slot {slot} saved successfully!";
			Instance._slotButtons[slot - 1].Text = $"Slot {slot} - Occupied";
			Instance._saveMessageTime = 2.0;
		}

		/// <summary>
		/// Method to select a slot
		/// </summary>
		/// <param name="slotIndex"></param>
		private void SelectSlot(int slotIndex)
		{
			_selectedSlot = slotIndex + 1;

			foreach (var button in _slotButtons)
			{
				button.CurrentColor = DefaultColor;
			}

			_slotButtons[slotIndex].CurrentColor = Color.Blue;
		}
		#endregion
	}
}