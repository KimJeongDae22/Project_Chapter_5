using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public interface Interactable
{
    public string GetInteractPrompt();
    public void Oninteract();
}
public class ItemObject : MonoBehaviour, Interactable
{
    public string GetInteractPrompt()
    {
        string str = $"{data.GetName()}\n{data.GetInfo()}";
        return str ;
    }
    public void Oninteract()
    {
        CharacterManager.Instance.Player.SetPlayerItemData(data);
        CharacterManager.Instance.Player.GetAddItem()?.Invoke();
        Destroy(gameObject);
    }
    [SerializeField] private ItemData data;
}
