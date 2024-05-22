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

        private void OnEnable()
        {
            EventManager.Instance.AddListener(EventName.RestartGame, StopTimer);
            EventManager.Instance.AddListener(EventName.WinGame, StopTimer);
            StartGame();
        }

        private void OnDisable()
        {
            StopTimer();
            EventManager.Instance.RemoveListener(EventName.RestartGame, StopTimer);
            EventManager.Instance.RemoveListener(EventName.WinGame, StopTimer);
        }

        void StartGame()
        {
            timeLeft = GameManager.Instance.AmountOfTime;
            StartCoroutine(TimerCoroutine());
        }

        void StopTimer()
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
