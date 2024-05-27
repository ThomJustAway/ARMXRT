using Patterns;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI_Related
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]TextMeshProUGUI timeText;
        private int timeLeft;
        private bool hasStartedBefore = false;
        private void OnEnable()
        {
            EventManager.Instance.AddListener(EventName.RestartGame, StopTimer);
            EventManager.Instance.AddListener(EventName.WinGame, StopTimer);
            EventManager.Instance.AddListener(EventName.PauseGame, PauseTimer);
            EventManager.Instance.AddListener(EventName.ResumeGame, StartTimer);
            StartGame();
            StartTimer();
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener(EventName.RestartGame, StopTimer);
            EventManager.Instance.RemoveListener(EventName.WinGame, StopTimer);
            EventManager.Instance.RemoveListener(EventName.PauseGame, PauseTimer);
            EventManager.Instance.RemoveListener(EventName.ResumeGame, StartTimer);
        }

        void StartGame()
        {
            if (hasStartedBefore) return;
            hasStartedBefore = true;
            timeLeft = GameManager.Instance.AmountOfTime;
        }

        private void StartTimer()
        {
            StartCoroutine(TimerCoroutine());
        }

        void StopTimer()
        {
            hasStartedBefore = false;
            StopAllCoroutines();
        }

        void PauseTimer()
        {
            StopAllCoroutines();
        }



        IEnumerator TimerCoroutine()
        {
            CreateTimeText();
            while (timeLeft > 0)
            {
                //always wait for one second
                yield return new WaitForSeconds(1);
                timeLeft -= 1;
                CreateTimeText();
            }
            //player lost the game
            EventManager.Instance.TriggerEvent(EventName.LoseGame);
            
        }

        void CreateTimeText()
        {
            int secs = timeLeft % 60;
            int mins = (timeLeft - secs) / 60;
            string secText = secs.ToString();
            if(secs < 10)
            {
                secText = "0" + secText;
            }
            timeText.text = $"{mins}: {secText}";
        }


    }
}
