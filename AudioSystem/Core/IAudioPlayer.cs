using UnityEngine;

namespace Game.Audio
{
    public interface IAudioPlayer
    {
        public bool mute { get; }
        public AudioSource PlayAudio(string clipName, float volume, bool loop, float pitch, object obj = null);
        public AudioSource PlayAudio(AudioParameters audioParameters, object obj = null);
    }
}