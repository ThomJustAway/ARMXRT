using Assets.Scripts.UI_Related;
using UnityEngine;
using UnityEngine.UI;

public class SlotButton : CustomButton
{
    [Header("Slot related")]
    private int index;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Image itemImage;
    private PlayerInventory inventory;
    private Holdable itemAssign;
    public bool HasItem { get { return ItemAssign != null; } }

    public Holdable ItemAssign { get => itemAssign; }

    public void Init(PlayerInventory inventory , int index)
    {
        this.inventory = inventory;
        this.index = index;
        itemImage.enabled = false; //dont show the item
        actions.AddListener(OnClick);  
    }

    private void OnEnable()
    {
        PlayerInventory.Instance.onclickEvent += CheckButton;
        
    }

    private void OnDisable()
    {
        inventory.onclickEvent -= CheckButton;
    }

    void CheckButton(int index)
    {
        print($"this is {name} recieving index {index}");
        if(this.index != index)
        {
            buttonImage.sprite = inventory.UnSelectedImageButton;
        }
        else
        {
            buttonImage.sprite = inventory.SelectedImageButton;
        }
    }

    void OnClick()
    {
        inventory.onclickEvent.Invoke(index);
    }

    public void AssignItem(Holdable item)
    {
        itemImage.enabled = true; //dont show the item
        itemImage.sprite = item.Image;
        itemAssign = item;
    }

    public void UseItem()
    {
        itemImage.enabled = false; //dont show the item
        itemAssign = null;

    }
}