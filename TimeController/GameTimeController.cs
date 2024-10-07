using GameEvents;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GTC
{
    public enum DefaultTimerTypes
    {
        Scene = 0,
        Game = 1,
        GameIgnorePause = 2,
        GameIgnoreEndPause = 3
    }
    
    public enum TimerType
    {
        Default = 0,
        Pause = 1,
        PauseTick = 2
    }

    public class GameTimeController : GameEventsMonoBehaviour
    {
        private Dictionary<DefaultTimerTypes, Timer> _defaultTimers;
        private Dictionary<string, Timer> _customTimers;
        private Dictionary<object, Timer> _customTimersObj;

        private bool _gameStarted;
        private bool _pause;

        public event Action<float> OnUpdate;

        private GameTimeController()
        {
            _gameStarted = false;

            _customTimers = new Dictionary<string, Timer>();
            _customTimersObj = new Dictionary<object, Timer>();

            _defaultTimers = new Dictionary<DefaultTimerTypes, Timer>()
            {
                { DefaultTimerTypes.Scene, new Timer(this) },
                { DefaultTimerTypes.Game, new Timer(this) },
                { DefaultTimerTypes.GameIgnorePause, new Timer(this)},
                { DefaultTimerTypes.GameIgnoreEndPause, new Timer(this)}
            };
        }

        private void Start()
        {
            _defaultTimers[DefaultTimerTypes.Scene].StartInifinity();
        }

        private void LateUpdate() => OnUpdate?.Invoke(Time.deltaTime);

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _defaultTimers[DefaultTimerTypes.Scene].Pause();
            _defaultTimers[DefaultTimerTypes.Game].Pause();
            _defaultTimers[DefaultTimerTypes.GameIgnorePause].Pause();
            _defaultTimers[DefaultTimerTypes.GameIgnoreEndPause].Pause();
            OnUpdate = null;
        }

        public Timer GetDefaultTimer(DefaultTimerTypes type) => _defaultTimers[type];

        public Timer CreateCustomTimer(string timerName, TimerType timerType = TimerType.Default)
        {
            _customTimers.Add(timerName, CreateTimer(timerType));
            return _customTimers[timerName];
        }

        public Timer CreateCustomTimer(object obj, TimerType timerType = TimerType.Default)
        {
            _customTimersObj.Add(obj, CreateTimer(timerType));
            return _customTimersObj[obj];
        }

        public bool TryGetCustomTimer(string timerName, out Timer timer)
        {
            if (_customTimers.ContainsKey(timerName)) timer = _customTimers[timerName];
            else timer = null;
            return timer != null;
        }
        public bool TryGetCustomTimer(object obj, out Timer timer)
        {
            if (_customTimersObj.ContainsKey(obj)) timer = _customTimersObj[obj];
            else timer = null;
            return timer != null;
        }

        public void ClearCustomTimer(string timerName, bool endEvent = false)
        {
            if (!_customTimers.ContainsKey(timerName)) return;
            _customTimers[timerName].Stop(endEvent);
            _customTimers[timerName].Clear();
            _customTimers.Remove(timerName);
        }
        public void ClearCustomTimer(object obj, bool endEvent = false)
        {
            if (!_customTimersObj.ContainsKey(obj)) return;
            _customTimersObj[obj].Stop(endEvent);
            _customTimersObj[obj].Clear();
            _customTimersObj.Remove(obj);
        }

        protected override void OnGameStart()
        {
            _defaultTimers[DefaultTimerTypes.Game].StartInifinity();
            _defaultTimers[DefaultTimerTypes.GameIgnorePause].StartInifinity();
            _defaultTimers[DefaultTimerTypes.GameIgnoreEndPause].StartInifinity();
            _gameStarted = true;
        }

        protected override void OnGameEnd()
        {
            _defaultTimers[DefaultTimerTypes.Game].Pause();
            _defaultTimers[DefaultTimerTypes.GameIgnorePause].Pause();
            _gameStarted = false;
        }

        protected override void OnGamePause(bool pause)
        {
            _pause = pause;
            if (_gameStarted)
            {
                if (pause) _defaultTimers[DefaultTimerTypes.Game].Pause();
                else _defaultTimers[DefaultTimerTypes.Game].Continue();
            }
        }

        private Timer CreateTimer(TimerType timerType)
        {
            switch (timerType)
            {
                case TimerType.Pause: 
                    var pauseTimer = new PauseTimer(this, GameEvents);
                    return pauseTimer;
                case TimerType.PauseTick:
                    var pauseTickTimer = new PauseTickTimer(this, GameEvents);
                    return pauseTickTimer; 
                default: return new Timer(this);
            }
        }
    }
}