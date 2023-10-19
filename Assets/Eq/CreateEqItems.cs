using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateEqItems : MonoBehaviour
{
    public Item[] EqItems;
    private GameObject CreatedItem;
    private Slot slot;
    private InventoryManager inventoryManager;
    public Transform parentTransform;


    private void Awake()
    {
        inventoryManager = InventoryManager.Instance;
    }
    private void SpawnObject(Item item)
    {
        // Tworzenie przedmiotu po klikniecuu na przycisk i resp jako dziecko eq
        CreatedItem = item.prefab;
        GameObject spawn = Instantiate(item.prefab);
        spawn.transform.SetParent(parentTransform);

        //wywolanie funkcji dodania do slotu w plecaku
       
    }

    public void CreateItem()
    {
        //id na podstawie listy podanej w elemencie a nie ogolnych itemow
        int RandomIndex = Random.Range(0, EqItems.Length);
        SpawnObject(EqItems[RandomIndex]);
    }

}
