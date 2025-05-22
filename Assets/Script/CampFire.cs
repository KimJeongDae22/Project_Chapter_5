using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageRate;
    // 불타오르네 파이어~
    List<DamageAble> objs = new List<DamageAble>();
    void Start()
    {
        InvokeRepeating("GetDamage", 0, damageRate);
    }

    void GetDamage()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            objs[i].TakeDamage(damage);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DamageAble damageAble))
        {
            objs.Add(damageAble);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out DamageAble damageAble))
        {
            objs.Remove(damageAble);
        }
    }
}
