using System;

namespace Ecliptica.Games
{
	public static class Extensions
	{
		#region Methods
		/// <summary>
		/// Method to get the next float value
		/// </summary>
		/// <param name="rand"></param>
		/// <param name="minValue"></param>
		/// <param name="maxValue"></param>
		/// <returns>A float next value</returns>
		public static float NextFloat(this Random rand, float minValue, float maxValue)
		{
			return (float)rand.NextDouble() * (maxValue - minValue) + minValue;
		}
		#endregion
	}
}
