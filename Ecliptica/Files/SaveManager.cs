using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecliptica.Files
{
	internal class SaveManager
	{
		public static string GetSaveSlotPath(int slot)
		{
			if (!Directory.Exists(FileIO.SaveDirectory))
			{
				Directory.CreateDirectory(FileIO.SaveDirectory);
			}
			return Path.Combine(FileIO.SaveDirectory, $"savegame_slot_{slot}.txt");
		}

		public static bool IsSlotOccupied(int slot)
		{
			return File.Exists(GetSaveSlotPath(slot));
		}
	}
}
