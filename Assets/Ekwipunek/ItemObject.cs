using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour
{
    public Item item;
    public Text itemNameText;

    private void Start()
    {
        if (itemNameText != null && item != null)
        {
            itemNameText.text = item.Name;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("dodalo sie");
        InventoryManager.Instance.AddItem(item);
        gameObject.SetActive(false);
    }
}
