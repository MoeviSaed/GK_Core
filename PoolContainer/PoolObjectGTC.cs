using GTC;

namespace PoolsContainer
{
    public class PoolObjectGTC : PoolObject
    {
        private Timer _timer;

        private const string Key = "GTC";

        public void BackToPoolGTC(GameTimeController gameTimeController, float time)
        {
            if (gameObject.activeInHierarchy)
            {
                if (_timer == null)
                {
                    _timer = gameTimeController.CreateCustomTimer(gameObject.GetInstanceID() + Key, TimerType.Pause);
                    _timer.OnTimerEnd += BackToPool;
                }
                _timer.StartTimer(time);
            }
            else BackToPool();
        }

        protected override void OnBackToPool()
        {
            if (_timer != null) _timer.Stop();
            base.OnBackToPool();
        }
    }
}