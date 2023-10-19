using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddEquipment : MonoBehaviour
{
    public Item ItemInSlot;
    public Image ItemIcon;
    public Text ItemName;
    public InventoryManager inventoryManager;


    public void AddItemToEqSlot(Item item)
    {
        ItemInSlot = item;
        ItemIcon.sprite = item.Sprite;
        ItemIcon.gameObject.SetActive(true);
        ItemName.text = ItemInSlot.Name;
        ItemName.gameObject.SetActive(true);
    }
}
