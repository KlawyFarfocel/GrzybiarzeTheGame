using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    private DBConnector dbConnector;
    public static InventoryManager Instance;
    public bool isNeededToStay = false;
    public Slot[] slots;
    public List<Item> Mushrooms;
    public int val;

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
    private void Start()
    {
        Debug.Log("InventoryManager Start: " + gameObject.scene.name);
        Mushrooms = new List<Item>();
        //dbConnector = GameObject.Find("UI").GetComponent<DBConnector>();

    }

    public void AddItem(Item item)
    {
        Mushrooms.Add(item);
        val += item.Value;
    }

    public int GetVal()
    {
        return val;
    }

    public void Clear()
    {
        Mushrooms.Clear();
        val = 0;
    }
}


