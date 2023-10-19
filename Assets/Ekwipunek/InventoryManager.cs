using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public bool isNeededToStay = false;
    public Slot[] slots;


    private void Awake()
    {
        if (Instance == null)
        {
            if (isNeededToStay)
            {
                Instance = this;
                DontDestroyOnLoad(transform.root.gameObject);
            }
        }
    }
    public void ForceValueEvaluation()
    {
        Debug.Log("ForceValueEvaluation - Odpalam sie");
        int val = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ItemIcon.sprite != null)
            {
                val += slots[i].ItemInSlot.Value;
            }
        }
        File.WriteAllText(Application.dataPath + "/Model/valuetoadd.txt", val.ToString());
    }
    private void Start()
    {
        Debug.Log("InventoryManager Start: " + gameObject.scene.name);
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