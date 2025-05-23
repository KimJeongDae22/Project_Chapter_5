using TMPro;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private ItemSlot[] slots;
    [SerializeField] private GameObject invenWindow;
    [SerializeField] private Transform slotPanel;
    [SerializeField] private Transform dropItemPos;

    [Header("선택된 아이템")]
    [SerializeField] private TextMeshProUGUI selected_ItemName;
    [SerializeField] private TextMeshProUGUI selected_ItemInfo;
    [SerializeField] private TextMeshProUGUI selected_StatName;
    [SerializeField] private TextMeshProUGUI selected_StatValue;
    [SerializeField] private GameObject useBtn;
    [SerializeField] private GameObject equipBtn;
    [SerializeField] private GameObject unEquipBtn;
    [SerializeField] private GameObject dropBtn;

    private Player_Controller controller;
    private Player_Condition condition;

    private ItemData selectedItemData;
    private int selectedItemIndex;

    private int curEquipIndex;
    void Start()
    {
        invenWindow = this.gameObject;
        controller = CharacterManager.Instance.Player.GetPlayer_Controller();
        controller.SetPlayerInven(ToggleInven);

        condition = CharacterManager.Instance.Player.GetCondition();

        dropItemPos = CharacterManager.Instance.Player.GetDropItemPos();

        CharacterManager.Instance.Player.SetAddItem(AddItem);

        invenWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].SetIndex(i);
            slots[i].SetInventory(this);
        }
        ClearSelectedItemWindow();
    }
    void Update()
    {

    }

    void ClearSelectedItemWindow()
    {
        selected_ItemName.text = string.Empty;
        selected_ItemInfo.text = string.Empty;
        selected_StatName.text = string.Empty;
        selected_StatValue.text = string.Empty;

        useBtn.SetActive(false);
        equipBtn.SetActive(false);
        unEquipBtn.SetActive(false);
        dropBtn.SetActive(false);
    }

    public void ToggleInven()
    {
        if (invenWindow.activeSelf == true)
        {
            invenWindow.SetActive(false);
        }
        else
        {
            invenWindow.SetActive(true);
        }
    }
    void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.GetPlayerItemData();
        // 아이템 중복 가능 체크
        if (data.GetStackAble())
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.SetQuantity(slot.GetQuantity() + 1);
                UI_Update();
                CharacterManager.Instance.Player.SetPlayerItemData(null);
                return;
            }
        }
        // 비어있는 슬롯 가져오기
        ItemSlot emptySlot = GetEmptySlot();
        // 비어있는 슬롯 있으면
        if (emptySlot != null)
        {
            emptySlot.SetItemData(data);
            emptySlot.SetQuantity(1);
            UI_Update();
            CharacterManager.Instance.Player.SetPlayerItemData(null);
            return;
        }
        // 없으면
        ThrowItem(data);
        CharacterManager.Instance.Player.SetPlayerItemData(null);
    }
    void UI_Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetItemData() != null)
            { slots[i].Set(); }
            else
            {
                slots[i].Clear();
            }
        }
    }
    void ThrowItem(ItemData data)
    {
        Instantiate(data.DropItem(), dropItemPos.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }
    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetItemData() == data && slots[i].GetQuantity() < data.GetMaxStackAmount())
            { return slots[i]; }
        }
        return null;
    }
    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetItemData() == null)
            { return slots[i]; }
        }
        return null;
    }
    public void SelectItem(int index)
    {
        if (slots[index].GetItemData() == null)
            return;

        selectedItemData = slots[index].GetItemData();
        selectedItemIndex = index;

        selected_ItemName.text = selectedItemData.GetName();
        selected_ItemInfo.text = selectedItemData.GetInfo();

        selected_StatName.text = string.Empty;
        selected_StatValue.text = string.Empty;

        for (int i = 0; i < selectedItemData.GetConsumAbles().Length; i++)
        {
            selected_StatName.text += selectedItemData.GetConsumAbles()[i].GetConsumType().ToString() + "\n";
            selected_StatValue.text += selectedItemData.GetConsumAbles()[i].GetConsumValue().ToString() + "\n";
        }

        useBtn.SetActive(selectedItemData.GetItemType() == ItemType.ConsumAble);
        equipBtn.SetActive(selectedItemData.GetItemType() == ItemType.EquipAble && !slots[index].GetEquipped());
        unEquipBtn.SetActive(selectedItemData.GetItemType() == ItemType.EquipAble && slots[index].GetEquipped());
        dropBtn.SetActive(true);
    }
    public void OnUseBtn()
    {
        if (selectedItemData.GetItemType() == ItemType.ConsumAble)
        {
            for (int i = 0; i < selectedItemData.GetConsumAbles().Length; i++)
            {
                switch (selectedItemData.GetConsumAbles()[i].GetConsumType())
                {
                    case ConsumType.Health:
                        condition.Heal(selectedItemData.GetConsumAbles()[i].GetConsumValue());
                        break;
                    case ConsumType.Hunger:
                        condition.Eat(selectedItemData.GetConsumAbles()[i].GetConsumValue());
                        break;
                }
            }
            RemoveSelectedItem();
        }
    }
    public void OnDropBtn()
    {
        ThrowItem(selectedItemData);
        RemoveSelectedItem();
    }
    public void RemoveSelectedItem()
    {
        slots[selectedItemIndex].SetQuantity(slots[selectedItemIndex].GetQuantity() - 1);
        if (slots[selectedItemIndex].GetItemData().GetItemType() == ItemType.EquipAble)
        {
            UnEquip(selectedItemIndex);
        }
        if (slots[selectedItemIndex].GetQuantity() <= 0)
        {
            selectedItemData = null;
            slots[selectedItemIndex].SetItemData(null);

            selectedItemIndex = -1;
            ClearSelectedItemWindow();
        }
        UI_Update();
    }
    public void OnEquipBtn()
    {
        //장착 중인 무기가 있을 시 해제
        if (slots[curEquipIndex].GetEquipped())
        {
            UnEquip(curEquipIndex);
        }
        slots[selectedItemIndex].SetEquipped(true);
        curEquipIndex = selectedItemIndex;
        CharacterManager.Instance.Player.GetEquipment().EquipNew(selectedItemData);
        UI_Update();

        SelectItem(selectedItemIndex);
    }
    void UnEquip(int index)
    {
        slots[index].SetEquipped(false);
        CharacterManager.Instance.Player.GetEquipment().UnEquip();
        UI_Update();

        if (selectedItemIndex == index)
        {
            SelectItem(selectedItemIndex);
        }
    }

    public void OnUnEquipBtn()
    {
        UnEquip(selectedItemIndex);
    }
}
