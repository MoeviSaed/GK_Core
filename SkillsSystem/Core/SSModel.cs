using SkillsSystem.MVVMCore.Data;
using System;
using System.Collections.Generic;

namespace SkillsSystem.MVVMCore
{
    public class SSModel
    {
        public SkillSave[] SkillSaves => _skillSaves.ToArray();
        private List<SkillSave> _skillSaves;

        private SkillSave[] data
        {
            get => _data.SkillSaves;
            set => _data.SkillSaves = value;
        }

        private readonly SSData _data;

        public SSModel(SSData data)
        {
            _data = data;
            _skillSaves = new List<SkillSave>(this.data);
        }

        public Action OnSkillSavesChanged;

        public void ClearSaves()
        {
            _data.ClearSaves();
            _skillSaves = new List<SkillSave>(data);
            OnSkillSavesChanged?.Invoke();
        }

        public int GetSkillLevel(int skillId)
        {
            if (TryFindSkill(skillId, out int index)) return _skillSaves[index].Level;   
            else return 0;
        }

        public void LevelUp(int skillId)
        {
            if (TryFindSkill(skillId, out int index))
            {
                var skill = _skillSaves[index];
                skill.Level++;
                _skillSaves[index] = skill;
            }
            else _skillSaves.Add(new SkillSave(skillId, 1));

            SaveTempToData();
        }

        private bool TryFindSkill(int skillId, out int index)
        {
            index = 0;
            for (int i = 0; i < _skillSaves.Count; i++)
            {
                if (_skillSaves[i].SkillId == skillId)
                {
                    index = i;
                    return true;
                }
            }
            return false;
        }

        public void SaveTempToData()
        {
            data = _skillSaves.ToArray();
            OnSkillSavesChanged?.Invoke();
        }
    }


    [System.Serializable]
    public struct SkillSave
    {
        public SkillSave(int skillId, int level)
        {
            SkillId = skillId;
            Level = level;
        }

        public int SkillId;
        public int Level;
    }
}

