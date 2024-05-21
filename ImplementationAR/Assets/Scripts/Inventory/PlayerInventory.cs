using Patterns;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    private int curSelectedIndex = 0;

    //will have a 5 slots
    [SerializeField] private SlotButton[] buttons;
    [SerializeField] private GameObject slotButtonPrefab;
    [SerializeField] private Transform buttonContainer;
    [Header("debugging purpose")]
    
    [SerializeField] Sprite selectedImageButton;
    [SerializeField] Sprite unSelectedImageButton;
    public delegate void OnClick(int index);
    public OnClick onclickEvent;
    public Sprite  SelectedImageButton { get => selectedImageButton; set => selectedImageButton = value; }
    public Sprite UnSelectedImageButton { get => unSelectedImageButton; set => unSelectedImageButton = value; }
    private void Start()
    {
        //Will have a predetermine slot number of 5 for this project
        int amountOfSlot = 5;
        buttons = new SlotButton[amountOfSlot];
        for(int i = 0; i < amountOfSlot; i++)
        {
            var component= Instantiate(slotButtonPrefab, buttonContainer).GetComponent<SlotButton>();
            component.Init(this, i);
            //assign the component to the array.
            component.name = $"Slot name {i}";
            buttons[i] = component;
        }
        onclickEvent += ChangeSelection;
        //make sure to show that at least one is being selected
        onclickEvent.Invoke(curSelectedIndex);
    }

    void ChangeSelection(int index)
    {
        curSelectedIndex = index;
        print($"Selected is {index}");
    }

    public bool TryGetHoldableFromSelected(out SlotButton btn)
    {
        btn = buttons[curSelectedIndex];
        if (btn.HasItem)
        {
            return true;
        }
        return false;
    }

}
