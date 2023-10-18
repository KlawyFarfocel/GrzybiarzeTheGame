using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class sell : MonoBehaviour
{
    private InventoryManager items=InventoryManager.Instance; //tutaj jest ten eq co przechodzi miedzy scenami 
    // Start is called before the first frame update
    public void add()
    {
        string wal = File.ReadAllText(Application.dataPath + "/Model/value.txt");
        string wal_add = File.ReadAllText(Application.dataPath + "/Model/valuetoadd.txt");

        int V=int.Parse(wal);
        int V_add=int.Parse(wal_add);
        int wynik=V+V_add;
        File.WriteAllText(Application.dataPath + "/Model/value.txt",wynik.ToString());
        File.WriteAllText(Application.dataPath + "/Model/valuetoadd.txt", "0");

        foreach (Slot slot in items.slots) //przepierdol sie po slotach i poodpinaj itemy
        {
            slot.RemoveItemFromSlot();
        }
    }
    public void Start()
    {
        items.ForceValueEvaluation();
    }
}
