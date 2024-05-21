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
        bool currentState = m_Animator.GetBool("Open");
        m_Animator.SetBool("Open", !currentState);
    }
}
