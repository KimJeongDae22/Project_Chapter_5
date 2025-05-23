using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBlock : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player_Controller player))
        {
            player.ForceJump(jumpPower);
        }
    }
}
