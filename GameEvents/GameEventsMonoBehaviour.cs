using UnityEngine;

namespace GameEvents
{
    public abstract class GameEventsMonoBehaviour : MonoBehaviour
    {
        protected IGameEvents GameEvents;

        protected bool pause;
        protected bool gameEnd;
        protected bool gameStarted;
        
        protected virtual void Awake()
        {
            GameEvents = GameEventsSender.Instance;
            gameEnd = false;
            gameStarted = false;

            GameEvents.OnGameStart += onGameStart;
            GameEvents.OnGamePredStart += OnGamePredStart;
            GameEvents.OnGameEnd += onGameEnd;
            GameEvents.OnGamePause += onGamePause;
            pause = GameEvents.Pause;
            OnGamePause(pause);
        }

        protected virtual void OnDestroy()
        {
            if (GameEvents != null)
            {
                GameEvents.OnGameStart -= onGameStart;
                GameEvents.OnGamePredStart -= onGamePredStart;
                GameEvents.OnGameEnd -= onGameEnd;
                GameEvents.OnGamePause -= onGamePause;
            }
        }

        private void onGameStart()
        {
            this.gameEnd = false;
            gameStarted = true;
            OnGameStart();
        }
        protected virtual void OnGameStart() { }

        private void onGamePredStart()
        {
            this.gameEnd = false;
            OnGamePredStart();
        }
        protected virtual void OnGamePredStart() { }

        private void onGameEnd()
        {
            this.gameEnd = true;
            OnGameEnd();
        }
        protected virtual void OnGameEnd() { }

        private void onGamePause(bool pause) 
        {
            this.pause = pause;
            OnGamePause(pause);
        }
        protected virtual void OnGamePause(bool pause) { }
    }
}