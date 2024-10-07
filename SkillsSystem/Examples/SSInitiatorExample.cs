using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillsSystem.MVVMCore.Example;

public class SSInitiatorExample : MonoBehaviour
{
    private SSViewModelExample _viewModelExample;

    public ExampleSkills SelectedSkill;
    public bool LevelUp;

    protected void Awake()
    {
        _viewModelExample = SSViewModelExample.Instance;
    }

    protected void Start() => _viewModelExample.SwitchOnAllSkills();

    protected void OnDestroy() => _viewModelExample.SwitchOffAllSkills();


    private void Update()
    {
        if (LevelUp)
        {
            LevelUp = false;
            _viewModelExample.GetSkill((int)SelectedSkill).TryLevelUp();
        }
    }
}
