namespace SkillsSystem.MVVMCore.Data
{
    public abstract class SSData
    {
        public SkillSave[] SkillSaves
        {
            get => Load();
            set => Save(value);
        }

        public abstract void ClearSaves();

        protected virtual SkillSave[] DefaultSkills() => new SkillSave[] { };

        protected abstract void Save(SkillSave[] save);

        protected abstract SkillSave[] Load();
    }
}
