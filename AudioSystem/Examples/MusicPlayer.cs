
namespace Game.Audio
{
    public class MusicPlayer : AudioPlayer, IAudioPlayer
    {
        private void Start()
        {
            soundsManager.OnMusicChanged += OnBaseVolumeChanged;
            OnBaseVolumeChanged(soundsManager.Music);
        }

        private void OnDestroy()
        {
            soundsManager.OnMusicChanged -= OnBaseVolumeChanged;
        }
    }
}