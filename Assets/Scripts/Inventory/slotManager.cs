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
    }

}
