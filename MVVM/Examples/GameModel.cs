using System;
using MVVM.Core;
using MVVM.Data;
using UnityEngine;

namespace MVVM
{
    public class GameModel : MVVMModel
    {
        protected override void InitializeDatas()
        {
            var parameterTime = new ParameterTime();
            var parameterMoney = new ParameterMoney();
            var parameterCurrentLevel = new ParameterCurrentLevel();
            var parameterLives = new ParameterLives();
            var parameterGuessedWord = new ParameterGuessedWord();
            var parameterLevelFlow = new ParameterLevelFlow();
            var parameterGoal = new ParameterGoal();
            var parameterBrainStage = new ParameterBrainStage();
            var parameterBonusCatched = new ParameterBonusCatched();
            var parameterTimeSpend = new ParameterTimeSpend();
            var parameterLightning = new ParameterLightningHint();
            var parameterEraserHint = new ParameterEraserHint();

            DataMap.Add(typeof(ParameterTime), parameterTime);
            DataMap.Add(typeof(ParameterMoney), parameterMoney);
            DataMap.Add(typeof(ParameterCurrentLevel), parameterCurrentLevel);
            DataMap.Add(typeof(ParameterLives), parameterLives);
            DataMap.Add(typeof(ParameterGuessedWord), parameterGuessedWord);
            DataMap.Add(typeof(ParameterLevelFlow), parameterLevelFlow);
            DataMap.Add(typeof(ParameterGoal), parameterGoal);
            DataMap.Add(typeof(ParameterBrainStage), parameterBrainStage);
            DataMap.Add(typeof(ParameterBonusCatched), parameterBonusCatched);
            DataMap.Add(typeof(ParameterTimeSpend), parameterTimeSpend);
            DataMap.Add(typeof(ParameterLightningHint), parameterLightning);
            DataMap.Add(typeof(ParameterEraserHint), parameterEraserHint);
        }

        #region Default

        public class ParameterLives : Core.DataDefaultValueInt<ParameterLives>
        {
            public override int DefaultValue => GameDefaultData.DEFAULT_LIVES;
            protected override int Min => 0;
            protected override int Max => 3;

            public event Action OnMin;
            public ParameterLives() { }

            public void RemoveHeart()
            {
                Value -= 1;
                if (Value <= 0)
                {
                    OnMin?.Invoke();
                }

            }
            public void AddHeart() => Value += 1;
            public void SetValue(int value) => Value = value;
        }

        public class ParameterDefault : Core.DataDefaultValueFloat<ParameterDefault>
        {
            public override float DefaultValue => GameDefaultData.CONSTANT_VALUE;
            protected override float Min => 0;
            protected override float Max => 100f;

            public ParameterDefault() { }

            public void SetValue(float value) => Value = value;
        }

        #endregion

        #region Save
        public class ParameterCurrentLevel : Core.DataSaveValueInt<ParameterCurrentLevel>
        {
            protected override int Min => 0;
            protected override int Max => 9999999;
            protected override int data
            {
                get => GameSaveData.CurrentLevelValue;
                set => GameSaveData.CurrentLevelValue = value;
            }

            public ParameterCurrentLevel() { }

            public void Reset() => Value = GameSaveData.CurrentLevelDefault;

            public void Add(int value) => Value += value;

            public void SetValue(int value)
            {
                Debug.LogWarning("LevelIndexChanged to " + value);
                Value = value;
            }
        }

        public class ParameterBrainStage : Core.DataSaveValueInt<ParameterBrainStage>
        {
            protected override int Min => 0;
            protected override int Max => 9999999;
            protected override int data
            {
                get => GameSaveData.BrainStageValue;
                set => GameSaveData.BrainStageValue = value;
            }

            public ParameterBrainStage() { }

            public void Reset() => Value = GameSaveData.BrainStageDefault;

            public void Add(int value) => Value += value;

            public void SetValue(int value)
            {
                Debug.LogWarning("BrainStageValue to " + value);
                Value = value;
            }
        }

        public class ParameterLevelFlow : Core.DataSaveValueInt<ParameterLevelFlow>
        {
            protected override int Min => 0;
            protected override int Max => 2;
            protected override int data
            {
                get => GameSaveData.CurrentLevelFlow;
                set => GameSaveData.CurrentLevelFlow = value;
            }

            public ParameterLevelFlow() { }

            public void Reset() => Value = GameSaveData.CurrentLevelFlowDefault;

            public void Add(int value) => Value += value;

            public void SetValue(int value)
            {
                Debug.LogWarning("LevelFlowIndexChanged to " + value);
                Value = value;
            }
        }

