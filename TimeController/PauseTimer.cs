using GameEvents;
using System;
using UnityEngine;

namespace GTC
{
    public class PauseTimer : Timer, IDisposable
    {
        private IGameEvents _gameEvents;

        protected override bool pause
        {
            get { return base.pause || _gamePause; }
            set => base.pause = value;
        }
        private bool _gamePause;

        public PauseTimer(GameTimeController gameTimeController, IGameEvents gameEvents) : base(gameTimeController)
        {
            _gameEvents = gameEvents;
            _gameEvents.OnGamePause += OnGamePause;
            _gamePause = gameEvents.Pause;
        }

        public virtual void Dispose()
        {
            _gameEvents.OnGamePause -= OnGamePause;
        }

        private void OnGamePause(bool value)
        {
            _gamePause = value;
        }
    }
}