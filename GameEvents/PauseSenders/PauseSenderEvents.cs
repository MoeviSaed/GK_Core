using UnityEngine;

namespace GameEvents.PauseSenders
{
    public class PauseSenderEvents : PauseSender
    {
        [Header("Events Module")]
        [SerializeField] private bool onEnableValue;
        [Space]
        [SerializeField] private bool onDisableValue;

        private void OnEnable()
        {
            if (onEnableValue) SendEnablePause();
            else SendDisablePause();
        }

        private void OnDisable()
        {
            if (onDisableValue) SendEnablePause();
            else SendDisablePause();
        }
    }
}