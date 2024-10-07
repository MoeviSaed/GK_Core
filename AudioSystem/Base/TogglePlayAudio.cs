using UnityEngine.UI;

namespace Game.Audio
{
    public class TogglePlayAudio : PlayAudio
    {
        private Toggle _toggle;

        protected override void Awake()
        {
            base.Awake();
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(bool value) => Play();
    }
}
