using UnityEngine;

public class Holdable : MonoBehaviour
{
    [SerializeField] private string nameOfItem;
    [SerializeField] private Sprite image;

    public string NameOfItem { get => nameOfItem; set => nameOfItem = value; }
    public Sprite Image { get => image; set => image = value; }
}
