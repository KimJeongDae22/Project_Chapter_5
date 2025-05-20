using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Condition : MonoBehaviour
{
    [SerializeField] private Condition health;
    [SerializeField] private Condition hunger;
    [SerializeField] private Condition stamina;

    private void Start()
    {
        CharacterManager.Instance.Player.GetCondition().ui_Condition = this;
    }

    public Condition GetHealth() { return health; }
    public Condition GetHunger() { return hunger; }
    public Condition GetStamina() { return stamina; }

}
