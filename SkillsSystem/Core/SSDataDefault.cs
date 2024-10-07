namespace SkillsSystem.MVVMCore.Data
{
    public abstract class SSDataDefault : SSData
    {
        private SkillSave[] _skills;

        protected override void Save(SkillSave[] save) => _skills = save;

        protected override SkillSave[] Load() => _skills == null ? DefaultSkills() : _skills;

        public override void ClearSaves() => _skills = null;
    }
}

