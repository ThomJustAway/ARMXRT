using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour, IHittable
{
    private Animator m_Animator;
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }
    public void OnHit()
    {
        bool IsOpen = m_Animator.GetBool("Open");
        if(IsOpen)
        {
            SoundManager.Instance.PlayAudio(SFXClip.ShelfClosing);
        }
        else
        {
            SoundManager.Instance.PlayAudio(SFXClip.ShelfOpening);
        }
        m_Animator.SetBool("Open", !IsOpen);
    }
}
