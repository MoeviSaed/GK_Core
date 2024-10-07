using UnityEngine;

namespace SkillsSystem.MVVMCore.Example
{
    public class SSSkillExample : SSSkill
    {
        public SSSkillExample(SSModel model) : base(model) { }

        public override int SkillID => (int)ExampleSkills.SkillExample;
        public override int MaxLevel => 1000;
        public override int NextLevelCost => BaseCost + (CostPerLevel * SkillLevel);
        public override bool CanOpen => true;
        public override bool CanLevelUp => SkillLevel < MaxLevel;


        private const int BaseCost = 500;
        private const int CostPerLevel = 250;
        private const int BonusPerLevel = 5;

        protected override void SwitchOnSkill()
        {
            Debug.Log("Example Skill: " + SkillID + ". Level: " + SkillLevel + ". Bonus: " + BonusPerLevel * SkillLevel + ". Next level cost: " + NextLevelCost);
            Debug.Log("Activated");
        }

        protected override void SwitchOffSkill()
        {
            Debug.Log("Example Skill: " + SkillID + ". Level: " + SkillLevel + ". Bonus: " + BonusPerLevel * SkillLevel + ". Next level cost: " + NextLevelCost);
            Debug.Log("Disabled");
        }
    }

    public enum ExampleSkills
    {
        SkillExample = 0
    }
}