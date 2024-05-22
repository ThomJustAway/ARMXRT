using Assets.Scripts.Manager;
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

        private void OnEnable()
        {
            EventManager.Instance.AddListener(EventName.WinGame, PlayWinMusic);
            EventManager.Instance.AddListener(EventName.LoseGame, PlayLoseMusic);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener(EventName.WinGame, PlayWinMusic);
            EventManager.Instance.RemoveListener(EventName.LoseGame, PlayLoseMusic);
        }

        private void PlayWinMusic()
        {
            SoundManager.Instance.PlayAudio(SFXClip.WinSound);
        }

        private void PlayLoseMusic()
        {
            SoundManager.Instance.PlayAudio(SFXClip.LoseSound);
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

        public void RestartGame()
        {
            EventManager.Instance.TriggerEvent(EventName.RestartGame);
        }
        [ContextMenu("Win Game")]
        public void WinGame()
        {
            EventManager.Instance.TriggerEvent(EventName.WinGame);
        }
        [ContextMenu("Lose Game")]
        public void LoseGame()
        {
            EventManager.Instance.TriggerEvent(EventName.LoseGame);
        }
    }
}