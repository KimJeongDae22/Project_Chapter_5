using UnityEngine;

public class Player : MonoBehaviour
{
    private Player_Controller controller;
    void Start()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<Player_Controller>();
    }
}
