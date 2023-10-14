using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


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
}
