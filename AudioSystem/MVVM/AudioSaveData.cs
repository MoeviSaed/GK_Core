using MVVM.Data;


namespace MVVM.Audio.Data
{
    public class AudioSaveData : MVVMSaveData
    {
        public static float SFXVolumeValue
        {
            get => LoadFloat(SFXVolumeLink, SFXVolumeDefault);
            set => SaveFloat(SFXVolumeLink, value);
        }
        public const float SFXVolumeDefault = 1f;
        private const string SFXVolumeLink = "SFX_Volume_Value";

        public static float MusicVolumeValue
        {
            get => LoadFloat(MusicVolumeLink, MusicVolumeDefault);
            set => SaveFloat(MusicVolumeLink, value);
        }
        public const float MusicVolumeDefault = 1f;
        private const string MusicVolumeLink = "Music_Volume_Value";

        public static bool VibrationValue
        {
            get => LoadBool(VibrationValueLink, VibrationValueDefault);
            set => SaveBool(VibrationValueLink, value);
        }
        public const bool VibrationValueDefault = true;
        private const string VibrationValueLink = "SFX_Volume_Value";
    }
}
