using MVVM;
using MVVM.Core;

namespace MVVM.Core
{
    public class GameVM : MVVMViewModel<GameModel>
    {
        public static GameVM Instance => _instance ??= new GameVM();

        private static GameVM _instance;

        protected override GameModel CreateModel() => new GameModel();
    }
}