using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameEvents
{
    public class GameEventsSender : MonoBehaviour, IGameEvents, IGameEventsSender
    {
        private GameEventsSender()
        {
            Instance = this;
        }

        public static IGameEvents Instance;

        public event Action OnGameStart;
        public event Action OnGameEnd;
        public event Action OnGamePredStart;
        public event Action<bool> OnGamePause;

        public bool Pause
        {
            get => _pause;
            private set
            {
                if (_pause != value)
                {
                    _pause = value;
                    OnGamePause?.Invoke(_pause);
                }
            }
        }
        private bool _pause;

        private void Awake()
        {
            _pause = false;   
        }

        protected List<object> PauseSources
        {
            get
            {
                if (_pauseSources == null) _pauseSources = new List<object>();
                return _pauseSources;
            }
            private set => _pauseSources = value;
        }
        private List<object> _pauseSources;

        public void InvokeStart() => OnGameStart?.Invoke();
        public void InvokePredStart() => OnGamePredStart?.Invoke();
        public void InvokeEnd() => OnGameEnd?.Invoke();

        public void EnablePause(object source)
        {
            if (!PauseSources.Contains(source)) PauseSources.Add(source);
            InvokePause();
        }

        public void DisablePause(object source)
        {
            if (PauseSources.Contains(source)) PauseSources.Remove(source);
            InvokePause();
        }

        public void InvokePause()
        {
            Pause = PauseSources.Count > 0;
        }
    }
}