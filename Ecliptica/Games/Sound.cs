using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecliptica.Games
{
	internal class Sound
	{
		//Music
		public static Song MusicTheme { get; private set; }
		public static Song TitleScreen { get; private set; }
		public static Song MenuScreen { get; private set; }
		public static Song GameOver { get; private set; }
		public static Song LevelComplete { get; private set; }
		public static Song GameEnd { get; private set; }

		//Sound Effects
		public static SoundEffect Shoot { get; private set; }
		public static SoundEffect Explosion { get; private set; }
		public static SoundEffect PlayerKilled { get; private set; }

		/// <summary>
		/// Method to load all the sounds
		/// </summary>
		/// <param name="content"></param>
		public static void Load(ContentManager content)
		{
			MusicTheme = content.Load<Song>("Audio/rain-of-lasers-level-theme");
			TitleScreen = content.Load<Song>("Audio/sky-fire-title-screen");
			MenuScreen = content.Load<Song>("Audio/brave-pilots-menu-screen");
			GameOver = content.Load<Song>("Audio/defeated-game-over-tune");
			LevelComplete = content.Load<Song>("Audio/victory-tune");
			GameEnd = content.Load<Song>("Audio/epic-end");

			Shoot = content.Load<SoundEffect>("Audio/sound-laser");
			Explosion = content.Load<SoundEffect >("Audio/falling-hit");
			PlayerKilled = content.Load<SoundEffect >("Audio/arcade-retro-game-over");
		}

		/// <summary>
		/// Method to play music
		/// </summary>
		/// <param name="song"></param>
		public static void PlayMusic(Song song)
		{
			MediaPlayer.Play(song);
			MediaPlayer.IsRepeating = true;
		}

		/// <summary>
		/// Method to stop music
		/// </summary>
		public static void StopMusic()
		{
			MediaPlayer.Stop();
		}

		/// <summary>
		/// Method to play sound effects
		/// </summary>
		/// <param name="soundEffect"></param>
		public static void PlaySound(SoundEffect soundEffect, float volume)
		{ 
			SetSoundEffectVolume(volume);

			soundEffect.Play();
		}

		/// <summary>
		/// Method to set the volume of the sound effects
		/// </summary>
		/// <param name="volume"></param>
		public static void SetSoundEffectVolume(float volume)
		{
			SoundEffect.MasterVolume = volume;
		}
	}
}
