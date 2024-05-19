using Patterns;
using System.Collections;
using Unity.VisualScripting;

namespace Assets.Scripts
{
    public class GameManager : Patterns.Singleton<GameManager>
    {
        private void Start()
        {
            StartPlacing();
        }

        public void StartPlacing()
        {
            EventManager.Instance.TriggerEvent(EventName.BeginPlacing);
        }

        public void FinishPlacing()
        {
            EventManager.Instance.TriggerEvent(EventName.BeginAdjustingARScene);
        }

        public void StartGame()
        {
            EventManager.Instance.TriggerEvent(EventName.StartGame);
        }
    }
}