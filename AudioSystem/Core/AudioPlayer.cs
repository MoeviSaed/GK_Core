using Game.Audio.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Audio
{
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {
        [SerializeField] private string addressableGroupLabel = "SFX"; // Лейбл группы Addressables (например, "SFX")
        
        [SerializeField] private string resourcesAudioFolderName;
        [SerializeField] private AudioClip[] clips;

        private Dictionary<string, AudioClip> _audioClipsMap;
        private List<AudioSourceParameters> _audioSources;

        public bool mute => lastBaseVolume == 0f;
        protected float lastBaseVolume;

        protected AudioManagerCore soundsManager;
        
        public void Initialize(AudioManagerCore soundsManager)
        {
            this.soundsManager = soundsManager;
        }

        // protected virtual void Awake()
        // {
        //     clips = Resources.LoadAll<AudioClip>(resourcesAudioFolderName);
        //
        //     _audioSources = new List<AudioSourceParameters>();
        //     _audioClipsMap = new Dictionary<string, AudioClip>();
        //
        //     for (int i = 0; i < clips.Length; i++)
        //     {
        //         if (!_audioClipsMap.ContainsKey(clips[i].name)) _audioClipsMap.Add(clips[i].name, clips[i]);
        //         else Debug.LogError(gameObject + " found audioClip with the same name " + clips[i].name);
        //     }
        // }
        
        protected virtual void Awake()
        {
            _audioSources = new List<AudioSourceParameters>();
            _audioClipsMap = new Dictionary<string, AudioClip>();

            LoadAudioClipsFromAddressables(addressableGroupLabel);
        }
        
        private void LoadAudioClipsFromAddressables(string label)
        {
            Addressables.LoadAssetsAsync<AudioClip>(label, OnAudioClipLoaded).Completed += OnAllClipsLoaded;
        }
        
        private void OnAudioClipLoaded(AudioClip clip)
        {
            if (!_audioClipsMap.ContainsKey(clip.name))
            {
                _audioClipsMap.Add(clip.name, clip);
            }
            else
            {
                Debug.LogError(gameObject + " found audioClip with the same name " + clip.name);
            }
        }
        
        private void OnAllClipsLoaded(AsyncOperationHandle<IList<AudioClip>> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"Successfully loaded {handle.Result.Count} audio clips.");
            }
            else
            {
                Debug.LogError("Failed to load audio clips from Addressables.");
            }
        }

        // Получение аудиоклипа по имени
        public AudioClip GetAudioClip(string clipName)
        {
            if (_audioClipsMap.TryGetValue(clipName, out var clip))
            {
                return clip;
            }

            Debug.LogWarning($"Audio clip '{clipName}' not found.");
            return null;
        }
        
        public AudioSource PlayAudio(string clipName, float volume, bool loop, float pitch, object obj = null)
       => PlayAudio(new AudioParameters() { clipName = clipName, volume = volume, loop = loop, pitch = pitch }, obj);

        public AudioSource PlayAudio(AudioParameters audioParameters, object obj = null)
        {
            if (_audioClipsMap.TryGetValue(audioParameters.clipName, out var clip))
            {
                var audioSource = GetAudioSource(audioParameters, clip);
                audioSource.clip = clip;
                audioSource.loop = audioParameters.loop;
                audioSource.volume = audioParameters.volume * lastBaseVolume;
                audioSource.pitch = audioParameters.pitch;
                audioSource.Play();
                return audioSource;
            }
            else
            {
                Debug.LogError("AudioClip " + audioParameters.clipName + " not found! Object is " + obj);
                return null;
            }
        }

        protected void OnBaseVolumeChanged(float value)
        {
            lastBaseVolume = value;
            for (int i = 0; i < _audioSources.Count; i++)
            {
                _audioSources[i].AudioSource.volume = _audioSources[i].AudioParameters.volume * value;
            }
        }

        private AudioSource GetAudioSource(AudioParameters audioParameters, AudioClip audioClip)
        {
            for (int i = 0; i < _audioSources.Count; i++)
            {
                if (_audioSources[i].AudioSource.isPlaying) continue;
                _audioSources[i] = new AudioSourceParameters()
                    { AudioSource = _audioSources[i].AudioSource, AudioClip = audioClip, AudioParameters = audioParameters };
                return _audioSources[i].AudioSource;
            }

            AudioSource temp = new GameObject("AudioSource " + _audioSources.Count, typeof(AudioSource)).GetComponent<AudioSource>();
            temp.transform.SetParent(transform);
            _audioSources.Add(new AudioSourceParameters()
            { AudioSource = temp, AudioClip = audioClip, AudioParameters = audioParameters });
            return temp;
        }

        private struct AudioSourceParameters
        {
            public AudioSource AudioSource;
            public AudioClip AudioClip;
            public AudioParameters AudioParameters;
        }
    }

    [Serializable]
    public struct AudioParameters
    {
        public string clipName;        
        [Range(0f, 1f)] public float volume;
        public bool loop;
        public float pitch;
    }
}