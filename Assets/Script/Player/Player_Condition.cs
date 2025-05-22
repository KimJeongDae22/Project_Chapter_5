using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
public interface DamageAble
{
    void TakeDamage(float damage);
}
public class Player_Condition : MonoBehaviour, DamageAble
{
    public void TakeDamage(float damage)
    {
        health.SubtractValue(damage);
        OnTakeDamage?.Invoke();
    }
    public UI_Condition ui_Condition;

    Condition health { get { return ui_Condition.GetHealth(); } }
    Condition hunger { get { return ui_Condition.GetHunger(); } }
    Condition stamina { get { return ui_Condition.GetStamina(); } }

    [SerializeField] private float noHungerDamage;

    public event Action OnTakeDamage;
    void Update()
    {
        hunger.SubtractValue(hunger.GetPassiveValue() * Time.deltaTime);
        stamina.AddValue(stamina.GetPassiveValue() * Time.deltaTime);

        if (hunger.GetCurValue() == 0f)
        {
            health.SubtractValue(noHungerDamage * Time.deltaTime);
        }
        if (health.GetCurValue() == 0f)
        {
            Die();
        }
    }
    public void Heal(float amount)
    {
        health.AddValue(amount);
    }
    public void Eat(float amount)
    {
        hunger.AddValue(amount);
    }
    void Die()
    {
        Debug.Log("µÚÁü;;");
    }

}
