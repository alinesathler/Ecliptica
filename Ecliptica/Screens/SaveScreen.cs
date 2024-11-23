using Ecliptica.Arts;
using Ecliptica.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

		private static float _screenAlpha = 0.8f;

		public bool IsActive;
		public static SaveScreen Instance { get; private set; }

		public SaveScreen()
		{
			Instance = this;
			IsActive = true;

			Music = Sounds.MusicTheme;
			BackgroundSolid = Images.BackgroundScreens;
			BackgroundStars = Images.BackgroundStars1;
			Font = Fonts.FontGame;
			DefaultScale = 1.0f;
			HoverScale = 1.2f;
			DefaultColor = Color.White;
			HoverColor = Color.Yellow;
			ButtonWidth = 450;
			ButtonHeight = 50;

			// Resume button
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

			_returnButton = new Button("Return", _returnButtonRect, Font, DefaultScale, HoverScale, DefaultColor, HoverColor, () => ScreenManager.PopScreen());

			Buttons.Add(_returnButton);

			// Exit button
			_exitButtonRect = new Rectangle(Buttons[1].Bounds.X, Buttons[1].Bounds.Y + (ButtonHeight + 10) * Buttons.Count, ButtonWidth,
				ButtonHeight);

			_exitButton = new Button("Exit", _exitButtonRect, Font, DefaultScale, HoverScale, DefaultColor, HoverColor, () => ScreenManager.PopScreen());

			Buttons.Add(_exitButton);
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
		}

		public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
		{
			base.Draw(spriteBatch);

			//spriteBatch.Draw(
			//	texture: CreateBlankTexture(graphicsDevice, Color.Black),
			//	destinationRectangle: new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height),
			//	color: Color.Black * _screenAlpha
			//);

			string title = "Save Your Game";
			Vector2 titleSize = Font.MeasureString(title);
			Vector2 titlePosition = new Vector2(
				(graphicsDevice.Viewport.Width - titleSize.X) / 2,
				100
			);

			spriteBatch.DrawString(Font, title, titlePosition, Color.White);

			for (int i = 1; i <= _totalSlots; i++)
			{
				string slotText = $"Slot {i} - {GetSaveSlotStatus(i)}";
				Vector2 slotSize = Font.MeasureString(slotText);
				Vector2 slotPosition = new Vector2(
					(graphicsDevice.Viewport.Width - slotSize.X) / 2,
					200 + (i - 1) * 50
				);

				Color slotColor = (i == _selectedSlot) ? Color.Yellow : Color.White;
				spriteBatch.DrawString(Font, slotText, slotPosition, slotColor);
			}

			string instructions = "[Up/Down] Select Slot   [Enter] Save   [Esc] Cancel";
			Vector2 instructionsSize = Font.MeasureString(instructions);
			Vector2 instructionsPosition = new Vector2(
				(graphicsDevice.Viewport.Width - instructionsSize.X) / 2,
				graphicsDevice.Viewport.Height - 100
			);

			spriteBatch.DrawString(Font, instructions, instructionsPosition, Color.White);
		}

		private static void SaveGame(int slot)
		{
			// Example save data
			string saveData = $"Slot: {slot}\nLevel: 2\nGame Score: 3";
			string savePath = GetSaveSlotPath(slot);

			// Write save data to the slot file
			File.WriteAllText(savePath, saveData);
			System.Console.WriteLine($"Game saved to {savePath}.");

			// Return to the game
			SaveScreen.Instance.IsActive = false;
			ScreenManager.PopScreen();
		}

		private static string GetSaveSlotStatus(int slot)
		{
			string savePath = GetSaveSlotPath(slot);
			return File.Exists(savePath) ? "Occupied" : "Empty";
		}

		private static string GetSaveSlotPath(int slot)
		{
			return $"savegame_slot_{slot}.txt";
		}

		private static Texture2D CreateBlankTexture(GraphicsDevice graphicsDevice, Color color)
		{
			Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
			texture.SetData(new[] { color });
			return texture;
		}
	}
}