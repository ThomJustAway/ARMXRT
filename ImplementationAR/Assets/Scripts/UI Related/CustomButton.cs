using Assets.Scripts.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI_Related
{
    //this is when the player hover on a button, it will play a hover clip.
    public class CustomButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private SFXClip clipToPlayWhenClick;
        [SerializeField] private float delayInSeconds = 0.1f;
        public UnityEvent actions;

        //this is special case for the button for the connecting button.
        public void ClickSound()
        {
            SoundManager.Instance.PlayAudio(clipToPlayWhenClick);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ClickSound();
            StartCoroutine(PlayingButtonBeforeActions());
        }

        private IEnumerator PlayingButtonBeforeActions()
        {
            yield return new WaitForSecondsRealtime(delayInSeconds);
            actions?.Invoke();
        }
    }
}