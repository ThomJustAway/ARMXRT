using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Door : MonoBehaviour , IHittable
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        public void OnHit()
        {
            animator.SetBool("Open", true);
        }
    }
}