using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace PikaGames.Games.Core.Sound
{
    public class SoundManager
    {
        public float MusicVolume { get; set; } = 1f;
        public float SfxVolume { get; set; } = 1f;

        private bool _soundEnabled = true;


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
