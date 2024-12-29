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

        public static int BrainStageValue
        {
            get => LoadInt(BrainStageLink, BrainStageDefault);
            set => SaveInt(BrainStageLink, value);
        }
        public const int BrainStageDefault = 0;
        private const string BrainStageLink = "BrainStage_Value";

        public static int CurrentLevelFlow
        {
            get => LoadInt(CurrentLevelFlowLink, CurrentLevelFlowDefault);
            set => SaveInt(CurrentLevelFlowLink, value);
        }
        public const int CurrentLevelFlowDefault = 0;
        private const string CurrentLevelFlowLink = "CurrentLevelFlow_Value";


        public static int GuessedWordValue
        {
            get => LoadInt(GuessedWordLink, GuessedWordDefault);
            set => SaveInt(GuessedWordLink, value);
        }
        public const int GuessedWordDefault = 0;
        private const string GuessedWordLink = "GuessedWord_Value";

        public static float TimeSpendValue
        {
            get => LoadFloat(TimeSpendLink, TimeSpendDefault);
            set => SaveFloat(TimeSpendLink, value);
        }
        public const float TimeSpendDefault = 0;
        private const string TimeSpendLink = "TimeSpend_Value";


        public static int CatcheedBonusValue
        {
            get => LoadInt(CatcheedBonusLink, CatcheedBonusDefault);
            set => SaveInt(CatcheedBonusLink, value);
        }
        public const int CatcheedBonusDefault = 0;
        private const string CatcheedBonusLink = "CatcheedBonus_Value";



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
        
        public static int EraserValue
        {
            get => LoadInt(EraserLink, EraserDefault);
            set => SaveInt(EraserLink, value);
        }
        public const int EraserDefault = 0;
        private const string EraserLink = "Eraser_Value";
        
        public static int LightningValue
        {
            get => LoadInt(LightningLink, LightningDefault);
            set => SaveInt(LightningLink, value);
        }
        public const int LightningDefault = 0;
        private const string LightningLink = "Lightning_Value";


        public static int GoalValue
        {
            get => LoadInt(GoalLink, GoalDefault);
            set => SaveInt(GoalLink, value);
        }
        public const int GoalDefault = 0;
        private const string GoalLink = "Goal_Value";

    }
}