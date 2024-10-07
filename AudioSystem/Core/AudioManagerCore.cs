using System;
using UnityEngine;

namespace Game.Audio.Core
{
    public abstract class AudioManagerCore : MonoBehaviour
    {
        public MusicPlayer MusicPlayer => musicPlayer;
        [SerializeField] private MusicPlayer musicPlayer;

        public SFXPlayer SFXPlayer => sfxPlayer;
        [SerializeField] private SFXPlayer sfxPlayer;

        public float Sounds
        {
            get => sounds;
            set
            {
                sounds = value;
                OnSoundsChanged?.Invoke(value);
            }
        }
        protected abstract float sounds { get; set; }

        public float Music
        {
            get => music;
            set
            {
                music = value;
                OnMusicChanged?.Invoke(value);
            }
        }
        protected abstract float music { get; set; }

        public bool Vibro
        {
            get => vibro;
            set
            {
                vibro = value;
                OnVibroChanged?.Invoke(value);
            }
        }
        protected abstract bool vibro { get; set; }


        public Action<float> OnSoundsChanged;
        public Action<float> OnMusicChanged;
        public Action<bool> OnVibroChanged;

        protected virtual void Awake()
        {
            musicPlayer.Initialize(this);
            sfxPlayer.Initialize(this);
        }

        public abstract void VibratePop();

        public abstract void VibrateNope();
    }
}