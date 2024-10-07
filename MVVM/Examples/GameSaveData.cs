using MVVM.Data;

namespace MVVM.Data
{
    public class GameSaveData : MVVMSaveData
    {
        public static int SaveValue
        {
            get => LoadInt(SaveLink, SaveDefault);
            set => SaveInt(SaveLink, value);
        }
        public const int SaveDefault = 0;
        private const string SaveLink = "Save_Value";
        
        public static int CurrentLevelValue
        {
            get => LoadInt(CurrentLevelLink, CurrentLevelDefault);
            set => SaveInt(CurrentLevelLink, value);
        }
        public const int CurrentLevelDefault = 0;
        private const string CurrentLevelLink = "CurrentLevel_Value";
        
        public static int CurrentLevelFlow
        {
            get => LoadInt(CurrentLevelFlowLink, CurrentLevelFlowDefault);
            set => SaveInt(CurrentLevelFlowLink, value);
        }
        public const int CurrentLevelFlowDefault = 0;
        private const string CurrentLevelFlowLink = "CurrentLevelFlow_Value";

        public static int CurrentChapterValue
        {
            get => LoadInt(CurrentChapterLink, CurrentChapterDefault);
            set => SaveInt(CurrentChapterLink, value);
        }
        public const int CurrentChapterDefault = 0;
        private const string CurrentChapterLink = "CurrentChapter_Value";
        
        public static int TimeValue
        {
            get => LoadInt(TimeLink, TimeDefault);
            set => SaveInt(TimeLink, value);
        }
        public const int TimeDefault = 0;
        private const string TimeLink = "Time_Value";
        
        public static int MoneyValue
        {
            get => LoadInt(MoneyLink, MoneyDefault);
            set => SaveInt(MoneyLink, value);
        }
        public const int MoneyDefault = 0;
        private const string MoneyLink = "Money_Value";
        
        public static int GoalValue
        {
            get => LoadInt(GoalLink, GoalDefault);
            set => SaveInt(GoalLink, value);
        }
        public const int GoalDefault = 0;
        private const string GoalLink = "Goal_Value";

    }
}