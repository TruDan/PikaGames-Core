﻿using PikaGames.Games.Core.UI.Controls;

namespace GameLauncher.Scenes.Options
{
    public class AudioOptionsScene : OptionsScene
    {
        private UiControlGroup _controls;

        public AudioOptionsScene(OptionsMenuScene optionsMenu) : base(optionsMenu, "Audio")
        {
            
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _controls = new UiControlGroup(Container, 0, 0);
            _controls.Width = (int)Game.VirtualSize.Y - 100;

            _controls.AddSlider("Music", (value) => Game.SoundManager.MusicVolume = value);
            _controls.AddSlider("SFX", (value) => Game.SoundManager.SfxVolume = value);
        }
    }
}
