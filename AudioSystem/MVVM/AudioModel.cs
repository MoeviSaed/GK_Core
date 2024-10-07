using MVVM.Audio.Data;
using MVVM.Core;

namespace MVVM.Audio
{
    public class AudioModel : MVVMModel
    {
        protected override void InitializeDatas()
        {
            var parameterSFXVolume = new ParameterSFXVolume();
            var parameterMusicVolume = new ParameterMusicVolume();
            var parameterVibration = new ParameterVibration();

            DataMap.Add(typeof(ParameterSFXVolume), parameterSFXVolume);
            DataMap.Add(typeof(ParameterMusicVolume), parameterMusicVolume);
            DataMap.Add(typeof(ParameterVibration), parameterVibration);
        }


        #region Save

        public class ParameterSFXVolume : Core.DataSaveValueFloat<ParameterSFXVolume>
        {
            protected override float data
            {
                get => AudioSaveData.SFXVolumeValue;
                set => AudioSaveData.SFXVolumeValue = value;
            }

            protected override float Min => 0;
            protected override float Max => 1f;

            public ParameterSFXVolume() { }

            public void SetValue(float value) => Value = value;
        }

        public class ParameterMusicVolume : Core.DataSaveValueFloat<ParameterMusicVolume>
        {
            protected override float data
            {
                get => AudioSaveData.MusicVolumeValue;
                set => AudioSaveData.MusicVolumeValue = value;
            }
            protected override float Min => 0;
            protected override float Max => 1f;

            public ParameterMusicVolume() { }

            public void SetValue(float value) => Value = value;
        }

        public class ParameterVibration : Core.DataSaveValueBool<ParameterVibration>
        {
            protected override bool data
            {
                get => AudioSaveData.VibrationValue;
                set => AudioSaveData.VibrationValue = value;
            }

            public ParameterVibration() { }
        }
        #endregion
    }
}