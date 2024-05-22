using UnityEngine;

public class Holdable : MonoBehaviour , IHittable
{
    [SerializeField] private string nameOfItem;
    [SerializeField] private Sprite image;

    public string NameOfItem { get => nameOfItem; set => nameOfItem = value; }
    public Sprite Image { get => image; set => image = value; }

    public void OnHit()
    {
        PlayerInventory inventory = PlayerInventory.Instance;
        inventory.AddItem(this);
        //show that the gameobject is used.
        gameObject.SetActive(false);
    }
}
