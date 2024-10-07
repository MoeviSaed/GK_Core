using UnityEngine;
using UnityEngine.UI;

namespace Game.Audio
{
    public class ButtonPlayerAudio : PlayAudio
    {
        [SerializeField] private Button _button;
        
        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick() => Play();
    }
}