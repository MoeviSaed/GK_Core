using Game.Audio.Core;
using System;
using UnityEngine;

namespace Game.Audio
{
    public class PlayAudio : MonoBehaviour
    {
        private enum AudioType
        {
            SFX = 0,
            Music = 1
        }

        protected bool AudioPlayerMute => AudioPlayer.mute;

        [SerializeField] private AudioManagerCore audioManager;
        [Space]
        [SerializeField] private AudioType audioType;
        [SerializeField] private AudioParameters audioParameters;
        [SerializeField] private bool switchOffOnDestroy = true;
        [SerializeField] private bool tryFindAudioManager;
        
        protected AudioSource CurrentAudioSource;
        protected IAudioPlayer AudioPlayer;

        protected virtual void Awake()
        {
            if (tryFindAudioManager) audioManager = FindObjectOfType<AudioManagerCore>();
            if (audioManager) Construct(audioManager);
        }
        protected virtual void OnDestroy()
        {
            if (switchOffOnDestroy) Stop();
        }

        public void Construct(AudioManagerCore audioManager)
        {
            this.audioManager = audioManager;
            AudioPlayer = audioType switch
            {
                AudioType.Music => audioManager.MusicPlayer,
                AudioType.SFX => audioManager.SFXPlayer,
                _ => AudioPlayer
            };
        }

        public void Play()
        {
            CurrentAudioSource = AudioPlayer.PlayAudio(audioParameters, this);
        }

        public void Play(string customAudioName)
        {
            var param = audioParameters;
            param.clipName = customAudioName;
            CurrentAudioSource = AudioPlayer.PlayAudio(param, this);
        }


        public void Stop()
        {
            if (CurrentAudioSource) CurrentAudioSource.Stop();
        }
    }
}