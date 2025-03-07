﻿using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Ecliptica.Arts
{
    internal class Sounds
    {
		#region Properties
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
		public static SoundEffect BonusSound { get; private set; }
		public static SoundEffect ButtonSound { get; private set; }
		#endregion

		#region Methods
		/// <summary>
		/// Method to load the sounds
		/// </summary>
		/// <param name="content"></param>
		public static void Load(ContentManager content)
        {
            //Musics
			TitleScreen = content.Load<Song>("Audio/sky-fire-title-screen");
			MenuScreen = content.Load<Song>("Audio/brave-pilots-menu-screen");
			MusicTheme = content.Load<Song>("Audio/rain-of-lasers-level-theme");
            GameOver = content.Load<Song>("Audio/defeated-game-over-tune");
            LevelComplete = content.Load<Song>("Audio/victory-tune");
            GameEnd = content.Load<Song>("Audio/epic-end");

			//Sound Effects
			Shoot = content.Load<SoundEffect>("Audio/sound-laser");
            Explosion = content.Load<SoundEffect>("Audio/falling-hit");
            PlayerKilled = content.Load<SoundEffect>("Audio/arcade-retro-game-over");
			BonusSound = content.Load<SoundEffect>("Audio/bonus-sound");
			ButtonSound = content.Load<SoundEffect>("Audio/button-sound");
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
		/// <param name="volume"></param>
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
		#endregion
	}
}
