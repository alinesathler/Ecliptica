using Ecliptica.Arts;
using Ecliptica.Files;
using Ecliptica.Levels;
using Ecliptica.UI;
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
		private static int _selectedSlot = 1;
		private static int _totalSlots = 3;

		private static float _screenAlpha = 0.6f;

		public bool IsActive;
		public static SaveScreen Instance { get; private set; }

		private string _saveMessage;
		private double _saveMessageTime;
		private List<Button> slotButtons;

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

			slotButtons = new();

			for (int i = 0; i < _totalSlots; i++)
			{
				int slotIndex = i;
				string slotStatus = SaveManager.IsSlotOccupied(slotIndex + 1) ? "Occupied" : "Empty";

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
				slotButtons.Add(slotButton);
			}

			// Select the first slot by default
			SelectSlot(0);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

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
			Vector2 titlePosition = new (
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
			Vector2 instructionsPosition = new (
				(spriteBatch.GraphicsDevice.Viewport.Width - instructionsSize.X) / 2,
				spriteBatch.GraphicsDevice.Viewport.Height - 100
			);
			spriteBatch.DrawString(Font, instructions, instructionsPosition, Color.White);

			// Draw the save message if it exists
			if (!string.IsNullOrEmpty(_saveMessage))
			{
				Vector2 messageSize = Font.MeasureString(_saveMessage);
				Vector2 messagePosition = new (
					(EclipticaGame.ScreenSize.X - messageSize.X) / 2,
					EclipticaGame.ScreenSize.Y - 150
				);
				spriteBatch.DrawString(Font, _saveMessage, messagePosition, HoverColor);
			}
		}

		private static void SaveGame(int slot)
		{
			string saveData = $"Slot: {slot}\nPlayer Name: {EclipticaGame.PlayerName}\nLevel: {LevelManager.CurrentLevel.LevelNumber}\nGame Score: {EntityManager.GetTotalScore()}\nShip Lifes: {ShipPlayer.Instance.Life}";
			string savePath = SaveManager.GetSaveSlotPath(slot);

			// Write save data to the slot file
			try
			{
				File.WriteAllText(savePath, saveData);
				Console.WriteLine($"Game saved to {savePath}.");
			} catch (Exception ex)
			{
				Console.WriteLine($"Failed to save game: {ex.Message}");
				SaveScreen.Instance._saveMessage = $"Failed to save Slot {slot}: {ex.Message}";
			}

			SaveScreen.Instance._saveMessage = $"Slot {slot} saved successfully!";
			SaveScreen.Instance.slotButtons[slot - 1].text = $"Slot {slot} - Occupied";
			SaveScreen.Instance._saveMessageTime = 2.0;
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