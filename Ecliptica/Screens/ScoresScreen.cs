using Ecliptica.Arts;
using Ecliptica.Files;
using Ecliptica.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ecliptica.Screens
{
	public class ScoresScreen : Screen
	{
		#region Fields
		private readonly List<KeyValuePair<int, String>> _scores;
		private string _message;
		private double _messageTime;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor to initialize the scores screen
		/// </summary>
		public ScoresScreen()
		{
			_message = "";
			_messageTime = 0;

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
			AddButton("Return", () => { ScreenManager.PopScreen(); }, new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2, ((int)EclipticaGame.ScreenSize.Y * 2 / 3)));
			AddButton("Exit", () => EclipticaGame.Instance.Exit());

			// Scores
			_scores = new();

			try
			{
				_scores = ReadHighScores();
			} catch (Exception ex)
			{
				Console.WriteLine($"Failed to read high scores: {ex.Message}");
				_message = "Failed to read high scores";
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Method to update the scores screen
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			// Update time for the message
			if (!string.IsNullOrEmpty(_message))
			{
				_messageTime -= gameTime.ElapsedGameTime.TotalSeconds;
				if (_messageTime <= 0) _message = null;
			}
		}

		/// <summary>
		/// Method to draw the scores screen
		/// </summary>
		/// <param name="spriteBatch"></param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			// Draw the title
			string title = "Hall of Fame";
			Vector2 titleSize = Font.MeasureString(title);
			Vector2 titlePosition = new(
				(EclipticaGame.ScreenSize.X - titleSize.X) / 2,
				100
			);
			spriteBatch.DrawString(Font, title, titlePosition, DefaultColor);

			// Draw the scores
			float startY = titlePosition.Y + titleSize.Y + 30;
			float spaceBetween = 15;
			foreach (var score in _scores)
			{
				string scoreText = $"{score.Value} - {score.Key}";
				Vector2 position = new(
					(EclipticaGame.ScreenSize.X - Fonts.FontGameSmall.MeasureString(scoreText).X) / 2,
					startY
				);
				spriteBatch.DrawString(Fonts.FontGameSmall, scoreText, position, DefaultColor);
				startY += Fonts.FontGameSmall.MeasureString(scoreText).Y + spaceBetween;
			}

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

		/// <summary>
		/// Method to read the high scores from the file
		/// </summary>
		/// <returns>A list with a KeyValuePair<int, String> score and player name</returns>
		public static List<KeyValuePair<int, String>> ReadHighScores()
		{
			string path = SaveManager.GetScoresPath();
			List<KeyValuePair<int, String>> scores = new();

			// Load high scores from the file
			if (File.Exists(path))
			{
				string[] lines = File.ReadAllLines(path);

				foreach (string line in lines)
				{
					string[] parts = line.Split(',');
					string playerName = parts[0];
					bool isScore = int.TryParse(parts[1], out int score);

					if (!isScore || string.IsNullOrEmpty(playerName))
					{
						continue;
					}

					scores.Add(new KeyValuePair<int, String> (score, playerName));
					var test = scores;
				}
			}

			// Sort the scores
			scores.Sort((a, b) => b.Key.CompareTo(a.Key));

			return scores;
		}

		/// <summary>
		/// Method to write the high scores to the file
		/// </summary>
		/// <param name="scores"></param>
		public static void WriteHighScores(List<KeyValuePair<int, String>> scores)
		{
			string path = SaveManager.GetScoresPath();

			// Write high scores to the file
			using StreamWriter writer = new(path);
			{
				foreach (KeyValuePair<int, String> score in scores)
				{
					writer.WriteLine($"{score.Value},{score.Key}");
				}
			}
		}

		/// <summary>
		/// Method to update the high scores
		/// </summary>
		public static void UpdateHighScores()
		{
			try
			{
				List<KeyValuePair<int, String>> scores = ReadHighScores();

				scores.Add(new KeyValuePair<int, String> (EntityManager.GetTotalScore(), EclipticaGame.PlayerName));

				// Sort the scores
				scores.Sort((a, b) => b.Key.CompareTo(a.Key));

				// Remove the lowest score if there are more than 10
				if (scores.Count > 10)
				{
					scores = scores.Take(10).ToList();
				}

				WriteHighScores(scores);
			} catch (Exception ex)
			{
				Console.WriteLine($"Failed to update high scores: {ex.Message}");
			}
		}
		#endregion
	}
}

