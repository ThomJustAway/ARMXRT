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
            //subscribe to the win and lose event where it would play the win and lose music.z
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
        //this event trigger is to 
        //call out event for the other script to recognise.
        #region event triggers
        public void StartPlacing()
        {
            EventManager.Instance.TriggerEvent(EventName.BeginPlacing);
        }

        public void FinishPlacing()
        {
            EventManager.Instance.TriggerEvent(EventName.BeginAdjustingARScene);
        }

        public void PauseGame()
        {
            EventManager.Instance.TriggerEvent(EventName.PauseGame);
        }

        public void ResumeGame()
        {
            EventManager.Instance.TriggerEvent(EventName.ResumeGame);
        }

        public void StartGame()
        {
            EventManager.Instance.TriggerEvent(EventName.StartGame);
        }
        #endregion
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