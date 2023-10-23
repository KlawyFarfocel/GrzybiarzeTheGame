using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject itemSelectionPanel;
    public Slot currentSlot;

    private void Awake()
    {
        Instance = this;
        itemSelectionPanel.SetActive(false);
    }

    public void ShowItemSelectionPanel(Slot slot)
    {
        Debug.Log("Wybrany slot to" + slot.ID);
        currentSlot = slot;
        itemSelectionPanel.SetActive(true);
        // currentSlot.GetComponent<Image>().color = Color.black;
    }

    public void SelectItem(Item item)
    {
        currentSlot.AddItemToSlot(item, currentSlot.ID);
        itemSelectionPanel.SetActive(false);
    }
}
