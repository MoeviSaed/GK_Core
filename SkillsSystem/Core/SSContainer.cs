using System.Collections.Generic;

namespace SkillsSystem.MVVMCore
{
    public abstract class SSContainer
    {
        protected readonly Dictionary<int, SSSkill> skillsMap;

        protected readonly SSModel Model;

        public SSContainer(SSModel model)
        {
            Model = model;
            skillsMap = new Dictionary<int, SSSkill>();
            InitializeSkills(skillsMap);
        }

        protected abstract void InitializeSkills(Dictionary<int, SSSkill> skillsMap);

        public SSSkill GetSkill(int skillId) => skillsMap[skillId];

        public void SwitchOnAllSkills()
        {
            foreach (KeyValuePair<int, SSSkill> kvp in skillsMap)
            {
                skillsMap[kvp.Key].IsAcitve = true;
            }
        }

        public void SwitchOffAllSkills()
        {
            foreach (KeyValuePair<int, SSSkill> kvp in skillsMap)
            {
                skillsMap[kvp.Key].IsAcitve = false;
            }
        }
    }
}