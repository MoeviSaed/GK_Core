namespace SkillsSystem.MVVMCore.Example
{
    public class SSViewModelExample : SSViewModel<SSDataDefaultExample, SSContainerExample>
    {
        private SSViewModelExample() { }

        public static SSViewModelExample Instance
        {
            get { return _instance ?? (_instance = new SSViewModelExample()); }
        }
        private static SSViewModelExample _instance;

        protected override SSDataDefaultExample CreateData() => new SSDataDefaultExample();
        protected override SSContainerExample CreateContainer(SSModel model) => new SSContainerExample(model);
    }
}