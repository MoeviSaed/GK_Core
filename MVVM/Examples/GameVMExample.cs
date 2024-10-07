using MVVM.Core;

namespace MVVM.Example
{
    public class GameVMExample : MVVMViewModel<GameModel>
    {
        public static GameVMExample Instance
        {
            get { return _instance ?? (_instance = new GameVMExample()); }
        }
        private static GameVMExample _instance;

        protected override GameModel CreateModel() => new GameModel();
    }
}