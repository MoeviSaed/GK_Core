using GameEvents;
using UnityEngine;

namespace GameEvents.PauseSenders
{
    public class PauseSender : MonoBehaviour
    {
        [SerializeField] private GameObject gameEventsSenderObject;

        protected IGameEventsSender gameEventsSender;

        protected virtual void Awake()
        {
            gameEventsSender = gameEventsSenderObject.GetComponent<IGameEventsSender>();
        }

        public void SendEnablePause() => gameEventsSender.EnablePause(this);
        public void SendDisablePause() => gameEventsSender.DisablePause(this);
    }
}