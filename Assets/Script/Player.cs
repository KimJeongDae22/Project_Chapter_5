using UnityEngine;

public class Player : MonoBehaviour
{
    private Player_Controller controller;
    private Player_Condition condition;
    void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<Player_Controller>();
        condition = GetComponent<Player_Condition>();
    }
    public Player_Controller GetPlayer_Controller() { return controller; }
    public Player_Condition GetCondition() { return condition; }
}
