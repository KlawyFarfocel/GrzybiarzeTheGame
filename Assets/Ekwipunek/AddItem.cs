using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    public bool add;
    public Item ItemToAdd;
    public InventoryManager inventoryManager;

    void AddItems()
    {
        inventoryManager.AddItem(ItemToAdd);
        add = false;
    }
    private void Update()
    {
        if(add == true)
        {
            AddItems();
        }
    }
}
