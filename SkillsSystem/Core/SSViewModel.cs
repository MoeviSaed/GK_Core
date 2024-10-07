using SkillsSystem.MVVMCore.Data;
using System;

namespace SkillsSystem.MVVMCore
{
    public abstract class SSViewModel<T, Y> where T : SSData where Y : SSContainer
    {
        public SSViewModel()
        {
            Model = new SSModel(CreateData());
            Container = CreateContainer(Model);

            Model.OnSkillSavesChanged += () => { OnSkillsChanged?.Invoke(); };
        }

        protected readonly SSModel Model;
        protected readonly Y Container;

        protected abstract T CreateData();
        protected abstract Y CreateContainer(SSModel model);

        public event Action OnSkillsChanged;
        
        public SSSkill GetSkill(int skillId) => Container.GetSkill(skillId);
        public void SwitchOnAllSkills() => Container.SwitchOnAllSkills();
        public void SwitchOffAllSkills() => Container.SwitchOffAllSkills();

        public void ClearSkills()
        {
            Model.ClearSaves();
        }
    }
}
