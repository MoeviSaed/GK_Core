using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Audio
{
    public class OnEnablePlayAudio : PlayAudio
    {
        private void OnEnable() => Play();
    }
}