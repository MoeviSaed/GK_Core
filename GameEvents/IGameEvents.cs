using System;

namespace GameEvents
{
    public interface IGameEvents
    {
        public event Action OnGameStart;
        public event Action OnGameEnd;
        public event Action OnGamePredStart;
        public event Action<bool> OnGamePause;
        public bool Pause { get; }
    }

    public interface IGameEventsSender
    {
        public void InvokeStart();
        public void InvokePredStart();
        public void InvokeEnd();
        public void EnablePause(object source);
        public void DisablePause(object source);
    }
}