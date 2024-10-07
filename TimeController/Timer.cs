using System;

namespace GTC
{
    public class Timer
    {
        public bool Active { get; protected set; }
        public float Time { get; protected set; }

        public float TimerTime { get; private set; }

        public event Action OnTimerEnd;

        private GameTimeController _gameTimeController;
        
        protected virtual bool pause
        {
            get => _pause;
            set => _pause = value;
        }
        private bool _pause;

        public Timer(GameTimeController gameTimeController)
        {
            _gameTimeController = gameTimeController;
            pause = true;
            TimerTime = 0;
            _gameTimeController.OnUpdate += Update;
        }

        public Timer StartInifinity(float startTime = 0)
        {
            Time = startTime;
            pause = false;
            Active = true;
            return this;
        }

        public Timer StartTimer(float time)
        {
            TimerTime = time;
            Time = 0;
            pause = false;
            Active = true;
            return this;
        }
        
        public void AddPenalty(float penaltySeconds)
        {
            Time -= penaltySeconds;
            if (Time < 0) Time = 0;
        }

        public void Pause() => pause = true;

        public void Continue() => pause = false;

        public virtual Timer Stop(bool endEvent = false)
        {
            Pause();
            if (endEvent) OnTimerEnd?.Invoke();
            TimerTime = 0;
            Active = false;
            return this;
        }

        public void Clear() => _gameTimeController.OnUpdate -= Update;
        public float GetRemainingTime()
        {
            return Math.Max(TimerTime - Time, 0);
        }
        
        protected virtual void Update(float time)
        {
            if (pause) return;
            Time += UnityEngine.Time.deltaTime;
            CheckTime();
        }

        protected void CheckTime()
        {
            if (Time >= TimerTime && TimerTime != 0)
            {
                OnTimerEnd?.Invoke();
                Time = 0;
            }
        }
    }
}