        public class ParameterGuessedWord : Core.DataSaveValueInt<ParameterGuessedWord>
        {
            protected override int Min => 0;
            protected override int Max => 9999999;

            protected override int data
            {
                get => GameSaveData.GuessedWordValue;
                set => GameSaveData.GuessedWordValue = value;
            }

            public ParameterGuessedWord()
            {
            }

            public void Reset() => Value = GameSaveData.GuessedWordDefault;

            public void Add(int value) => Value += value;
            
            public void SetValue(int value)
            {
                Value = value;
            }
        }

        public class ParameterTimeSpend : Core.DataSaveValueFloat<ParameterTimeSpend>
        {
            protected override float Min => 0;
            protected override float Max => 9999999;

            protected override float data
            {
                get => GameSaveData.TimeSpendValue;
                set => GameSaveData.TimeSpendValue = value;
            }

            public ParameterTimeSpend()
            {
            }

            public void Reset() => Value = GameSaveData.TimeSpendDefault;

            public void Add(float value) => Value += value;

            public void SetValue(float value)
            {
                Value = value;
            }
        }

        public class ParameterBonusCatched : Core.DataSaveValueInt<ParameterBonusCatched>
        {
            protected override int Min => 0;
            protected override int Max => 9999999;

            protected override int data
            {
                get => GameSaveData.CatcheedBonusValue;
                set => GameSaveData.CatcheedBonusValue = value;
            }

            public ParameterBonusCatched()
            {
            }

            public void Reset() => Value = GameSaveData.CatcheedBonusDefault;

            public void Add(int value) => Value += value;

            public void SetValue(int value)
            {
                Value = value;
            }
        }

        public class ParameterTime : Core.DataSaveValueInt<ParameterTime>
        {
            protected override int Min => 0;
            protected override int Max => 9999999;
            protected override int data
            {
                get => GameSaveData.TimeValue;
                set => GameSaveData.TimeValue = value;
            }

            public ParameterTime() { }

            public void Reset() => Value = GameSaveData.TimeDefault;

            public void Add(int value) => Value += value;

            public void SetValue(int value) => Value = value;
            public bool TrySpend(int value)
            {
                if (Value >= value)
                {
                    Value -= value;
                    return true;
                }
                return false;
            }
        }

        public class ParameterGoal : Core.DataSaveValueInt<ParameterGoal>
        {
            protected override int Min => 0;
            protected override int Max => 9999999;
            protected override int data
            {
                get => GameSaveData.GoalValue;
                set => GameSaveData.GoalValue = value;
            }

            public ParameterGoal() { }

            public void Reset() => Value = GameSaveData.GoalDefault;

            public void Add(int value) => Value += value;

            public void SetValue(int value) => Value = value;
        }

        public class ParameterMoney : Core.DataSaveValueInt<ParameterMoney>
        {
            protected override int Min => 0;
            protected override int Max => 9999999;
            protected override int data
            {
                get => GameSaveData.MoneyValue;
                set => GameSaveData.MoneyValue = value;
            }

            public ParameterMoney() { }

            public void Reset() => Value = GameSaveData.MoneyDefault;

            public void SetValue(int value) => Value = value;

            public void Add(int value) => Value += value;

            public bool TrySpend(int value)
            {
                if (Value >= value)
                {
                    Value -= value;
                    return true;
                }
                else return false;
            }
        }
        
        public class ParameterEraserHint : Core.DataSaveValueInt<ParameterEraserHint>
        {
            protected override int Min => 0;
            protected override int Max => 9999999;
            protected override int data
            {
                get => GameSaveData.EraserValue;
                set => GameSaveData.EraserValue = value;
            }

            public ParameterEraserHint() { }

            public void Reset() => Value = GameSaveData.EraserDefault;

            public void SetValue(int value) => Value = value;

            public void Add(int value) => Value += value;

            public bool TrySpend(int value)
            {
                if (Value >= value)
                {
                    Value -= value;
                    return true;
                }
                else return false;
            }
        }
        
        public class ParameterLightningHint : Core.DataSaveValueInt<ParameterLightningHint>
        {
            protected override int Min => 0;
            protected override int Max => 9999999;
            protected override int data
            {
                get => GameSaveData.LightningValue;
                set => GameSaveData.LightningValue = value;
            }

            public ParameterLightningHint() { }

            public void Reset() => Value = GameSaveData.LightningDefault;

            public void SetValue(int value) => Value = value;

            public void Add(int value) => Value += value;

            public bool TrySpend(int value)
            {
                if (Value >= value)
                {
                    Value -= value;
                    return true;
                }
                else return false;
            }
        }

        #endregion



    }
}