using Ecliptica.Games;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ecliptica.Arts
{
    public class Fonts
    {
        #region Properties
        public static SpriteFont FontArial { get; private set; }
        public static SpriteFont FontGame { get; private set; }
        public static SpriteFont FontGameSmall { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Method to load the fonts
        /// </summary>
        /// <param name="content"></param>
        public static void Load(ContentManager content)
        {
            FontArial = content.Load<SpriteFont>("Fonts/MyFont");
            FontGame = content.Load<SpriteFont>("Fonts/GameFont");
            FontGameSmall = content.Load<SpriteFont>("Fonts/GameFontSmaller");
        }
        #endregion
    }
}
