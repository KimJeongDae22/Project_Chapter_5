using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    EquipAble,
    ConsumAble,
    Resource
}

public enum ConsumType
{
    Health,
    Hunger
}
[Serializable]
public class ItemDataConsumAble
{
    [SerializeField] private ConsumType type;
    [SerializeField] private float value;

    public ConsumType GetConsumType()
    { return type; }
    public float GetConsumValue()
    { return value; }
}
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("정보")]
    [SerializeField] private string name;
    [SerializeField] private string info;
    [SerializeField] private ItemType type;
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject dropItem;

    [Header("복수 소지 가능")]
    [SerializeField] private bool stackAble;
    [SerializeField] private int maxStackAmount;

    [Header("소모품")]
    [SerializeField] private ItemDataConsumAble[] consumAbles;

    public string GetName()
    { return name; }
    public string GetInfo()
        { return info; }
    public ItemType GetItemType()
        { return type; }
    public Sprite GetIcon() { return icon; }
    public bool GetStackAble()
        { return stackAble; }
    public int GetMaxStackAmount()
        { return maxStackAmount; }
    public GameObject DropItem() { return dropItem; }
    public ItemDataConsumAble[] GetConsumAbles() {  return consumAbles; }
}
