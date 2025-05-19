using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Condition : MonoBehaviour
{
    public UI_Condition ui_Condition;

    Condition health { get { return ui_Condition.GetHealth(); } }
    Condition hunger { get { return ui_Condition.GetHunger(); } }
    Condition stamina { get { return ui_Condition.GetStamina(); } }

    [SerializeField] private float noHungerDamage;
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
        Debug.Log("µÚÁü;;");
    }
    void Die()
    {
        Debug.Log("µÚÁü;;");
    }
}
