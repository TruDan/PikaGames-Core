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
        private static float _musicVolume = 1f;
        private static float _soundEffectVolume = 1f;

        private static bool _soundEnabled = true;


        public void Play(SoundEffect soundEffect)
        {
            if (_soundEnabled && soundEffect != null)
            {
                var seIntance = soundEffect.CreateInstance();
                seIntance.Volume = _soundEffectVolume;
                seIntance.Play();
            }
        }

    }
}
