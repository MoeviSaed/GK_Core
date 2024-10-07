using GameEvents;
using System;
using UnityEngine;

namespace GTC
{
    public class PauseTickTimer : PauseTimer
    {
        public float TickTime { get; protected set; }
        public float Tick { get; protected set; }
        public float LastTickTime { get; protected set; }


        public event Action OnTick;
        
        public PauseTickTimer(GameTimeController gameTimeController, IGameEvents gameEvents) : base(gameTimeController, gameEvents) {}
        
        
        public PauseTickTimer StartTickTimer(float time, float tick)
        {
            TickTime = tick;
            Tick = 0;
            StartTimer(time);
            return this;
        }

        public PauseTickTimer StartInfinityTick(float tick, float startTime = 0)
        {
            TickTime = tick;
            Tick = 0;
            StartInifinity(startTime);
            return this;
        }

        public override Timer Stop(bool endEvent = false)
        {
            TickTime = 0;
            return base.Stop(endEvent);
        }

        protected override void Update(float time)
        {
            if (pause) return;
            if (TickTime > 0) Tick += time;
            CheckTick();
            base.Update(time);
        }

        protected void CheckTick()
        {
            if (Tick >= TickTime)
            {
                LastTickTime = Tick;
                OnTick?.Invoke();
                Tick = 0;
            }
        }
    }
}