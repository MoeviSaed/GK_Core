using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{
    public class SwitchAfterEvent : GameEventsMonoBehaviour
    {
        [SerializeField] private GameObject[] go;
        [Space]
        [SerializeField] private bool switchActive;
        [Space]
        [SerializeField] private bool afterEnd;
        [SerializeField] private bool afterStart;
        [Space]
        [SerializeField] private bool pauseBeforeEvent;
        [SerializeField] private float pauseTime;
        
        private IGameEvents _gameEvents;
        
        protected override void OnGameStart()
        {
            if (afterStart) 
            {
                if (pauseBeforeEvent) StartCoroutine(Pause());
                else Switch();
            }
        }
        protected override void OnGameEnd()
        {
            if (afterEnd)
            {
                if (pauseBeforeEvent) StartCoroutine(Pause());
                else Switch();
            }
        }

        private IEnumerator Pause()
        {
            yield return new WaitForSeconds(pauseTime);
            Switch();
        }

        protected void Switch()
        {
            for (int i = 0; i < go.Length; i++)
                go[i].SetActive(switchActive);
        }
    }
}