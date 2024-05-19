using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI_Related
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]TextMeshPro timeText;
        private int timeLeft;

        private void Start()
        {
            timeLeft = GameManager.Instance.AmountOfTime;
        }

        void CreateTimeText()
        {
            int mins = timeLeft % 60;

        }
    }
}