using UnityEngine;

namespace Game.Audio
{
    public class SFXPlayer : AudioPlayer, IAudioPlayer
    { 
        private void Start()
        {
            soundsManager.OnSoundsChanged += OnBaseVolumeChanged;
            OnBaseVolumeChanged(soundsManager.Sounds);
        }

        private void OnDestroy()
        {
            soundsManager.OnSoundsChanged -= OnBaseVolumeChanged;
        }
    }
}