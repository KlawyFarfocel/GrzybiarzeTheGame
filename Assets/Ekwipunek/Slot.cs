using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class Slot : MonoBehaviour
{
    public Item ItemInSlot;
    public Image ItemIcon;
    public Text ItemName;

    public void AddItemToSlot(Item item)
    {
        ItemInSlot = item;
        ItemIcon.sprite = item.Sprite;
        ItemIcon.gameObject.SetActive(true);
        ItemName.text = ItemInSlot.Name;
        ItemName.gameObject.SetActive(true);
    }

    public void RemoveItemFromSlot()
    {
        ItemInSlot = null;
        ItemIcon.sprite = null;
        ItemIcon.gameObject.SetActive(false);
        ItemName.text = null;
        ItemName.gameObject.SetActive(false);
    }
}
