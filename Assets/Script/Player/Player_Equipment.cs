using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Equipment : MonoBehaviour
{
    [SerializeField] private Equip curEquip;
    [SerializeField] private Transform equipCamera;

    private Player_Controller controller;
    private Player_Condition condition;
    void Start()
    {
        controller = GetComponent<Player_Controller>();
        condition = GetComponent<Player_Condition>();
    }
    void Update()
    {
        
    }
    public void EquipNew(ItemData data)
    {
        UnEquip();
        curEquip = Instantiate(data.GetEquipPrefab(), equipCamera).GetComponent<Equip>();
    }
    public void UnEquip()
    {
        if (curEquip != null)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}
