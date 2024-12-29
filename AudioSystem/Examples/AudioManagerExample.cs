using Game.Audio.Core;
using MVVM.Audio;

namespace Game.Audio
{
    public class AudioManagerExample : AudioManagerCore
    {
        protected override float sounds 
        {
            get
            {
                if (_sfxVolume == null) _sfxVolume = AudioVM.Instance.Data<AudioModel.ParameterSFXVolume>();
                return _sfxVolume.Value;
            }
            set
            {
                if (_sfxVolume == null) _sfxVolume = AudioVM.Instance.Data<AudioModel.ParameterSFXVolume>();
                _sfxVolume.SetValue(value);
            }
        }

        protected override float music 
        {
            get
            {
                if (_musicVolume == null) _musicVolume = AudioVM.Instance.Data<AudioModel.ParameterMusicVolume>();
                return _musicVolume.Value;
            }
            set
            {
                if (_musicVolume == null) _musicVolume = AudioVM.Instance.Data<AudioModel.ParameterMusicVolume>();
                _musicVolume.SetValue(value);
            }
        }

        protected override bool vibro
        {
            get
            {
                if (_vibrationValue == null) _vibrationValue = AudioVM.Instance.Data<AudioModel.ParameterVibration>();
                return _vibrationValue.Value;
            }
            set
            {
                if (_vibrationValue == null) _vibrationValue = AudioVM.Instance.Data<AudioModel.ParameterVibration>();
                _vibrationValue.SetValue(value);
            }
        }

        private AudioVM _gameVM;

        private AudioModel.ParameterSFXVolume _sfxVolume;
        private AudioModel.ParameterMusicVolume _musicVolume;
        private AudioModel.ParameterVibration _vibrationValue;


        public static AudioManagerExample Instance;
        protected override void Awake()
        {
            base.Awake();
            _gameVM = AudioVM.Instance;
            _sfxVolume = _gameVM.Data<AudioModel.ParameterSFXVolume>();
            _musicVolume = _gameVM.Data<AudioModel.ParameterMusicVolume>();
            _vibrationValue = _gameVM.Data<AudioModel.ParameterVibration>();
            
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public override void VibratePop()
        {
            // if (Vibro) global::Vibration.VibratePop();
        }

        public override void VibrateNope()
        {
            // if (Vibro) global::Vibration.VibrateNope();
        }
    }
}