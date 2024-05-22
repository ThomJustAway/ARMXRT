using Patterns;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTip : Singleton<ToolTip>
{
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] Animator animator;
    
    public void OpenNewMessage(string text)
    {

        gameObject.SetActive(true);
        message.text = text;
        StopAllCoroutines();//stop all the coroutine and start a new one.
        StartCoroutine(ShowMessage());
    }

    public void CloseMessage()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator ShowMessage()
    {
        yield return new WaitForSeconds(5f);
        animator.SetTrigger("Close");
    }
}
