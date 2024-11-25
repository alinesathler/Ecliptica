using Ecliptica.Games;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Ecliptica.Arts
{
    public class Fonts
    {
        public static SpriteFont FontArial { get; private set; }
        public static SpriteFont FontGame { get; private set; }
		public static SpriteFont FontGameSmall { get; private set; }
		//public static SpriteFont FontTitle { get; private set; }

		public static void Load(ContentManager content)
        {
			FontArial = content.Load<SpriteFont>("Fonts/MyFont");
			FontGame = content.Load<SpriteFont>("Fonts/GameFont");
			FontGameSmall = content.Load<SpriteFont>("Fonts/GameFontSmaller");
			//FontTitle = content.Load<SpriteFont>("Fonts/Title");
		}
    }
}
