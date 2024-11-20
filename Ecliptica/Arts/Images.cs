using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Ecliptica.Arts
{
    public static class Images
    {
        public static Texture2D BackgroundBlue { get; set; }
        public static Texture2D BackgroundStars { get; set; }
        public static Texture2D BackgroundStars1 { get; set; }
		public static Texture2D BackgroundScreens { get; set; }
		public static Texture2D BackgroundLevelWin { get; set; }
        public static Texture2D BackgroundGameOver { get; set; }

		public static Texture2D AsteroidRedSmall { get; set; }
        public static Texture2D AsteroidRedMedium { get; set; }
        public static Texture2D AsteroidRedBig { get; set; }

        public static Texture2D ShipPlayer { get; set; }
        public static Texture2D ShipLife { get; set; }
        public static Texture2D LaserYellow { get; set; }

        public static Texture2D Explosion { get; set; }

		public static Texture2D Pixel;

		public static void Load(ContentManager content, GraphicsDevice graphicsDevice)
        {
            BackgroundBlue = content.Load<Texture2D>("Images/background-blue");
            BackgroundStars = content.Load<Texture2D>("Images/background-stars");
			BackgroundStars1 = content.Load<Texture2D>("Images/backgroud-stars-1");
			BackgroundScreens = content.Load<Texture2D>("Images/background-black");
			BackgroundLevelWin = content.Load<Texture2D>("Images/background-level-transition");
			BackgroundGameOver = content.Load<Texture2D>("Images/background-game-over");


			AsteroidRedSmall = content.Load<Texture2D>("Images/asteroid-red-small");
            AsteroidRedMedium = content.Load<Texture2D>("Images/asteroid-red-medium");
            AsteroidRedBig = content.Load<Texture2D>("Images/asteroid-red-big");

            ShipPlayer = content.Load<Texture2D>("Images/ship-player");
            ShipLife = content.Load<Texture2D>("Images/life-sheet");
            LaserYellow = content.Load<Texture2D>("Images/laser-yellow");

            Explosion = content.Load<Texture2D>("Images/explosion");

			Pixel = new Texture2D(graphicsDevice, 1, 1);
			Pixel.SetData(new[] { Color.White });
		}
    }
}
