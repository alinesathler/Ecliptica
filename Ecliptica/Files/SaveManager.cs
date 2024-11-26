using System.IO;

namespace Ecliptica.Files
{
	internal class SaveManager
	{
		#region Methods
		/// <summary>
		/// Method to get the path of a save slot
		/// </summary>
		/// <param name="slot"></param>
		/// <returns>The path of the save file for the slot</returns>
		public static string GetSaveSlotPath(int slot)
		{
			if (!Directory.Exists(FileIO.SaveDirectory))
			{
				Directory.CreateDirectory(FileIO.SaveDirectory);
			}
			return Path.Combine(FileIO.SaveDirectory, $"savegame_slot_{slot}.txt");
		}

		/// <summary>
		/// Method to check if a slot is occupied or empty
		/// </summary>
		/// <param name="slot"></param>
		/// <returns>Return true if it's occupied</returns>
		public static bool IsSlotOccupied(int slot)
		{
			return File.Exists(GetSaveSlotPath(slot));
		}

		/// <summary>
		/// Method to get the path of the scores file
		/// </summary>
		/// <returns>The path of the high scores file</returns>
		public static string GetScoresPath()
		{
			if (!Directory.Exists(FileIO.ScoresDirectory))
			{
				Directory.CreateDirectory(FileIO.ScoresDirectory);
			}
			return Path.Combine(FileIO.ScoresDirectory, "scores.txt");
		}
		#endregion
	}
}
