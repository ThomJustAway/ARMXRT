using Assets.Scripts.Manager;
using Patterns;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTip : Singleton<ToolTip>
{
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] Animator animator;

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    public void OpenNewMessage(string text)
    {
        gameObject.SetActive(true);
        message.text = text;

        SoundManager.Instance.PlayAudio(SFXClip.NotificationSound);

        StopAllCoroutines();//stop all the coroutine and start a new one.
        StartCoroutine(StartTimer());
    }

    public void CloseMessage()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(5f);
        animator.SetTrigger("Close");
    }
}
