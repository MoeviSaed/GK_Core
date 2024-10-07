using SkillsSystem.MVVMCore.Data;

namespace SkillsSystem.MVVMCore.Example
{
    public class SSDataJsonPlayerPrefsExample : SSDataJsonPlayerPrefs
    {
        protected override string SavePath => "SSDJPP_Example";

        protected override SkillSave[] DefaultSkills()
        {
            return base.DefaultSkills();
        }
    }
}