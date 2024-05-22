using Assets.Scripts.Manager;
using Patterns;

using UnityEngine;

public class Vault :MonoBehaviour, IHittable
{
    private Animator animator;
    [SerializeField] private Holdable keyNeeded;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnHit()
    {
        if (PlayerInventory.Instance.TryGetHoldableFromSelected(out var btn))
        {
            if (btn.ItemAssign == keyNeeded)
            {
                btn.UseItem();
                animator.SetTrigger("Open");
                SoundManager.Instance.PlayAudio(SFXClip.VaultDoorOpening);
                ToolTip.Instance.OpenNewMessage("vault is unlock");
                GetComponent<Collider>().enabled = false;
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
