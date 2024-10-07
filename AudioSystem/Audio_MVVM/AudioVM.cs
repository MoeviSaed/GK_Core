using MVVM.Core;


namespace MVVM.Audio
{
    public class AudioVM : MVVMViewModel<AudioModel>
    {
        public static AudioVM Instance
        {
            get { return _instance ??= new AudioVM(); }
        }
        private static AudioVM _instance;

        protected override AudioModel CreateModel() => new AudioModel();
    }
}
