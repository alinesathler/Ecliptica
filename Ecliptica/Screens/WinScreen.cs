using Ecliptica.Arts;
using Ecliptica.Games;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Ecliptica.Screens
{
	internal class WinScreen : Screen
	{
		List<Firework> fireworks = new List<Firework>();

		List<Firework> fireworksStore = new List<Firework>();

		public WinScreen()
		{
			Music = Sounds.GameEnd;
			BackgroundSolid = Images.BackgroundYouWin;
			BackgroundStars = Images.BackgroundStars1;
			Font = Fonts.FontGame;
			DefaultScale = 1.0f;
			HoverScale = 1.2f;
			DefaultColor = Color.White;
			HoverColor = Color.Yellow;
			ButtonWidth = 450;
			ButtonHeight = 50;

			// Buttons
			AddButton("Play Again", () => ScreenManager.ReplaceScreen(new GameScreen()), new Vector2(((int)EclipticaGame.ScreenSize.X - ButtonWidth) / 2, (int)EclipticaGame.ScreenSize.Y / 2 + ButtonHeight));
			AddButton("Main Menu", () => ScreenManager.ReplaceScreen(new MenuScreen()));
			AddButton("High Scores", () => ScreenManager.PushScreen(new ScoresScreen()));
			AddButton("Exit", () => EclipticaGame.Instance.Exit());

			fireworksStore.Add(new Firework(Images.FireworksRocketBlue, 1, 50, Images.FireworksLongBlue, 1, 57, startPosition: EclipticaGame.ScreenSize / 2,
					   startVelocity: new Vector2(0, -100),
					   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketOrange, 1, 50, Images.FireworksLongOrange, 1, 57, startPosition: new Vector2(EclipticaGame.ScreenSize.X * 8 / 10, EclipticaGame.ScreenSize.Y),
					   startVelocity: new Vector2(0, -200),
					   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketBlue, 1, 50, Images.FireworksLongGreen, 1, 54,startPosition: EclipticaGame.ScreenSize/3,
					   startVelocity: new Vector2(0, -150),
					   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketOrange, 1, 50, Images.FireworksCrystalBlue, 1, 82, startPosition: new Vector2(EclipticaGame.ScreenSize.X * 9/10, EclipticaGame.ScreenSize.Y * 9/10),
					   startVelocity: new Vector2(0, -300),
					   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketBlue, 1, 50, Images.FireworksLongBlue, 1, 57, startPosition: EclipticaGame.ScreenSize * 6/10,
		   startVelocity: new Vector2(0, -50),
		   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketOrange, 1, 50, Images.FireworksLongOrange, 1, 57, startPosition: new Vector2(EclipticaGame.ScreenSize.X * 4/10, EclipticaGame.ScreenSize.Y * 8/10),
					   startVelocity: new Vector2(0, -100),
					   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketBlue, 1, 50, Images.FireworksLongGreen, 1, 54, startPosition: EclipticaGame.ScreenSize * 7 / 10,
					   startVelocity: new Vector2(0, -50),
					   trailDuration: 2.0f));
			fireworksStore.Add(new Firework(Images.FireworksRocketOrange, 1, 50, Images.FireworksCrystalBlue, 1, 82, startPosition: new Vector2(EclipticaGame.ScreenSize.X /10, EclipticaGame.ScreenSize.Y - 100),
					   startVelocity: new Vector2(0, -400),
					   trailDuration: 2.0f));

			foreach (var firework in fireworksStore)
			{
				fireworks.Add(Firework.Clone(firework));
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			// Update fireworks
			foreach (var firework in fireworks)
			{
				firework.Update(gameTime);
			}

			// Remove finished fireworks
			for (int i = fireworks.Count - 1; i >= 0; i--)
			{
				if (fireworks[i].IsFinished)
				{
					var index = i;
					fireworks[i] = Firework.Clone(fireworksStore[i]);
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			foreach (var firework in fireworks)
				firework.Draw(spriteBatch);
				
		}
	}
}
