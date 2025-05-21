using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private ItemSlot[] slots;
    [SerializeField] private GameObject invenWindow;
    [SerializeField] private Transform slotPanel;

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

    void Start()
    {
        invenWindow = this.gameObject;
        controller = CharacterManager.Instance.Player.GetPlayer_Controller();
        controller.SetPlayerInven(ToggleInven);
        condition = CharacterManager.Instance.Player.GetCondition();
        
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

    // Update is called once per frame
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


}
