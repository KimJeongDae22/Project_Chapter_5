using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private Button btn;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Outline outline;

    [SerializeField] private UI_Inventory inventory;

    [SerializeField] private int index;
    [SerializeField] private bool equipped;
    [SerializeField] private int quantity;
    void Start()
    {
        outline = GetComponent<Outline>();

    }

    private void OnEnable()
    {
        outline.enabled = equipped;
    }
    void Update()
    {

    }
    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = itemData.GetIcon();
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        if (outline != null)
            outline.enabled = equipped;
    }
    public void Clear()
    {
        itemData = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }
    public void OnClickBtn()
    {
        inventory.SelectItem(index);
    }
    public ItemData GetItemData()
    { return itemData; }
    public void SetItemData(ItemData a)
    {
        itemData = a;
    }
    public void SetIndex(int a)
    { index = a; }
    public void SetInventory(UI_Inventory a)
    { inventory = a; }
    public int GetQuantity()
        { return quantity; }
    public void SetQuantity(int a)
    { quantity = a; }
    public bool GetEquipped()
        { return equipped; }
    public void SetEquipped(bool a)
        { equipped = a; }
}
