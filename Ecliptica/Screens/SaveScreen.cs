using Ecliptica.Arts;
using Ecliptica.Levels;
using Ecliptica.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Ecliptica.Games
{
	internal class SaveScreen : Screen
	{
		private Rectangle _saveButtonRect;
		private Button _saveButton;

		private Rectangle _returnButtonRect;
		private Button _returnButton;

		private Rectangle _exitButtonRect;
		private Button _exitButton;

		private static int _selectedSlot = 1;
		private static int _totalSlots = 3;

		private static float _screenAlpha = 0.6f;

		public bool IsActive;
		public static SaveScreen Instance { get; private set; }

		private string _saveMessage;
		private double _saveMessageTime;
		private List<Button> slotButtons;

		private int _levelNumber;
		private int _score;
		private int _lives;


		public SaveScreen()
		{
			Instance = this;
			IsActive = true;

			Music = Sounds.MusicTheme;
			Font = Fonts.FontGame;
			DefaultScale = 1.0f;
			HoverScale = 1.2f;
			DefaultColor = Color.White;
			HoverColor = Color.Yellow;
			ButtonWidth = 450;
			ButtonHeight = 50;

			// Save button
			_saveButtonRect = new Rectangle(
				((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2,
				((int)EclipticaGame.ScreenSize.Y - ButtonHeight) / 2,
				ButtonWidth,
				ButtonHeight);

			_saveButton = new Button(
			"Save",
			_saveButtonRect,
			Font,
			DefaultScale,
			HoverScale,
			DefaultColor,
			HoverColor,
			() => { SaveGame(_selectedSlot); }
			);

			Buttons.Add(_saveButton);

			// Return button
			_returnButtonRect = new Rectangle(Buttons[0].Bounds.X, Buttons[0].Bounds.Y + (ButtonHeight + 10) * Buttons.Count, ButtonWidth,
				ButtonHeight);

			_returnButton = new Button("Continue", _returnButtonRect, Font, DefaultScale, HoverScale, DefaultColor, HoverColor, () =>
			{
				IsActive = false;
				ScreenManager.PopScreen();
			});

			Buttons.Add(_returnButton);

			// Exit button
			_exitButtonRect = new Rectangle(Buttons[0].Bounds.X, Buttons[0].Bounds.Y + (ButtonHeight + 10) * Buttons.Count, ButtonWidth,
				ButtonHeight);

			_exitButton = new Button("Exit", _exitButtonRect, Font, DefaultScale, HoverScale, DefaultColor, HoverColor, () => EclipticaGame.Instance.Exit());

			Buttons.Add(_exitButton);

			slotButtons = new List<Button>();

			for (int i = 0; i < _totalSlots; i++)
			{
				int slotIndex = i;
				string slotStatus = GetSaveSlotStatus(slotIndex + 1);

				Button slotButton = new Button(
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
				slotButtons.Add(slotButton);
			}

			SelectSlot(0);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (KeyboardHandler.IsKeyPressed(Keys.Up))
			{
				_selectedSlot = Math.Max(1, _selectedSlot - 1);
			} else if (KeyboardHandler.IsKeyPressed(Keys.Down))
			{
				_selectedSlot = Math.Min(_totalSlots, _selectedSlot + 1);
			}

			if (!string.IsNullOrEmpty(_saveMessage))
			{
				_saveMessageTime -= gameTime.ElapsedGameTime.TotalSeconds;
				if (_saveMessageTime <= 0) _saveMessage = null;
			}

            foreach (var slotButton in slotButtons)
            {
				slotButton.Update(Mouse.GetState());
            }
        }

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
			Vector2 titlePosition = new Vector2(
				(EclipticaGame.ScreenSize.X - titleSize.X) / 2,
				100
			);
			spriteBatch.DrawString(Font, title, titlePosition, Color.White);

			for (int i = 0; i < slotButtons.Count; i++)
			{
				Color slotColor = (i == _selectedSlot) ? Color.Yellow : Color.White;

				slotButtons[i].Draw(spriteBatch);
			}

			// Draw the instructions
			string instructions = "Select a slot and save your game";
			Vector2 instructionsSize = Font.MeasureString(instructions);
			Vector2 instructionsPosition = new Vector2(
				(spriteBatch.GraphicsDevice.Viewport.Width - instructionsSize.X) / 2,
				spriteBatch.GraphicsDevice.Viewport.Height - 100
			);
			spriteBatch.DrawString(Font, instructions, instructionsPosition, Color.White);

			// Draw the save message if it exists
			if (!string.IsNullOrEmpty(_saveMessage))
			{
				Vector2 messageSize = Font.MeasureString(_saveMessage);
				Vector2 messagePosition = new Vector2(
					(EclipticaGame.ScreenSize.X - messageSize.X) / 2,
					EclipticaGame.ScreenSize.Y - 150
				);
				spriteBatch.DrawString(Font, _saveMessage, messagePosition, HoverColor);
			}
		}

		private static void SaveGame(int slot)
		{
			string saveData = $"Slot: {slot}\nLevel: {LevelManager.CurrentLevel.LevelNumber}\nGame Score: {EntityManager.GetNumberOfEnemiesDestroyedTotal()}\nShip Lifes: {ShipPlayer.Instance.Life}";
			string savePath = GetSaveSlotPath(slot);

			// Write save data to the slot file
			try
			{
				File.WriteAllText(savePath, saveData);
				Console.WriteLine($"Game saved to {savePath}.");
			} catch (Exception ex)
			{
				Console.WriteLine($"Failed to save game: {ex.Message}");
			}

			SaveScreen.Instance._saveMessage = $"Slot {slot} saved successfully!";
			SaveScreen.Instance.slotButtons[slot - 1].text = $"Slot {slot} - Occupied";
			SaveScreen.Instance._saveMessageTime = 2.0;
		}

		private static string GetSaveSlotStatus(int slot)
		{
			string savePath = GetSaveSlotPath(slot);
			return File.Exists(savePath) ? "Occupied" : "Empty";
		}

		private static string GetSaveSlotPath(int slot)
		{
			string saveDirectory = "Saves";
			if (!Directory.Exists(saveDirectory))
			{
				Directory.CreateDirectory(saveDirectory);
			}
			return Path.Combine(saveDirectory, $"savegame_slot_{slot}.txt");
		}

		private void SelectSlot(int slotIndex)
		{
			_selectedSlot = slotIndex + 1;

			foreach (var button in slotButtons)
			{
				button.CurrentColor = DefaultColor;
			}

			var i = slotIndex;

			slotButtons[slotIndex].CurrentColor = Color.Blue;
		}
	}
}