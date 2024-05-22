using Patterns;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Door : MonoBehaviour , IHittable
    {
        private Animator animator;
        [SerializeField] private Holdable keyNeeded;
        private bool isOpen;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        public void OnHit()
        {
            if(isOpen)
            {
                animator.SetBool("Open", true);
                EventManager.Instance.TriggerEvent(EventName.WinGame);
            }
            else
            {
                if(PlayerInventory.Instance.TryGetHoldableFromSelected(out var btn))
                {
                    if(btn.ItemAssign == keyNeeded)
                    {
                        isOpen = true;
                        btn.UseItem();
                        //can add something to show that the player manage to unlock the door.
                    }
                    else
                    {
                        //show message that it is wrong.
                    }
                }
                else
                {
                    //show the message here to tell player that they need a key
                }
            }
        }
    }
}