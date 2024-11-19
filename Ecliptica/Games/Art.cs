using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Ecliptica.Games
{
    public static class Art
    {
        public static Texture2D AsteroidRedSmall { get; set; }
		public static Texture2D AsteroidRedMedium { get; set; }
		public static Texture2D AsteroidRedBig { get; set; }

        public static Texture2D ShipPlayer { get; set; }
		public static Texture2D LaserYellow { get; set; }

		public static Texture2D Explosion { get; set; }

		public static void Load(ContentManager content)
        {
			AsteroidRedSmall = content.Load<Texture2D>("Images/asteroid-red-small");
			AsteroidRedMedium = content.Load<Texture2D>("Images/asteroid-red-medium");
			AsteroidRedBig = content.Load<Texture2D>("Images/asteroid-red-big");

			ShipPlayer = content.Load<Texture2D>("Images/ship-player");
			LaserYellow = content.Load<Texture2D>("Images/laser-yellow");

			Explosion = content.Load<Texture2D>("Images/explosion");
		}
    }
}
