//using Ecliptica.Games;
//using Microsoft.Xna.Framework;
//using Ecliptica.Arts;

//namespace Ecliptica.Screens
//{
//	internal class LoadScreen : Screen
//	{
//		public LoadScreen()
//		{
//			Music = Sounds.MenuScreen;
//			BackgroundSolid = Images.BackgroundScreens;
//			BackgroundStars = Images.BackgroundStars1;
//			Font = Fonts.FontGame;
//			DefaultScale = 1.0f;
//			HoverScale = 1.2f;
//			DefaultColor = Color.White;
//			HoverColor = Color.Yellow;
//			ButtonWidth = 450;
//			ButtonHeight = 50;

//			// Buttons
//			AddButton("Return", () => ScreenManager.PopScreen(), new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2, 10));
//		}
//	}
//}

using Ecliptica.Arts;
using Ecliptica.Files;
using Ecliptica.Levels;
using Ecliptica.Screens;
using Ecliptica.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Ecliptica.Games
{
	internal class LoadScreen : Screen
	{
		private static int _selectedSlot = 1;
		private static int _totalSlots = 3;

		private static float _screenAlpha = 0.6f;

		public bool IsActive;
		public static LoadScreen Instance { get; private set; }

		private string _loadMessage;
		private double _loadMessageTime;
		private List<Button> slotButtons;

		private static bool _isLoadSuccessful;
		private static string _playerName;
		private static int _levelNumber;
		private static int _gameScore;
		private static int _shipLifes;

		public LoadScreen()
		{
			Instance = this;
			IsActive = true;

			Music = Sounds.MenuScreen;
			BackgroundSolid = Images.BackgroundScreens;
			BackgroundStars = Images.BackgroundStars1;
			Font = Fonts.FontGame;
			DefaultScale = 1.0f;
			HoverScale = 1.2f;
			DefaultColor = Color.White;
			HoverColor = Color.Yellow;
			ButtonWidth = 450;
			ButtonHeight = 50;

			// Buttons
			AddButton("Load", () => { LoadGame(_selectedSlot); }, new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2, ((int)EclipticaGame.ScreenSize.Y * 2 / 3)));
			AddButton("Return", () => { IsActive = false; ScreenManager.PopScreen(); });
			AddButton("Exit", () => EclipticaGame.Instance.Exit());

			slotButtons = new();

			for (int i = 0; i < _totalSlots; i++)
			{
				int slotIndex = i;
				string slotStatus = SaveManager.IsSlotOccupied(slotIndex + 1) ? "Occupied" : "Empty";

				Button slotButton = new(
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
					() => { SelectSlot(slotIndex); }
				);
				slotButtons.Add(slotButton);
			}

			// Select the first slot by default
			SelectSlot(0);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (!string.IsNullOrEmpty(_loadMessage))
			{
				_loadMessageTime -= gameTime.ElapsedGameTime.TotalSeconds;
				if (_loadMessageTime <= 0) _loadMessage = null;

				if (_loadMessageTime <= 0 && _isLoadSuccessful)
				{
					IsActive = false;
					ScreenManager.ReplaceScreen(new GameScreen(true, _shipLifes, _levelNumber + 1, _gameScore));
				}
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
			string title = "Load Your Game";
			Vector2 titleSize = Font.MeasureString(title);
			Vector2 titlePosition = new(
				(EclipticaGame.ScreenSize.X - titleSize.X) / 2,
				100
			);
			spriteBatch.DrawString(Font, title, titlePosition, Color.White);

			foreach (var slotButton in slotButtons)
			{
				slotButton.Draw(spriteBatch);
			}

			// Draw the instructions
			string instructions = "Select a slot and load your game";
			Vector2 instructionsSize = Font.MeasureString(instructions);
			Vector2 instructionsPosition = new(
				(spriteBatch.GraphicsDevice.Viewport.Width - instructionsSize.X) / 2,
				spriteBatch.GraphicsDevice.Viewport.Height - 100
			);
			spriteBatch.DrawString(Font, instructions, instructionsPosition, Color.White);

			// Draw the load message if it exists
			if (!string.IsNullOrEmpty(_loadMessage))
			{
				Vector2 messageSize = Font.MeasureString(_loadMessage);
				Vector2 messagePosition = new(
					(EclipticaGame.ScreenSize.X - messageSize.X) / 2,
					EclipticaGame.ScreenSize.Y - 150
				);
				spriteBatch.DrawString(Font, _loadMessage, messagePosition, HoverColor);
			}
		}

		private static void LoadGame(int slot)
		{
			_isLoadSuccessful = false;

			string savePath = SaveManager.GetSaveSlotPath(slot);

			if (!File.Exists(savePath))
			{
				Instance._loadMessage = $"Slot {slot} is empty!";
				Instance._loadMessageTime = 2.0;
				return;
			}

			try
			{
				string[] saveData = File.ReadAllLines(savePath);

				_playerName = "";
				_levelNumber = 0;
				_gameScore = 0;
				_shipLifes = 0;

				bool isLevelNumber = false;
				bool isGameScore = false;
				bool isShipLifes = false;

				foreach (var line in saveData)
				{
					if (line.StartsWith("Level:"))
					{
						isLevelNumber = int.TryParse(line.Split(':')[1].Trim(), out _levelNumber);
					} else if (line.StartsWith("Player Name:"))
					{
						_playerName = line.Split(':')[1].Trim();
					} else if (line.StartsWith("Game Score:"))
					{
						isGameScore = int.TryParse(line.Split(':')[1].Trim(), out _gameScore);
					} else if (line.StartsWith("Ship Lifes:"))
					{
						isShipLifes = int.TryParse(line.Split(':')[1].Trim(), out _shipLifes);
					}
				}

				if (!isLevelNumber || !isGameScore || !isShipLifes || string.IsNullOrEmpty(_playerName))
				{
					throw new Exception("Invalid save data");
				}

				EclipticaGame.PlayerName = _playerName;

				_isLoadSuccessful = true;

				Instance._loadMessage = $"Welcome {_playerName}. Slot {slot} loaded successfully!";
			} catch (Exception ex)
			{
				Instance._loadMessage = $"Failed to load Slot {slot}: {ex.Message}";
			}

			Instance._loadMessageTime = 2.0;
		}

		private void SelectSlot(int slotIndex)
		{
			_selectedSlot = slotIndex + 1;

			foreach (var button in slotButtons)
			{
				button.CurrentColor = DefaultColor;
			}

			slotButtons[slotIndex].CurrentColor = Color.Blue;
		}
	}
}
