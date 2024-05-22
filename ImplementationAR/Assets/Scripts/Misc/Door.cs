using Assets.Scripts.Manager;
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
                SoundManager.Instance.PlayAudio(SFXClip.MetalDoorOpening);
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
                        SoundManager.Instance.PlayAudio(SFXClip.UnlockingDoor);
                        ToolTip.Instance.OpenNewMessage("Door is unlock");
                        //can add something to show that the player manage to unlock the door.
                    }
                    else
                    {
                        ToolTip.Instance.OpenNewMessage("The Item used seems to be wrong");
                    }
                }
                else
                {
                    //show the message here to tell player that they need a key
                    ToolTip.Instance.OpenNewMessage("It Seems that you need a key to open the door.");
                }
            }
        }
    }
}