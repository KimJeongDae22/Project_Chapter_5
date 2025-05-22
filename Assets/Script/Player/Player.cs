using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Player_Controller controller;
    private Player_Condition condition;
    private Player_Equipment equipment;

    private ItemData itemData;
    private Action addIitem;

   [SerializeField]  private Transform dropItemPos;
    void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<Player_Controller>();
        condition = GetComponent<Player_Condition>();
        equipment = GetComponent<Player_Equipment>();
    }
    public Player_Controller GetPlayer_Controller() { return controller; }
    public Player_Condition GetCondition() { return condition; }
    public Player_Equipment GetEquipment() { return equipment; }
    public void SetPlayerItemData(ItemData data) { itemData = data; }
    public ItemData GetPlayerItemData() { return itemData; }
    public Action GetAddItem() { return addIitem; }
    public void SetAddItem(Action a)
    { addIitem += a; }
    public Transform GetDropItemPos() { return dropItemPos; }
}
