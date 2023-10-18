using JetBrains.Annotations;
using System.IO;
using UnityEngine;
using static UnityEditor.Progress;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance;
    public Slot[] slots;
    public Slot[] oldSlots;
    public GameObject eqHandler;


    private void Start()
    {
        
        slots = GetComponentsInChildren<Slot>();
        InventoryManager inventoryManager = InventoryManager.Instance;
        oldSlots = inventoryManager.slots;
        Debug.Log(slots.Length);
        for (int i = 0; i < oldSlots.Length; i++)
        {
            if (oldSlots[i] != null)
            {
                Debug.Log(oldSlots[i].ItemIcon);
                slots[i].ItemIcon.sprite = oldSlots[i].ItemIcon.sprite;
                slots[i].ItemIcon.gameObject.SetActive(true);
                slots[i].ItemInSlot = oldSlots[i].ItemInSlot;
            }
        }
        int val = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ItemIcon.sprite != null)
            {
                string b = slots[i].ItemIcon.sprite.ToString();
                string a = b.Remove(b.Length - 21);
                Debug.Log(a);
                Debug.Log(val);
                if (a == "grzyb1") val += 20;
                if (a == "grzyb2") val += 40;
                if (a == "grzyb3") val += 60;
                //  slots[i].ItemIcon.sprite = oldSlots[i].ItemIcon.sprite;
                // slots[i].ItemIcon.gameObject.SetActive(true);
                // slots[i].ItemInSlot = oldSlots[i].ItemInSlot;
            }
        }
        File.WriteAllText(Application.dataPath + "/Model/valuetoadd.txt",val.ToString());
    }

}
