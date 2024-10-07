using System.Collections.Generic;

namespace SkillsSystem.MVVMCore.Example
{
    public class SSContainerExample : SSContainer
    {
        public SSContainerExample(SSModel model) : base(model) { }

        protected override void InitializeSkills(Dictionary<int, SSSkill> skillsMap)
        {
            skillsMap.Add((int)ExampleSkills.SkillExample, new SSSkillExample(Model));
        }
    }
}

