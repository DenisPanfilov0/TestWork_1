using UnityEngine;

public class ItemDataComponent : MonoBehaviour
{
    [SerializeField] private Item _item;
        
    private Inventory _inventory;

    public Item Item
    {
        get => _item;
        set => _item = value;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _inventory = FindObjectOfType<Inventory>(true);
            _inventory.AddItem(_item.Name);
            Destroy(gameObject);
        }
    }
}