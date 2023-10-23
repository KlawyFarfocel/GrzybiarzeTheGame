using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class CreateEqItems : MonoBehaviour
{
    public static CreateEqItems Instance;
    public Item[] EqItems;
    private GameObject CreatedItem;
    private Slot slot;
    public Transform parentTransform;


    private void Start()
    {
        InventoryManager inventoryManager = InventoryManager.Instance;
    }
    private void Awake()
    {
        if (Instance == null)
        {
                Instance = this;
        }
    }
    public void SpawnObject(Item item)
    {
        // Tworzenie przedmiotu po klikniecuu na przycisk i resp jako dziecko eq
        //CreatedItem = item.prefab;
        //GameObject spawn = Instantiate(item.prefab);
        //spawn.transform.SetParent(parentTransform);
        //wywolanie funkcji dodania do slotu w plecaku

        EqManager.Instance.AddItem(item);

    }
    public void CreateItem()
    {
        //id na podstawie listy podanej w elemencie a nie ogolnych itemow
        int RandomIndex = Random.Range(0, EqItems.Length);
        SpawnObject(EqItems[RandomIndex]);
    }

    //gdy item ma byc stwrzony ponownie na podstawie id
    public void CreateItem(int id)
    {
        SpawnObject(EqItems[id]);
    }

}
