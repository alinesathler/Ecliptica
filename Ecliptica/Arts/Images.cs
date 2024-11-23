using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Ecliptica.Arts
{
    public static class Images
    {
		public static Texture2D BackgroundTitle { get; set; }
		public static Texture2D BackgroundBlue { get; set; }
        public static Texture2D BackgroundStars { get; set; }
        public static Texture2D BackgroundStars1 { get; set; }
		public static Texture2D BackgroundScreens { get; set; }
		public static Texture2D BackgroundLevelWin { get; set; }
        public static Texture2D BackgroundGameOver { get; set; }
        public static Texture2D BackgroundYouWin { get; set; }
		public static Texture2D BackgroundAlpha { get; set; }

		public static Texture2D Cursor { get; set; }

		public static Texture2D Ecliptica { get; set; }

		public static Texture2D AsteroidRedSmall { get; set; }
        public static Texture2D AsteroidRedMedium { get; set; }
        public static Texture2D AsteroidRedBig { get; set; }

		public static Texture2D AsteroidBlueSmall { get; set; }
		public static Texture2D AsteroidBlueMedium { get; set; }
		public static Texture2D AsteroidBlueBig { get; set; }

		public static Texture2D ShipPlayer { get; set; }
        public static Texture2D LaserYellow { get; set; }

        public static Texture2D Explosion { get; set; }
        public static Texture2D FireworksCrystalBlue { get; set; }
		public static Texture2D FireworksCrystalGreen { get; set; }
		public static Texture2D FireworksCrystalOrnge { get; set; }
		public static Texture2D FireworksCrystalWhite { get; set; }
		public static Texture2D FireworksDefaultBlue { get; set; }
		public static Texture2D FireworksDefaultGreen { get; set; }
		public static Texture2D FireworksDefaultOrange { get; set; }
		public static Texture2D FireworksLongBlue { get; set; }
		public static Texture2D FireworksLongGreen { get; set; }
		public static Texture2D FireworksLongOrange { get; set; }
		public static Texture2D FireworksRocketBlue { get; set; }
		public static Texture2D FireworksRocketOrange { get; set; }

		public static List<Texture2D> LifeBarShip { get; set; }
		public static Texture2D Life { get; set; }


		public static Texture2D Pixel;

		public static void Load(ContentManager content, GraphicsDevice graphicsDevice)
        {
			BackgroundTitle = content.Load<Texture2D>("Images/backgroundTitlePage");
			BackgroundBlue = content.Load<Texture2D>("Images/background-blue");
            BackgroundStars = content.Load<Texture2D>("Images/background-stars");
			BackgroundStars1 = content.Load<Texture2D>("Images/backgroud-stars-1");
			BackgroundScreens = content.Load<Texture2D>("Images/background-black");
			BackgroundLevelWin = content.Load<Texture2D>("Images/background-level-transition");
			BackgroundGameOver = content.Load<Texture2D>("Images/background-game-over");
			BackgroundYouWin = content.Load<Texture2D>("Images/background-you-win");
			BackgroundAlpha = content.Load<Texture2D>("Images/background-alpha");

			Cursor = content.Load<Texture2D>("Images/mouse-rocket");

			Ecliptica = content.Load<Texture2D>("Images/Ecliptica");

			AsteroidRedSmall = content.Load<Texture2D>("Images/asteroid-red-small");
            AsteroidRedMedium = content.Load<Texture2D>("Images/asteroid-red-medium");
            AsteroidRedBig = content.Load<Texture2D>("Images/asteroid-red-big");

			AsteroidBlueSmall = content.Load<Texture2D>("Images/asteroid-blue-small");
			AsteroidBlueMedium = content.Load<Texture2D>("Images/asteroid-blue-medium");
			AsteroidBlueBig = content.Load<Texture2D>("Images/asteroid-blue-big");

			ShipPlayer = content.Load<Texture2D>("Images/ship-player");
            LaserYellow = content.Load<Texture2D>("Images/laser-yellow");

            Explosion = content.Load<Texture2D>("Images/explosion");
			FireworksCrystalBlue = content.Load<Texture2D>("Images/explosion-crystals-blue-sheet");
			FireworksCrystalGreen = content.Load<Texture2D>("Images/explosion-crystals-green-sheet");
			FireworksCrystalOrnge = content.Load<Texture2D>("Images/explosion-crystals-orange-sheet");
			FireworksCrystalWhite = content.Load<Texture2D>("Images/explosion-crystals-white-sheet");
			FireworksDefaultBlue = content.Load<Texture2D>("Images/explosion-default-blue-sheet");
			FireworksDefaultGreen = content.Load<Texture2D>("Images/explosion-default-green-sheet");
			FireworksDefaultOrange = content.Load<Texture2D>("Images/explosion-default-orange-sheet");
			FireworksLongBlue = content.Load<Texture2D>("Images/explosion-long-blue-sheet");
			FireworksLongGreen = content.Load<Texture2D>("Images/explosion-long-green-sheet");
			FireworksLongOrange = content.Load<Texture2D>("Images/explosion-long-orange-sheet");
			FireworksRocketBlue = content.Load<Texture2D>("Images/explosion-rocket-blue-sheet");
			FireworksRocketOrange = content.Load<Texture2D>("Images/explosion-rocket-orange-sheet");

			LifeBarShip = new List<Texture2D>();
			LifeBarShip.Add(content.Load<Texture2D>("Images/life-bar-animated-0"));
			LifeBarShip.Add(content.Load<Texture2D>("Images/life-bar-animated-1"));
			LifeBarShip.Add(content.Load<Texture2D>("Images/life-bar-animated-2"));
			LifeBarShip.Add(content.Load<Texture2D>("Images/life-bar-animated-3"));
			LifeBarShip.Add(content.Load<Texture2D>("Images/life-bar-animated-4"));
			LifeBarShip.Add(content.Load<Texture2D>("Images/life-bar-animated-5"));
			LifeBarShip.Add(content.Load<Texture2D>("Images/life-bar-animated-6"));
			LifeBarShip.Add(content.Load<Texture2D>("Images/life-bar-animated-7"));
			LifeBarShip.Add(content.Load<Texture2D>("Images/life-bar-animated-8"));
			Life = content.Load<Texture2D>("Images/life-sheet");

			Pixel = new Texture2D(graphicsDevice, 1, 1);
			Pixel.SetData(new[] { Color.White });
		}
    }
}
