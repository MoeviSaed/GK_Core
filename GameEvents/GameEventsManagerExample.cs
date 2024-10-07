using GTC;
using UnityEngine;
using UnityEngine.UI;

namespace GameEvents.Example
{
    public class GameEventsManagerExample : MonoBehaviour
    {
        [SerializeField] private GameEventsSender gameEvents;
        [SerializeField] private GameTimeController timeController;
        [SerializeField] private Button playButton;
        [Space]
        [SerializeField] private GameObject[] confettis;
        [SerializeField] private float confettisWait;
        [Space]
        [SerializeField] private float gamePreStartTime;
        [SerializeField] private float gameStartTime;
        [SerializeField] private float gameEndTime;

        private const string GAME_PRE_START_TIMER_NAME = "GAME_PRE_START_TIMER";
        private const string GAME_START_TIMER_NAME = "GAME_START_TIMER";
        private const string GAME_END_TIMER_NAME = "GAME_END_TIMER";

        private Timer _preStartTimer;
        private Timer _startTimer;
        private Timer _endTimer;

        private bool _gameStarted;
        private bool _gameEnd;

        private void Start()
        {
            _gameStarted = false;
            _gameEnd = false;

            if (playButton) playButton.onClick.AddListener(OnPlayClicked);

            _preStartTimer = timeController.CreateCustomTimer(GAME_PRE_START_TIMER_NAME);
            _startTimer = timeController.CreateCustomTimer(GAME_START_TIMER_NAME);
            _endTimer = timeController.CreateCustomTimer(GAME_END_TIMER_NAME);

            _preStartTimer.OnTimerEnd += () =>
            {
                gameEvents.InvokePredStart();
                _preStartTimer.Stop();
                _startTimer.StartTimer(gamePreStartTime);
            };

            _startTimer.OnTimerEnd += () =>
            {
                gameEvents.InvokeStart();
                gameEvents.DisablePause(this);
                _startTimer.Stop();
            };

            gameEvents.EnablePause(this);
        }

        private void OnDestroy()
        {
            timeController.ClearCustomTimer(GAME_PRE_START_TIMER_NAME);
            timeController.ClearCustomTimer(GAME_START_TIMER_NAME);
        }

        private void OnPlayClicked()
        {
            if (!_gameStarted)
            {
                _preStartTimer.StartTimer(gamePreStartTime);
                _gameStarted = true;
            }
        }

        private void GameEnd()
        {
            _gameEnd = true;
            gameEvents.EnablePause(true);
            gameEvents.InvokeEnd();
            Debug.Log("End");
        }

        private void GameLost(object obj)
        {
            _gameEnd = true;
            gameEvents.EnablePause(true);
            gameEvents.InvokeEnd();
            Debug.Log("End");
        }
    }
}
