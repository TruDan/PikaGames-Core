using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace PikaGames.Games.Core.Sound
{
    public class SoundManager
    {
        public float MusicVolume
        {
            get { return _musicVolume; }
            set
            {
                _musicVolume = value;
                MediaPlayer.Volume = _musicVolume;
            }
        }

        public float SfxVolume { get; set; } = 1f;

        private bool _soundEnabled = true;
        private float _musicVolume = 0f;

        public SoundManager()
        {
            MediaPlayer.IsRepeating = true;
        }

        public void PlayBackground(Song song)
        {
            if (_soundEnabled && song != null)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                MediaPlayer.Volume = MusicVolume;
            }
        }

        public void Play(SoundEffect soundEffect)
        {
            if (_soundEnabled && soundEffect != null)
            {
                var seIntance = soundEffect.CreateInstance();
                seIntance.Volume = SfxVolume;
                seIntance.Play();
            }
        }

    }
}
