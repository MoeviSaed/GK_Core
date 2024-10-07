using UnityEngine;

namespace Game.Audio
{
    public class TriggerPlayAudio : PlayAudio
    {
        [SerializeField] private string _tagForTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if (_tagForTrigger == other.tag)
            {
                Play();
            }
        }
    }
}