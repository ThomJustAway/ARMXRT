using Patterns;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : Patterns.Singleton<GameManager>
    {
        [SerializeField] private int amountOfTime = 180;

        public int AmountOfTime { get => amountOfTime; set => amountOfTime = value; }

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