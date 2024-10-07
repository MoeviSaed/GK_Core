using UnityEngine;
using UnityEngine.UI;

namespace GameEvents.PauseSenders
{
    public class PauseSenderButton : PauseSender
    {
        [Header("Button Module")]
        [SerializeField] private Button senderButton;
        [SerializeField] private bool valueToSend;

        protected override void Awake()
        {
            base.Awake();
            senderButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (valueToSend) SendEnablePause();
            else SendDisablePause();
        }
    }
}