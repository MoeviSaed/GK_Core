using UnityEngine;

namespace SkillsSystem.MVVMCore.Data
{
    public abstract class SSDataJsonPlayerPrefs : SSData
    {
        protected abstract string SavePath { get; }

        protected override void Save(SkillSave[] save)
        {
            string json = JsonUtility.ToJson(new SaveJson(save));
            PlayerPrefs.SetString(SavePath, json);
        }

        protected override SkillSave[] Load()
        {
            string json = PlayerPrefs.GetString(SavePath, GetDefaultPackJSON());
            SaveJson saveJson = JsonUtility.FromJson<SaveJson>(json);
            return saveJson.SkillSaves;
        }

        protected string GetDefaultPackJSON()
        {
            SaveJson saveJson = new SaveJson();
            saveJson.SkillSaves = DefaultSkills();
            return JsonUtility.ToJson(saveJson);
        }

        public override void ClearSaves() => Save(null);


        [System.Serializable]
        public struct SaveJson
        {
            public SaveJson(SkillSave[] skillSaves) => SkillSaves = skillSaves;

            public SkillSave[] SkillSaves;
        }
    }
}

