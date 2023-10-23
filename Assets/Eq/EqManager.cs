using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqManager : MonoBehaviour
{
    public static EqManager Instance;
    public Slot[] slots;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Debug.Log("EqManager Start: " + gameObject.scene.name);
        slots = GetComponentsInChildren<Slot>();
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ItemInSlot == null)
            {
                slots[i].AddItemToSlot(item);
                break;
            }
        }
    }
    }