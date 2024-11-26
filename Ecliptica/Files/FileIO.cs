using System.IO;
using System;

namespace Ecliptica.Files
{
	internal class FileIO
	{
		#region Properties
		private static readonly string BaseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Ecliptica");

		internal static readonly string SaveDirectory = Path.Combine(BaseDirectory, "Saves");

		internal static readonly string ScoresDirectory = Path.Combine(BaseDirectory, "Scores");
		#endregion

		#region Methods
		/// <summary>
		/// Method to ensures that necessary directories exist.
		/// </summary>
		internal static void EnsureDirectories()
		{
			try
			{
				if (!Directory.Exists(SaveDirectory))
				{
					Directory.CreateDirectory(SaveDirectory);
				}

				if (!Directory.Exists(ScoresDirectory))
				{
					Directory.CreateDirectory(ScoresDirectory);
				}
			} catch (Exception ex)
			{
				Console.WriteLine($"Error ensuring directories: {ex.Message}");
				throw;
			}
		}
		#endregion
	}
}
