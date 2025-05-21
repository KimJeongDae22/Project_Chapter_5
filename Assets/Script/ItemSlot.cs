using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private UI_Inventory inventory;

    [SerializeField] private int index;
    [SerializeField] private bool equipped;
    [SerializeField] private int quantity;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetIndex(int a)
    { index = a; }
    public void SetInventory(UI_Inventory a)
    { inventory = a; }
}
