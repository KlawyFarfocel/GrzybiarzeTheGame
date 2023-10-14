using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Slot[] slots;

    private void Awake()
    {
        if(Instance == null)
        { Instance = this; }else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        slots = GetComponentsInChildren<Slot>();
    }

    public void AddItem(Item item)
    {
        for(int i=0; i<slots.Length; i++)
        {
            if (slots[i].ItemInSlot == null)
            {
                slots[i].AddItemToSlot(item);
                break;
            }
        }
    }
}
