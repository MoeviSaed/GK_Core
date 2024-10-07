using UnityEngine;
using UnityEngine.UI;

namespace Game.Audio
{
    public class MuteCheckBoxes : MonoBehaviour
    {
        private enum AudioType
        {
            SFX = 0,
            Music = 1
        }

        [SerializeField] private AudioType _audioType;

        private IAudioPlayer _audioPlayer;
        private Toggle _toggle;

        private void Construct(SFXPlayer sfxPlayer, MusicPlayer musicPlayer)
        {
            switch (_audioType)
            {
                case AudioType.SFX:
                    _audioPlayer = sfxPlayer;
                    break;
                case AudioType.Music:
                    _audioPlayer = musicPlayer;
                    break;
            }
        }
    }
}