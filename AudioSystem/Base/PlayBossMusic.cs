using Game.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBossMusic : MonoBehaviour
{
    [SerializeField] private PlayAudio basePlayer;
    [SerializeField] private string[] clipNames;

    private void Start()
    {
        basePlayer.Play(clipNames[Random.Range(0, clipNames.Length)]);
    }
}
