using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{
    [SerializeField] private float atkRate;
    [SerializeField] private float atkDistance;
    [SerializeField] private bool isAtk;

    [Header("리소스 게더링")]
    [SerializeField] private bool doesGatherResources;

    [Header("컴벳")]
    [SerializeField] private bool doesDealDamage;
    [SerializeField] private int damage;
}
