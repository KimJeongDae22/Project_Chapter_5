using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Player_Controller controller;
    private Player_Condition condition;

    public ItemData itemData;
    private Action addIitem;
    void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<Player_Controller>();
        condition = GetComponent<Player_Condition>();
    }
    public Player_Controller GetPlayer_Controller() { return controller; }
    public Player_Condition GetCondition() { return condition; }
    public void SetPlayerItemData(ItemData data) { itemData = data; }
    public Action GetAddItem() { return addIitem; }
}
